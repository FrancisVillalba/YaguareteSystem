using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;

namespace YaguareteSystem.UI.Workflow.Compras
{
    public partial class CrearOrdenCompras : System.Web.UI.Page
    {
        LogsBLL lg = new LogsBLL();
        GestionDocumentosBLL ges = new GestionDocumentosBLL();
        ProcesosBLL pro = new ProcesosBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            alertError.Visible = false;
            alerCorrecto.Visible = false;

            if (lblControl.InnerText == "")
            {
                lblControl.InnerText = "cargado";
                lblDepartamento.Text = "Compras";
                llenarSolicitante();

                txtNroOC.Text = "0";
            }
        }
        private void llenarSolicitante()
        {
            string path = ConfigurationManager.AppSettings.Get("ActiveDirectory");
            ActiveDirectoryBLL aut = new ActiveDirectoryBLL(path);

            DataTable dt = aut.getTodosUsuarios();
            ddlSolicitante.DataSource = dt;
            ddlSolicitante.DataValueField = "usuario";
            ddlSolicitante.DataTextField = "nombre";
            ddlSolicitante.DataBind();
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Workflow/Compras/Compras.aspx", false);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            alertError.Visible = false;
            alerCorrecto.Visible = false;
            try
            {
                string mensaje = controlCarga();
                if (mensaje == "OK")
                {


                    int control = ges.InsertaOrdenCompras(txtNroOC.Text.Trim(), ddlTipoLegajo.SelectedValue, Convert.ToInt32(ddlProveedor.SelectedValue),
                    ddlMoneda.SelectedValue, txtMontoTotal.Text, ddlEmpresas.SelectedValue, ddlSolicitante.SelectedValue, txtNroSP.Text,
                    txtComentario.InnerText, Session["usuario"].ToString(), ddlSolicitante.SelectedItem.ToString(), Session["NombreApellido"].ToString(),
                    lblEsProyecto.Text, txtNroOrdenInterna.Text, ddlEsDEG.SelectedValue, txtNroContratoMarco.Text, ddlDireccinado.SelectedValue);

                    //ges.InsertaSeguimientoSolicitud(Convert.ToInt32(txtNroOC.Text), -1, Session["usuario"].ToString(), txtComentario.InnerText);

                    if (control == 1)
                    {
                        lg.registrarLogs(Session["usuario"].ToString(), "CrearOrdenCompras", "Se cargo una orden de compra Nro:" + txtNroOC.Text.Trim());
                        //insertaDatosDeCompras
                        lg.insertarComentario(Session["NombreApellido"].ToString(),0, "Se creó la OC " + txtNroOC.Text.Trim(), txtNroOC.Text.Trim(), "Carga", "Carga de OC", "Compras", "Compras");
                        //lg.insertarDatosReporteStatusFacturaPowerBI("Compras", "Compras", 0, Session["NombreApellido"].ToString(), "Carga", "Carga de OC", txtNroOC.Text, "", Session["NombreApellido"].ToString(),
                        //    ddlSolicitante.SelectedItem.ToString(), "", "");
                        limpiarCampo();
                        //alerCorrecto.Visible = true;
                        //lblMensajeOK.InnerText = "Registro se guardo con exito.";
                        Response.Redirect("/UI/Workflow/Compras/Compras.aspx", false);
                    }
                    else
                    if (control == -2)
                    {
                        alertError.Visible = true;
                        lblMensajeError.InnerText = "Ya existe una orden de compras con el número " + txtNroOC.Text.Trim();
                    }
                    else
                    {
                        alertError.Visible = true;
                        lblMensajeError.InnerText = "Problemas para guardar OC número " + txtNroOC.Text.Trim();
                    }
                }
                else
                {
                    alertError.Visible = true;
                    lblMensajeError.InnerText = mensaje;
                }
            }
            catch (Exception ex)
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Problemas para guardar registro." + ex.Message.ToString();
            }

        }
        private void limpiarCampo()
        {
            txtNroOC.Text = "";
            ddlTipoLegajo.SelectedValue = "Local";
            ddlProveedor.SelectedValue = "-1";
            ddlMoneda.SelectedValue = "-1";
            txtMontoTotal.Text = "";
            ddlEmpresas.SelectedValue = "-1";
            txtNroSP.Text = "";
            txtComentario.InnerText = "";
            ckEsProyecto.Checked = false;
            txtNroOrdenInterna.Text = "";
        }
        private string controlCarga()
        {
            string control = "OK";

            if (txtNroOC.Text == "")
            {
                control = "Debe cargar el número de la OC";
                return control;
            }

            if (ddlTipoLegajo.SelectedValue == "-1")
            {
                control = "Seleccione el tipo de documento";
                return control;
            }

            if (ddlProveedor.SelectedValue == "-1")
            {
                control = "Seleccione un proveedor";
                return control;
            }

            if (ddlMoneda.SelectedValue == "-1")
            {
                control = "Seleccione tipo de moneda";
                return control;
            }

            if (txtMontoTotal.Text == "")
            {
                control = "Cargue el monto total";
                return control;
            }

            if (ddlEmpresas.SelectedValue == "-1")
            {
                control = "Seleccine una empresa";
                return control;
            }

            if (ddlMoneda.SelectedValue == "PYG")
            {
                if (txtMontoTotal.Text.Contains(".") == false)
                { 
                    control = "El monto total debe tener puntos (.) Ej: 11.111";
                    return control;
                }
                else
                {
                    alertError.Visible = false;
                    lblMensajeError.InnerText = "";
                }
            }
            else
            {
                if (txtMontoTotal.Text.Contains(",") == false)
                { 
                    control = "El monto total debe tener coma (,) Ej: 11.111,44";
                    return control;
                }
                else
                {
                    if (txtMontoTotal.Text.Length > 6)
                    {
                        if (txtMontoTotal.Text.Contains(".") == false)
                        {
                            control = "El monto total debe tener coma (,) Ej: 11.111,44";
                            return control;
                        }
                    }
                }
            }

            return control;
        }

        protected void btnImportaDatos_Click(object sender, EventArgs e)
        {
            if (ddlTipoDocumentoAdjunto.SelectedValue == "-1")
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Seleccione un documento para adjuntar";

                return;
            }

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
                if (txtNroOC.Text != "0" || txtNroOC.Text != "")
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
                            var esActualizacion = "NO";
                            DataTable dt = ges.InsertaDatosDocumentos(txtNroOC.Text, FileNombre + " " + DateTime.Now.ToString("dd-MM-yyyy") + FileExtension, direccionArchivoFisico, direccionArchivoVirtual, "Compras", ""
                                                          , ddlTipoDocumentoAdjunto.SelectedValue, esActualizacion);

                            if (dt != null)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    if (row["ID"].ToString() == "-1")
                                    {
                                        alertError.Visible = true;
                                        lblMensajeError.InnerText = row["MENSAJE"].ToString();

                                        return;
                                    }
                                }
                            }

                            ddlTipoDocumentoAdjunto.DataBind();
                            gvDetalle.DataBind();
                            sdsDocumentos.DataBind();
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblID = (Label)clickedRow.FindControl("lblID");

            lg.registrarLogs(Session["usuario"].ToString(), "CrearOrdenCompras", "Se elimino un archivo adjunto con el nombre:" + clickedRow.Cells[0].Text);

            ges.EliminarDatosDocumentos(Convert.ToInt32(lblID.Text));

            ddlTipoDocumentoAdjunto.DataBind();
            gvDetalle.DataBind();
            sdsDocumentos.DataBind();
        }

        //protected void txtMontoTotal_TextChanged(object sender, EventArgs e)
        //{
        //    if (ddlMoneda.SelectedValue == "PYG")
        //    {
        //        if (txtMontoTotal.Text.Contains(".") == false)
        //        {
        //            alertError.Visible = true;
        //            lblMensajeError.InnerText = "El monto total debe tener puntos (.) Ej: 11.111";
        //        }
        //        else
        //        {
        //            alertError.Visible = false;
        //            lblMensajeError.InnerText = "";
        //        }
        //    }
        //    else
        //    {
        //        if (txtMontoTotal.Text.Contains(",") == false)
        //        {
        //            alertError.Visible = true;
        //            lblMensajeError.InnerText = "El monto total debe tener coma (,) Ej: 11.111,44";
        //        }
        //        else
        //        {
        //            if (txtMontoTotal.Text.Length > 6)
        //            {
        //                if (txtMontoTotal.Text.Contains(".") == false)
        //                {
        //                    alertError.Visible = true;
        //                    lblMensajeError.InnerText = "El monto total debe tener puntos (.) Ej: 11.111,44";
        //                }
        //                else
        //                {
        //                    alertError.Visible = false;
        //                    lblMensajeError.InnerText = "";
        //                }
        //            }
        //            else
        //            {
        //                alertError.Visible = false;
        //                lblMensajeError.InnerText = "";
        //            }
        //        }
        //    }
        //}

        protected void ckEsProyecto_CheckedChanged(object sender, EventArgs e)
        {
            if (ckEsProyecto.Checked == true)
            {
                lblEsProyecto.Text = "SI";
                txtNroOrdenInterna.Enabled = true;
            }
            else
            {
                lblEsProyecto.Text = "NO";
                txtNroOrdenInterna.Enabled = false;
            }
        }

        protected void btnAltaProveedores_Click(object sender, EventArgs e)
        {
            if (controlaCampos())
            {
                DataTable dt = ges.InsertaProveedorCompras(txtCodigoSap.Value, txtRuc.Value, txtRazonSocial.Value);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["ID"].ToString() == "-1")
                        {
                            alertError.Visible = true;
                            lblMensajeError.InnerText = "Este proveedor ya existe.";

                            return;
                        }
                        else
                        {
                            ddlProveedor.DataBind();
                            sdsProveedor.DataBind();
                            ddlProveedor.SelectedValue = row["ID"].ToString();
                        }
                        limpiarCampos();
                    }
                }
            }
        }
        private bool controlaCampos()
        {
            if (txtCodigoSap.Value == "")
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Ingrese el código de SAP del proveedor.";
                return false;
            }

            if (txtRazonSocial.Value == "")
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Ingrese razón social del proveedor.";
                return false;
            }

            return true;
        }

        private void limpiarCampos()
        {
            txtCodigoSap.Value = "";
            txtRazonSocial.Value = "";
            txtRuc.Value = "";
        }
    }
}