using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;

namespace YaguareteSystem.UI.Workflow.BuscadorGeneral
{
    public partial class InformacionFactura : System.Web.UI.Page
    {
        LogsBLL lg = new LogsBLL();
        GestionDocumentosBLL ges = new GestionDocumentosBLL();
        ProcesosBLL pro = new ProcesosBLL();
        SharePointBLL sharePoint = new SharePointBLL();
        AccesosBLL ac = new AccesosBLL();
        AlertasBLL m = new AlertasBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("/Default.aspx");
            }

            alertError.Visible = false;
            alerCorrecto.Visible = false;
            txtEntregado.Enabled = false;

            if (lblControl.InnerText == "")
            {
                lblControl.InnerText = "cargado";
                lblID.Text = Page.Request.QueryString["ID"].ToString();
                fuArchivo.Visible = false;
                btnImportaDatos.Enabled = false;
                pnlRechazoImpuestos.Visible = false;
                ddlEstadoDocumento.Enabled = false;
                btnEditar1.Enabled = false;

                DatosPrincipalesPorIdModels datosPrincipal = new DatosPrincipalesPorIdModels();
                List<DatosPrincipalesPorIdModels> lista = new List<DatosPrincipalesPorIdModels>();

                var json = sharePoint.RetornaDatosPorID(lblID.Text, Session["usuario"].ToString(), Session["pass"].ToString());
                datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesPorIdModels>(json);
                lista.Add(datosPrincipal);

                CargaFormulario(lista);
                CargaUltimoComentario(lista);
                CargaListaComentario(lista);
                CargaAdjuntos();
                controlMostrarFacturaRechazada();
            }
        }
        private void controlMostrarFacturaRechazada()
        {
            ArrayList list = (ArrayList)Session["grupos"];

            if (list.Count > 0)
            {
                foreach (string element in list)
                {
                    if (element == "ROLE_IMPUESTOS")
                    {
                        pnlRechazoImpuestos.Visible = true;
                    }

                    if (element == "ROLE_RECEPCION")
                    {
                        fuArchivo.Visible = true;
                        btnImportaDatos.Enabled = true;
                        ddlEstadoDocumento.Enabled = true;
                        btnEditar1.Enabled = true;
                    }
                }
            }
        }
        private void CargaAdjuntos()
        {//DataTable dtDocumentos = new DataTable();
            //dtDocumentos.Clear();
            //dtDocumentos.Columns.Add("ID");
            //dtDocumentos.Columns.Add("OC");
            //dtDocumentos.Columns.Add("DOCNOMBRE");
            //dtDocumentos.Columns.Add("RUTA");

            //foreach (DatosPrincipalesPorIdModels p in lista)
            //{
            //    foreach (AttachmentFile v in p.AttachmentFiles)
            //    {
            //        object[] o = { p.Id, p.Title, v.FileName, ConfigurationManager.AppSettings.Get("UrlSharePoint") + @"/Lists/" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "/Attachments/" + p.Id + "/" + v.FileName };
            //        dtDocumentos.Rows.Add(o);
            //    }
            //}
            var dt = pro.retornaDatosDocumentos(lblNroOC.Text, lblDepartamento.Text, txtNroFactura.Text);
            gvDetalle.DataSource = dt;
            gvDetalle.DataBind();
        }
        private void CargaListaComentario(List<DatosPrincipalesPorIdModels> lista)
        {
            DataTable dtListaComentario = lg.getListaComentario(lblNroOC.Text, Convert.ToInt32(lblID.Text));

            gvListaComentarios.DataSource = dtListaComentario;
            gvListaComentarios.DataBind();
        }
        private void CargaUltimoComentario(List<DatosPrincipalesPorIdModels> lista)
        {
            //Crea la tabla para cargar la grilla
            DataTable dtUltimoComentario = new DataTable();
            dtUltimoComentario.Clear();
            dtUltimoComentario.Columns.Add("USUARIO");
            dtUltimoComentario.Columns.Add("DEPARTAMENTO");
            dtUltimoComentario.Columns.Add("FECHA");
            dtUltimoComentario.Columns.Add("MOTIVO");
            dtUltimoComentario.Columns.Add("COMENTARIO");

            foreach (DatosPrincipalesPorIdModels p in lista)
            {
                string departamento = p.DocDepartamentoAnterior.ToString();

                if (p.DocDepartamentoAnterior.ToString() == "Recepcion")
                {
                    departamento = "Recepción";
                }

                if (p.DocDepartamentoAnterior.ToString() == "RevisionCompras")
                {
                    departamento = "Revision por compras";
                }

                if (p.DocDepartamentoAnterior.ToString() == "CuentaPagar")
                {
                    departamento = "Cuentas a pagar";
                }

                if (p.DocDepartamentoAnterior.ToString() == "Tesoreria")
                {
                    departamento = "Tesorería";
                }

                object[] o = { p.DocUltimoUsuarioAccion, departamento, p.DocFechaUltimaAccion.ToString().Substring(0, 10), p.DocUltimaAccion, p.DocUltimoComentario };
                dtUltimoComentario.Rows.Add(o);
            }

            gvUltimoComentario.DataSource = dtUltimoComentario;
            gvUltimoComentario.DataBind();
        }
        private void CargaFormulario(List<DatosPrincipalesPorIdModels> lista)
        {
            foreach (DatosPrincipalesPorIdModels p in lista)
            {
                ddlNroOC.SelectedValue = p.Title;
                lblNroOC.Text = p.Title;

                if (p.RecepNroFactura == null)
                {
                    txtNroFactura.Text = "";
                }
                else
                {
                    txtNroFactura.Text = p.RecepNroFactura;
                }

                if (p.RecepFechaFactura == null)
                {
                    txtFechaFactura.Text = "";
                }
                else
                {
                    txtFechaFactura.Text = p.RecepFechaFactura.ToString().Substring(0, 10);
                }


                if (p.RecepTipoDocumento == null)
                {
                    ddlTipoDocumento.SelectedValue = "-1";
                }
                else
                {
                    ddlTipoDocumento.SelectedValue = p.RecepTipoDocumento;
                }
                if (p.RecepTimbrado == null)
                {
                    txtTimbrado.Text = "";
                }
                else
                {
                    txtTimbrado.Text = p.RecepTimbrado.ToString();
                }

                if (p.RecepMontoFactura == null)
                {
                    txtMontoTotal.Text = "";
                }
                else
                {
                    txtMontoTotal.Text = p.RecepMontoFactura.ToString();
                }

                // txtMontoTotal.Text = p.RecepMontoFactura.ToString();
                //ddlEstadoDocumento.SelectedValue = p.RecepOriginalCopia;
                if (p.RecepOriginalCopia == null)
                {
                    ddlEstadoDocumento.SelectedValue = "-1";
                }
                else
                {
                    ddlEstadoDocumento.SelectedValue = p.RecepOriginalCopia.ToString();
                }

                if (p.DocIdUltimaAccion == null)
                {
                    lblMensajeFacturaRechazada.Text = "";
                }
                else
                {
                    if (p.DocIdUltimaAccion.ToString() == "43")
                    {
                        lblMensajeFacturaRechazada.Text = p.DocUltimoComentario;
                    }
                }

                if (p.CuentaPagarNroAsiento == null)
                {
                    txtNroAsientoCuentaPagar.Text = "";
                }
                else
                {
                    txtNroAsientoCuentaPagar.Text = p.CuentaPagarNroAsiento.ToString();
                }
            }
        }
        protected void ckEsProyecto_CheckedChanged(object sender, EventArgs e)
        {
            if (ckEsProyecto.Checked == true)
            {
                txtEntregado.Enabled = true;
            }
            else
            {
                txtEntregado.Enabled = false;
                txtEntregado.Text = "";
            }
        }

        protected void btnGuardarRechazado_Click(object sender, EventArgs e)
        {
            try
            {
                string comentario = "Factura rechazada, factura física entregada a: " + txtEntregado.Text;
                sharePoint.RechazaFacturaFisicaImpuestos(Convert.ToInt64(lblID.Text), Session["usuario"].ToString(), Session["pass"].ToString(), comentario,
                       Session["NombreApellido"].ToString(), "Factura física entregada", "Rechazados", "43", "Rechazados", "NO", "Impuestos", "Rechazado");

                lblMensajeFacturaRechazada.Text = comentario;
            }
            catch (Exception ex)
            {
                m.alert("Problema para rechazar factura");
            }

        }

        protected void btnImportaDatos_Click(object sender, EventArgs e)
        {

            if (!fuArchivo.HasFile)
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Campo vacio";
            }
            else
            {

                ImportaArchivo();
            }
        }
        private void ImportaArchivo()
        {
            DataTable dr = new DataTable();

            try
            {
                if (ddlNroOC.SelectedValue != "-1")
                {
                    foreach (HttpPostedFile postedFile in fuArchivo.PostedFiles)
                    {
                        string PathFisico = ConfigurationManager.AppSettings.Get("RutaFisicaDocumentos") + Session["usuario"].ToString() + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + @"\";
                        string PathVirtual = ConfigurationManager.AppSettings.Get("RutaVirtualDocumentos") + Session["usuario"].ToString() + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + @"/";

                        var FileExtension = System.IO.Path.GetExtension(postedFile.FileName).Substring(0);
                        string FileNombre = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);


                        if (pro.creaDirectorio(PathFisico))
                        {
                            string direccionArchivoFisico = PathFisico + FileNombre + " " + DateTime.Now.ToString("dd-MM-yyyy") + FileExtension;
                            string direccionArchivoVirtual = PathVirtual + FileNombre + " " + DateTime.Now.ToString("dd-MM-yyyy") + FileExtension;

                            fuArchivo.SaveAs(direccionArchivoFisico);

                            ges.InsertaDatosDocumentos(ddlNroOC.SelectedValue, FileNombre + " " + DateTime.Now.ToString("dd-MM-yyyy") + FileExtension, direccionArchivoFisico,
                                direccionArchivoVirtual, "Recepcion", txtNroFactura.Text, "", "NO");

                            CargaAdjuntos(); 
                        }
                        else
                        {
                            alertError.Visible = true;
                            lblMensajeError.InnerText = "No se pudo importar archivos. Verifique";
                        }
                    }
                }
                else
                {
                    alertError.Visible = true;
                    lblMensajeError.InnerText = "Debe cargar número de Orden de compras para suber archivos";
                }
            }
            catch (Exception ex)
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "No se pudo Importar. Verifique";
            }
        } 

        protected void btnEditar1_Click(object sender, EventArgs e)
        {
            try
            {
                sharePoint.ActualizaEstadoDocumento(Convert.ToInt64(lblID.Text), Session["usuario"].ToString(), Session["pass"].ToString(), ddlEstadoDocumento.SelectedValue, "NO");

                m.alert("Factura modificada con exito.");
            }
            catch (Exception ex)
            {
                m.alert("Problemas para modificar factura.");
            }
        }
    }
}
