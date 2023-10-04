using ClosedXML.Excel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;

namespace YaguareteSystem.UI.Tesaka
{
    public partial class Tesaka : System.Web.UI.Page
    {
        ProcesosBLL p = new ProcesosBLL();
        LogsBLL lg = new LogsBLL();
        BackgroundWorker tarea = new BackgroundWorker();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGeneraJson_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            OracleDataAdapter adap = new OracleDataAdapter();
            if (controlCargaCampos())
            {
                alertError.Visible = false;
                lblMensaje.InnerText = "";

                ds = p.sp_fechasFacturasTesaka(txtFechaFacIni001.Value, ddlEmpresas.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lg.registrarLogs(Session["usuario"].ToString(), "Tesaka", "Se generó archivo Json, Datos = Fecha_Retención: " + txtFechaRetencion001.Value + " Fecha_Autofactura: " + txtFechaFacIni001.Value + " Empresa: " + ddlEmpresas.SelectedItem.ToString());

                        alertError.Visible = false;
                        lblMensaje.InnerText = "";

                        byte[] byteArray = p.sp_retornaDatosJson(Convert.ToDateTime(txtFechaRetencion001.Value).ToString("dd/MM/yyyy"), row["FECHAS"].ToString(), ddlEmpresas.SelectedValue);
                        Response.AddHeader("Content-Disposition", "attachment; filename=tesaka(" + row["FECHAS"].ToString().Substring(0, 2) + ").json");
                        Response.ContentType = "application/json";
                        Response.BinaryWrite(byteArray.ToArray());
                        Response.End();
                    }
                }
                else
                {
                    lg.registrarLogs(Session["usuario"].ToString(), "Tesaka", "No existen datos para generar Archivo Json.");
                    alertError.Visible = true;
                    lblMensaje.InnerText = "No existen datos para generar json.";
                    return;
                }
            }
        }
        private void limpiarCampos()
        {
            txtFechaRetencion001.Value = "";
            txtFechaFacIni001.Value = "";
            ddlEmpresas.SelectedValue = "001";
        }
        private bool controlCargaCampos()
        {
            if (txtFechaRetencion001.Value == "")
            {
                alertError.Visible = true;
                lblMensaje.InnerText = "Cargue la fecha de retención";
                return false;
            }

            if (txtFechaFacIni001.Value == "")
            {
                alertError.Visible = true;
                lblMensaje.InnerText = "Seleccione fecha de la factura";
                return false;
            }

            return true;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            OracleDataAdapter adap = new OracleDataAdapter();
            if (controlCargaCampos())
            {
                alertError.Visible = false;
                lblMensaje.InnerText = "";

                ds = p.sp_fechasFacturasTesaka(txtFechaFacIni001.Value, ddlEmpresas.SelectedValue);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lg.registrarLogs(Session["usuario"].ToString(), "Tesaka", "Se generó archivo Excel, Datos = Fecha_Retención: " + txtFechaRetencion001.Value + " Fecha_Autofactura: " + txtFechaFacIni001.Value + " Empresa: " + ddlEmpresas.SelectedItem.ToString());

                        alertError.Visible = false;
                        lblMensaje.InnerText = "";

                        DataTable dt = p.sp_retornaDatosDataTable(Convert.ToDateTime(txtFechaRetencion001.Value).ToString("dd/MM/yyyy"), row["FECHAS"].ToString(), ddlEmpresas.SelectedValue);
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt, "Products");

                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment;filename=Reporte.xlsx");
                            using (MemoryStream MyMemoryStream = new MemoryStream())
                            {
                                wb.SaveAs(MyMemoryStream);
                                MyMemoryStream.WriteTo(Response.OutputStream);
                                Response.Flush();
                                Response.End();
                            }
                        }
                        Response.End();
                    }
                }
                else
                {
                    lg.registrarLogs(Session["usuario"].ToString(), "Tesaka", "No existen datos para generar Archivo Json.");
                    alertError.Visible = true;
                    lblMensaje.InnerText = "No existen datos para generar datos.";
                    return;
                }
            }
        }

        protected void btnImportaDatos_Click(object sender, EventArgs e)
        {
            if (!fuArchivo.HasFile)
            {
                alertError2.Visible = true;
                lblMensaje2.InnerText = "Seleccione un archivo para importar";
            }
            else
            {

                alertError2.Visible = false;
                lblMensaje2.InnerText = "";

                tarea.DoWork += ImportaArchivo;
                tarea.RunWorkerAsync();

                //ImportaArchivo();
            }
        }
        private void ImportaArchivo(object o, DoWorkEventArgs e) 
        {
            alertError2.Visible = false;

            string FileName = System.IO.Path.GetFileName(fuArchivo.PostedFile.FileName);
            lblExtension.Text = System.IO.Path.GetExtension(fuArchivo.PostedFile.FileName);

            if (controlCargaCamposImportacion())
            {
                if (lblExtension.Text == ".csv" || lblExtension.Text == ".CSV")
                {
                    try
                    {
                        string FolderPath = @"C:\ruta\";
                        if (p.creaDirectorio(FolderPath))
                        {
                            lblDirArchivo.Text = FolderPath + FileName;
                            fuArchivo.SaveAs(FolderPath + FileName);
                            string procesos = importarTxtCamp(lblDirArchivo.Text);
                            if (procesos == "")
                            {
                                alertError2.Visible = true;
                                lblMensaje2.InnerText = "Solo puede importar hasta 1.000 Registros. Verifique";
                            }
                            else
                            {
                                byte[] byteArray = p.sp_retornaDatosJsonImportacion(Convert.ToDateTime(txtRetencionImportacion.Value).ToString("dd/MM/yyyy"), procesos);
                                Response.AddHeader("Content-Disposition", "attachment; filename=tesaka(" + txtRetencionImportacion.Value.Substring(0, 2) + ").json");
                                Response.ContentType = "application/json";
                                Response.BinaryWrite(byteArray.ToArray());
                                Response.End();

                            }
                        }

                        else
                        {
                            alertError2.Visible = true;
                            lblMensaje2.InnerText = "No se pudo importar registros. Verifique";
                        }
                    }
                    catch
                    {
                        alertError2.Visible = true;
                        lblMensaje2.InnerText = "No se pudo Importar. Verifique";
                    }
                }
                else
                {
                    alertError2.Visible = true;
                    lblMensaje2.InnerText = "Archivo No Valido. Verifique";
                }
            }
        }
        public string importarTxtCamp(string FilePath)
        {
            string concatenado = "";
            string[] columns = null;

            var lines = File.ReadAllLines(FilePath);

            if (lines.Count() > 0)
            {
                columns = lines[0].Split(new char[] { ';' });
            }

            for (int i = 1; i < lines.Count(); i++)
            {
                string[] values = lines[i].Split(new char[] { ';' });

                for (int j = 0; j < values.Count() && j < columns.Count(); j++)
                {
                    concatenado = concatenado + values[j] + ",";
                }

            }
            return concatenado.Remove(concatenado.Length - 1);
        }

        protected void btnExcelImportacion_Click(object sender, EventArgs e)
        {
            if (!fuArchivo.HasFile)
            {
                alertError2.Visible = true;
                lblMensaje2.InnerText = "Seleccione un archivo para importar";
            }
            else
            {

                alertError2.Visible = false;
                lblMensaje2.InnerText = "";

                tarea.DoWork += ImportaArchivoExcel;
                tarea.RunWorkerAsync();

                //ImportaArchivo();
            }
        }

        private void ImportaArchivoExcel(object o, DoWorkEventArgs e)
        {

            DataSet ds = new DataSet();
            OracleDataAdapter adap = new OracleDataAdapter();
            string FileName = System.IO.Path.GetFileName(fuArchivo.PostedFile.FileName);
            lblExtension.Text = System.IO.Path.GetExtension(fuArchivo.PostedFile.FileName);

            if (lblExtension.Text == ".csv" || lblExtension.Text == ".CSV")
            {
                if (controlCargaCamposImportacion())
                {
                    string FolderPath = @"C:\ruta\";
                    if (p.creaDirectorio(FolderPath))
                    {
                        lblDirArchivo.Text = FolderPath + FileName;
                        fuArchivo.SaveAs(FolderPath + FileName);

                        string procesos = importarTxtCamp(lblDirArchivo.Text);
                        if (procesos == "")
                        {
                            alertError2.Visible = true;
                            lblMensaje2.InnerText = "Solo puede importar hasta 1.000 Registros. Verifique";
                        }
                        else
                        {
                            DataTable dt = p.sp_retornaDatosDatatableImportacion(Convert.ToDateTime(txtRetencionImportacion.Value).ToString("dd/MM/yyyy"), procesos);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "Products");
                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Reporte.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                            Response.End();

                        }
                    }
                }
            }
            else
            {
                alertError2.Visible = true;
                lblMensaje2.InnerText = "Archivo No Valido. Verifique";
            }
        }

        private bool controlCargaCamposImportacion()
        {
            if (txtRetencionImportacion.Value == "")
            {
                alertError2.Visible = true;
                lblMensaje2.InnerText = "Cargue la fecha de retención";
                return false;
            }

            return true;
        }
    }
}