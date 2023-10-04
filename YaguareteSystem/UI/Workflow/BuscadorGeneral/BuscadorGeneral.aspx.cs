using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;

namespace YaguareteSystem.UI.Workflow.BuscadorGeneral
{
    public partial class BuscadorGeneral : System.Web.UI.Page
    {
        SharePointBLL sharePoint = new SharePointBLL();
        ProcesosBLL pro = new ProcesosBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("/Default.aspx");
            }

            btnExportExcel.Visible = false;


        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                DatosPrincipalesSharePointModels datosPrincipal = new DatosPrincipalesSharePointModels();
                List<DatosPrincipalesSharePointModels> lista = new List<DatosPrincipalesSharePointModels>();
                string url = "https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$filter=((impTipoDocumentoReporte eq '1')";

                string json = "";

                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("NroFactura");
                dt.Columns.Add("NroOC");
                dt.Columns.Add("Departamento");
                dt.Columns.Add("TipoDocumento");
                dt.Columns.Add("Proveedor");
                dt.Columns.Add("Comprador");
                //dt.Columns.Add("CodigoSAP");
                dt.Columns.Add("Accion");
                dt.Columns.Add("Comentario");

                if (ddlDepartamento.SelectedValue != "-1")
                {
                    url = url + " and (docDepartamento eq '" + ddlDepartamento.SelectedValue.ToString() + "')";
                }

                if (ddlTipoLegajo.SelectedValue != "-1")
                {
                    url = url + " and (comTipoLegajo eq '" + ddlTipoLegajo.SelectedValue.ToString() + "')";
                }

                if (txtNroFactura.Text != "")
                {
                    url = url + " and (recepNroFactura eq '" + txtNroFactura.Text + "')";
                }

                if (txtNroProyecto.Text != "")
                {
                    url = url + " and (comOrdenInterna eq '" + txtNroProyecto.Text + "')";
                }

                if (ddlNroOC.SelectedValue != "-1")
                {
                    url = url + " and (Title eq '" + ddlNroOC.SelectedValue + "')";
                }

                if (ddlAccion.SelectedValue != "-1")
                {
                    url = url + " and (docIdUltimaAccion eq '" + ddlAccion.SelectedValue + "')";
                }

                if (ddlListaCompradores.SelectedValue != "-1")
                {
                    url = url + " and (comCreadoPor eq '" + ddlListaCompradores.SelectedValue + "')";
                }

                if (ddlEsDEG.SelectedValue != "-1")
                {
                    url = url + " and (esDeg eq '" + ddlEsDEG.SelectedValue + "')";
                }



                json = sharePoint.RetornaDatosBuscadorGeneral(url + ")", Session["usuario"].ToString(), Session["pass"].ToString());


                datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesSharePointModels>(json);

                lista.Add(datosPrincipal);

                foreach (DatosPrincipalesSharePointModels p in lista)
                {
                    foreach (Value v in p.Values)
                    {
                        //if (v.RecepAccion != null)
                        //{
                        string tipoLegajo = v.ComTipoLegajo;
                        if (v.ComTipoLegajo == "Importacion")
                        {
                            tipoLegajo = "Importación";
                        }

                        object[] obj = { v.Id, v.RecepNroFactura, v.Title, v.DocNombreDepartamento, tipoLegajo, v.RecepProveedorNombre, v.ComCreadoNombre, v.DocUltimaAccion, v.DocUltimoComentario };
                        dt.Rows.Add(obj);
                        //}
                    }
                }
                Session["dtBuscador"] = dt;
                btnExportExcel.Visible = true;
                gvBuscador.DataSource = dt;
                gvBuscador.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                 
                DataTable dt = Session["dtBuscador"] as DataTable;
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Products");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=BuscadorGeneral.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}