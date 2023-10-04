
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;

namespace YaguareteSystem.UI.Workflow.Tesoreria
{
    public partial class ImportarDatosTesoreria : System.Web.UI.Page
    {
        AlmacenBLL p = new AlmacenBLL();
        ProcesosBLL pro = new ProcesosBLL();
        LogsBLL lg = new LogsBLL();
        BackgroundWorker tarea = new BackgroundWorker();
        SharePointBLL sharePoint = new SharePointBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlLoad.Visible = false;
        }

        protected void btnImportaDatos_Click(object sender, EventArgs e)
        {
            pnlLoad.Visible = true;
            pnlImportar.Visible = true;
            alertError.Visible = false;
            alerCorrecto.Visible = false;

            if (!fuArchivo.HasFile)
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Campo vacio";
            }
            else
            {
                tarea.DoWork += ImportaArchivo;
                tarea.RunWorkerAsync();
            }
        }
        private void ImportaArchivo(object o, DoWorkEventArgs e)
        {
            DatosPrincipalesSharePointModels datosPrincipal = new DatosPrincipalesSharePointModels(); 
            DataTable dr = new DataTable();
           
            string url, json = "";
            int control = 0;


            string FileName = System.IO.Path.GetFileName(fuArchivo.PostedFile.FileName);
            lblExtension.Text = System.IO.Path.GetExtension(fuArchivo.PostedFile.FileName);
            if (lblExtension.Text == ".csv" || lblExtension.Text == ".CSV")
            {
                try
                {
                    string FolderPath = @"C:\ruta\"+DateTime.Now.ToString("dd/MM/yyyy")+"\\";
                    if (pro.creaDirectorio(FolderPath))
                    {
                        lblDirArchivo.Text = FolderPath + FileName;
                        fuArchivo.SaveAs(FolderPath + FileName);
                        dr = pro.importarTxtCamp(lblDirArchivo.Text);
                        if (dr.Rows.Count > 5000)
                        {
                            alertError.Visible = true;
                            lblMensajeError.InnerText = "Solo puede importar hasta 5.000 Registros. Verifique";
                        }
                        else
                        {
                            foreach (DataRow row in dr.Rows)
                            {
                                url = "https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('"+ ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$select=ID,docDepartamento&$filter=((cuentaPagarNroAsiento eq '" + row["ASIENTO_CUENTA_PAGAR"].ToString() + "') and (comEmpresa eq '" + row["EMPRESA"].ToString() + "'))";
                                json = sharePoint.RetornaDatosBuscadorGeneral(url, Session["usuario"].ToString(), Session["pass"].ToString());

                                datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesSharePointModels>(json);
                                List<DatosPrincipalesSharePointModels> lista = new List<DatosPrincipalesSharePointModels>();
                                lista.Add(datosPrincipal);

                                foreach (DatosPrincipalesSharePointModels p in lista)
                                {
                                    foreach (Value v in p.Values)  
                                    {
                                        if (v.DocDepartamento != "Procesadas")
                                        {
                                            //object[] obj = { v.Id};
                                            control = sharePoint.ActualizaCamposSubaMasivaTesoreria(v.Id, Session["usuario"].ToString(), Session["pass"].ToString(), 
                                                row["NRO_RETENCION"].ToString(), row["COMPENSACION-PAGO"].ToString(), "NO", "Pendiente procesar", row["FECHA_REAL_PAGO"].ToString());

                                            if (control == 1)
                                            {
                                                pnlLoad.Visible = false;
                                                pnlImportar.Visible = true;
                                                alerCorrecto.Visible = true;
                                                lblMensajeOK.InnerText = "Actualizado correctamente";
                                            }
                                            else
                                            {
                                                pnlLoad.Visible = false;
                                                pnlImportar.Visible = true;
                                                alertError.Visible = true;
                                                lblMensajeError.InnerText = "Algo salio mal al actualizar el registro "+ row["NRO_RETENCION"].ToString();
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    } 
                    else
                    {
                        alertError.Visible = true;
                        lblMensajeError.InnerText = "No se pudo importar registros. Verifique";

                        return;
                    }

                }
                catch
                {
                    alertError.Visible = true;
                    lblMensajeError.InnerText = "No se pudo Importar. Verifique";

                    return;
                }
            }
            else
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Archivo No Valido. Verifique";
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Workflow/Tesoreria/Tesoreria.aspx");
        }

        protected void btnVista_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Workflow/Tesoreria/VistaImportado.aspx");
        }
    }
}