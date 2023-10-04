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
    public partial class ModificarOrdenCompras : System.Web.UI.Page
    {
        LogsBLL lg = new LogsBLL();
        GestionDocumentosBLL ges = new GestionDocumentosBLL();
        ProcesosBLL pro = new ProcesosBLL();
        SharePointBLL sp = new SharePointBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("/Default.aspx");
            }
            alertError.Visible = false;
            alerCorrecto.Visible = false;
             
            if (lblControl.InnerText == "")
            {
                lblID.InnerText = Page.Request.QueryString["ID"].ToString();
                lblProcesado.InnerText = "NO";
                lblControl.InnerText = "cargado";
                lblDepartamento.Text = "Compras";
                lblUsuario.Text = Session["usuario"].ToString();
                lblNombreUsuario.Text = Session["NombreApellido"].ToString();
                lblPass.Text = Session["pass"].ToString(); 
                llenarSolicitante();
                cargaDatos();
            }
        }

        private void cargaDatos()
        {

            DataTable dt = ges.RetornaDatosOrdenComprasCompras(Convert.ToInt32(lblID.InnerText));

            foreach (DataRow row in dt.Rows)
            {
                //lblCodigoSap.InnerText = row["provCodigoSAP"].ToString();
                if (row["provCodigoSAP"].ToString() == "")
                {
                    ddlProveedor.SelectedValue = "-1";
                }
                else
                {
                    ddlProveedor.SelectedValue = row["oComProveedorID"].ToString();
                }
                ddlTipoLegajo.Text = row["oComTipoDocumentoID"].ToString();
                txtNroOC.Text = row["oComNroOC"].ToString();

                ddlMoneda.Text = row["oComTipoMonedaID"].ToString();
                // txtMontoTotal.Text = ;
                txtMontoTotal.Text = row["oComMontoTotal"].ToString();
                ddlEmpresas.Text = row["oComCodigoEmpresa"].ToString();
                ddlSolicitante.Text = row["oComUsuarioSolicitante"].ToString();
                txtNroSP.Text = row["oComNroSP"].ToString();
                ddlEstado.Text = row["oComEstado"].ToString();
                lblCodigoSap.InnerText = row["provCodigoSAP"].ToString();
                ddlDireccionado.SelectedValue = row["provEsDireccinado"].ToString();
                lblRuc.InnerText = row["provRUC"].ToString();
                lblFechaAdd.InnerText = row["oComFechaAdd"].ToString();
                lblUsuCreador.InnerText = row["oComUsuarioCreador"].ToString();
                lblNombreCreador.Text = row["oComNombreCreador"].ToString();
                lblNombreProveedor.InnerText = row["provRazonSocial"].ToString();
                txtNroOrdenInterna.Text = row["oComNroOrdenInterna"].ToString();
                txtNroContratoMarco.Text = row["ocContratoMarco"].ToString();
                lblEsProyecto.InnerText = row["oComEsProyecto"].ToString();
                ddlEsDEG.SelectedValue = row["esDeg"].ToString();
                if (lblEsProyecto.InnerText == "SI")
                {
                    ckEsProyecto.Checked = true;
                }
                else
                {
                    ckEsProyecto.Checked = false;
                    txtNroOrdenInterna.Enabled = false;
                }

                if (row["oComEnUso"].ToString() == "SI")
                {
                    ckProcesado.Checked = true;
                    lblProcesado.InnerText = "SI";
                    btnGuardar.Visible = false;
                    btnActualizar.Visible = true;
                    lblActualizacion.Text = "SI";
                }
                else
                {
                    ckProcesado.Checked = false;
                    lblProcesado.InnerText = "NO";
                    btnGuardar.Visible = true;
                    btnActualizar.Visible = false;
                    lblActualizacion.Text = "NO";
                }

                txtComentario.InnerText = row["oComComentario"].ToString();
                //lblCorreoProveedor.InnerText = row["provCorreo"].ToString();
            }




        }
        protected void ckEsProyecto_CheckedChanged(object sender, EventArgs e)
        {
            if (ckEsProyecto.Checked == true)
            {
                lblEsProyecto.InnerText = "SI";
                txtNroOrdenInterna.Enabled = true;
            }
            else
            {
                lblEsProyecto.InnerText = "NO";
                txtNroOrdenInterna.Enabled = false;
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
            Response.Redirect("/UI/Workflow/Compras/Compras.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            alertError.Visible = false;
            alerCorrecto.Visible = false;
            DataTable dt = new DataTable();
            try
            {
                string mensaje = controlCarga();
                if (mensaje == "OK")
                {
                    int control = 0;
                    if (lblProcesado.InnerText == "SI")
                    {
                        int cantidad = ges.controlCargaAdjunto(txtNroOC.Text.Trim());
                        if (cantidad == -1)
                        {
                            alertError.Visible = true;
                            lblMensajeError.InnerText = "Debe adjuntar los documentos obligatorios para procesar";

                            return;
                        }

                        control = ges.ModificarOrdenCompras(Convert.ToInt32(lblID.InnerText), lblProcesado.InnerText, txtNroOC.Text.Trim(), ddlTipoLegajo.Text,
                              Convert.ToInt32(ddlProveedor.SelectedValue), ddlMoneda.SelectedValue, txtMontoTotal.Text, ddlEmpresas.SelectedValue, ddlSolicitante.SelectedValue,
                                  ddlSolicitante.SelectedItem.ToString(), txtNroSP.Text, ddlEstado.SelectedValue, txtComentario.InnerText, lblEsProyecto.InnerText, txtNroOrdenInterna.Text, 
                                  ddlEsDEG.SelectedValue, txtNroContratoMarco.Text, ddlDireccionado.SelectedValue);


                        //dt = ges.RetornaDatosDocumentos(txtNroOC.Text, "Compras", "");
                        sp.InsertSharePointAdjuntos(Session["usuario"].ToString(), Session["pass"].ToString(), txtNroOC.Text.Trim(), "Procesado", txtComentario.InnerText, "Recepcion",
                            ddlTipoLegajo.SelectedValue, lblCodigoSap.InnerText, lblRuc.InnerText, lblNombreProveedor.InnerText, ddlMoneda.SelectedValue, txtMontoTotal.Text,
                            ddlEmpresas.SelectedValue, txtNroSP.Text, txtComentario.Value, lblProcesado.InnerText, Convert.ToDateTime(lblFechaAdd.InnerText), DateTime.Now, ddlSolicitante.SelectedValue,
                            ddlSolicitante.SelectedItem.ToString(), lblUsuCreador.InnerText, lblNombreCreador.Text, lblEsProyecto.InnerText, txtNroOrdenInterna.Text,
                            "SI", "Compras", lblRuc.InnerText.Substring(lblRuc.InnerText.Length - 1, 1), "Creado", ddlDireccionado.SelectedValue, 
                            ddlEsDEG.SelectedValue, txtNroContratoMarco.Text);

                        lg.insertarComentario(Session["NombreApellido"].ToString(), 0, "Se proceso la OC " + txtNroOC.Text.Trim(), txtNroOC.Text.Trim(), "Procesar", "Procesado", "Compras", "Recepción");

                        //lg.insertarDatosReporteStatusFacturaPowerBI("Compras", "Recepción",0, Session["NombreApellido"].ToString(),"Procesar", "Procesado", txtNroOC.Text,"", Session["NombreApellido"].ToString(),
                        //    ddlSolicitante.SelectedItem.ToString(),"", "");
                    }
                    else
                    {
                        control = ges.ModificarOrdenCompras(Convert.ToInt32(lblID.InnerText), lblProcesado.InnerText, txtNroOC.Text.Trim(), ddlTipoLegajo.Text,
                              Convert.ToInt32(ddlProveedor.SelectedValue), ddlMoneda.SelectedValue, txtMontoTotal.Text, ddlEmpresas.SelectedValue, ddlSolicitante.SelectedValue,
                                  ddlSolicitante.SelectedItem.ToString(), txtNroSP.Text, ddlEstado.SelectedValue, txtComentario.InnerText, lblEsProyecto.InnerText, txtNroOrdenInterna.Text, 
                                  ddlEsDEG.SelectedValue, txtNroContratoMarco.Text, ddlDireccionado.SelectedValue);

                        lg.registrarLogs(Session["usuario"].ToString(), "ModificarOrdenCompras", "Se modifico una orden de compra Nro:" + txtNroOC.Text.Trim());
                    }

                    if (control == 1)
                    { 
                        limpiarCampo();
                        //alerCorrecto.Visible = true;
                        //lblMensajeOK.InnerText = "Registro se guardo con exito.";
                        Response.Redirect("/UI/Workflow/Compras/Compras.aspx");
                    }
                    else
                    {
                        alertError.Visible = true;
                        lblMensajeError.InnerText = "Problemas para guardar registro.";
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
            ddlEstado.SelectedValue = "A";
            ckProcesado.Checked = false;
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

            //if (txtNroSP.Text == "")
            //{
            //    control = "Cargue número de SP";
            //    return control;
            //}

            //if (txtComentario.InnerText == "")
            //{
            //    control = "Cargue el comentario";
            //    return control;
            //}

            return control;
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
                foreach (HttpPostedFile postedFile in fuArchivo.PostedFiles)
                {
                    string PathFisico = ConfigurationManager.AppSettings.Get("RutaFisicaDocumentos") + Session["usuario"].ToString() + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + @"\";
                    string PathVirtual = ConfigurationManager.AppSettings.Get("RutaVirtualDocumentos") + Session["usuario"].ToString() + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + @"\";

                    var FileExtension = System.IO.Path.GetExtension(postedFile.FileName).Substring(0);
                    string FileNombre = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);


                    if (pro.creaDirectorio(PathFisico))
                    {
                        string direccionArchivoFisico = PathFisico + FileNombre + " " + DateTime.Now.ToString("dd-MM-yyyy") + FileExtension;
                        string direccionArchivoVirtual = PathVirtual + FileNombre + " " + DateTime.Now.ToString("dd-MM-yyyy") + FileExtension;

                        fuArchivo.SaveAs(direccionArchivoFisico);
                        DataTable dt = ges.InsertaDatosDocumentos(txtNroOC.Text, FileNombre + " " + DateTime.Now.ToString("dd-MM-yyyy") + FileExtension, direccionArchivoFisico, direccionArchivoVirtual, "Compras", ""
                                                                        , ddlTipoDocumentoAdjunto.SelectedValue, lblActualizacion.Text);

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

                        gvDetalle.DataBind();
                        ddlTipoDocumentoAdjunto.DataBind();
                    }
                    else
                    {
                        alertError.Visible = true;
                        lblMensajeError.InnerText = "No se pudo importar archivos. Verifique";
                    }
                }
            }
            catch
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "No se pudo Importar. Verifique";
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        { 

            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblID = (Label)clickedRow.FindControl("lblID");

            ges.EliminarDatosDocumentos(Convert.ToInt32(lblID.Text));

            ddlTipoDocumentoAdjunto.DataBind();
            gvDetalle.DataBind();

        }


        protected void ckProcesado_CheckedChanged(object sender, EventArgs e)
        {
            if (ckProcesado.Checked == true)
            {
                lblProcesado.InnerText = "SI";
            }
            else
            {
                lblProcesado.InnerText = "NO";
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            alertError.Visible = false;
            alerCorrecto.Visible = false;
            var sap = "";
            var nombre = "";
            var ruc = "";
            DataTable dt = new DataTable(); 
            DatosPrincipalesSharePointModels datosPrincipal = new DatosPrincipalesSharePointModels();
            List<DatosPrincipalesSharePointModels> lista = new List<DatosPrincipalesSharePointModels>();
            try
            {
                string mensaje = controlCarga();
                if (mensaje == "OK")
                {
                    int control = ges.ModificarOrdenCompras(Convert.ToInt32(lblID.InnerText), lblProcesado.InnerText, txtNroOC.Text, ddlTipoLegajo.Text,
                               Convert.ToInt32(ddlProveedor.SelectedValue), ddlMoneda.SelectedValue, txtMontoTotal.Text, ddlEmpresas.SelectedValue, ddlSolicitante.SelectedValue,
                                   ddlSolicitante.SelectedItem.ToString(), txtNroSP.Text, ddlEstado.SelectedValue, txtComentario.InnerText, lblEsProyecto.InnerText, txtNroOrdenInterna.Text, 
                                   ddlEsDEG.SelectedValue, txtNroContratoMarco.Text, ddlDireccionado.SelectedValue);

                    if (lblProcesado.InnerText == "SI")
                    { 
                        string json = sp.getVerificaSiExisteNroOC(txtNroOC.Text, Session["usuario"].ToString(), Session["pass"].ToString());
                        datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesSharePointModels>(json);

                        lista.Add(datosPrincipal);

                        DataTable tabla = ges.GetDatosProveedor(Convert.ToInt32(ddlProveedor.SelectedValue));
                        foreach (DataRow row in tabla.Rows)
                        {
                            sap = row["provCodigoSAP"].ToString();
                            ruc = row["provRUC"].ToString();
                            nombre = row["provRazonSocial"].ToString();
                        }

                        foreach (DatosPrincipalesSharePointModels p in lista)
                        {
                            foreach (Value v in p.Values)
                            {
                                var enviarCorreo = v.DocDepartamento == "Solicitante" ? "SI" : "NO";
                                //dt = ges.RetornaDatosDocumentosParaActualizar(txtNroOC.Text, "Compras", "");
                                sp.ActualizaComprasSharePointAdjuntos(v.Id.ToString(), Session["usuario"].ToString(), Session["pass"].ToString(), txtNroOC.Text, "Procesado", txtComentario.InnerText, "Recepcion",
                                    ddlTipoLegajo.SelectedValue, sap, ruc, nombre, ddlMoneda.SelectedValue, txtMontoTotal.Text,
                                    ddlEmpresas.SelectedValue, txtNroSP.Text, txtComentario.Value, lblProcesado.InnerText, Convert.ToDateTime(lblFechaAdd.InnerText), DateTime.Now, ddlSolicitante.SelectedValue,
                                    ddlSolicitante.SelectedItem.ToString(), lblUsuCreador.InnerText, lblNombreCreador.Text, dt, lblEsProyecto.InnerText, txtNroOrdenInterna.Text,
                                    enviarCorreo, "Compras", ruc.Substring(ruc.Length - 1, 1), "Creado", ddlEsDEG.SelectedValue, txtNroContratoMarco.Text, ddlDireccionado.SelectedValue);
                            }
                        }

                        ges.updateDocumentoBanderaEsActualizacion(txtNroOC.Text);

                        lg.insertarComentario(Session["NombreApellido"].ToString(), 0, "Se proceso la OC " + txtNroOC.Text, txtNroOC.Text, "Procesar", "Procesado", "Compras", "Recepción");
                    }

                    if (control == 1)
                    {
                        lg.registrarLogs(Session["usuario"].ToString(), "ModificarOrdenCompras", "Se modifico una orden de compra Nro:" + txtNroOC.Text);

                        limpiarCampo();
                        //alerCorrecto.Visible = true;
                        //lblMensajeOK.InnerText = "Registro se guardo con exito.";
                        Response.Redirect("/UI/Workflow/Compras/Compras.aspx", false);
                    }
                    else
                    {
                        alertError.Visible = true;
                        lblMensajeError.InnerText = "Problemas para guardar registro.";
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
    }
}