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
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;

namespace YaguareteSystem.UI.Workflow.CuentaPagar
{
    public partial class ModificarCuentaPagar : System.Web.UI.Page
    {
        LogsBLL lg = new LogsBLL();
        GestionDocumentosBLL ges = new GestionDocumentosBLL();
        ProcesosBLL pro = new ProcesosBLL();
        SharePointBLL sharePoint = new SharePointBLL();
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
                lblControl.InnerText = "cargado";
                lblID.Text = Page.Request.QueryString["ID"].ToString();
                lblDepartamento.Text = "CuentaPagar";
                lblUsuario.Text = Session["usuario"].ToString();
                lblPass.Text = Session["pass"].ToString();

                DatosPrincipalesPorIdModels datosPrincipal = new DatosPrincipalesPorIdModels();
                List<DatosPrincipalesPorIdModels> lista = new List<DatosPrincipalesPorIdModels>();

                var json = sharePoint.RetornaDatosPorID(lblID.Text, Session["usuario"].ToString(), Session["pass"].ToString());
                datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesPorIdModels>(json);
                lista.Add(datosPrincipal);

                CargaFormulario(lista);
                CargaUltimoComentario(lista);
                CargaListaComentario(lista);
                CargaAdjuntos(lista);
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Workflow/CuentaPagar/CuentaPagar.aspx");
        }
        private void CargaAdjuntos(List<DatosPrincipalesPorIdModels> lista)
        {
            //DataTable dtDocumentos = new DataTable();
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
            // //Crea la tabla para cargar la grilla
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
                txtNroFactura.Text = p.RecepNroFactura;
                ddlTipoDocumento.SelectedValue = p.RecepTipoDocumento;
                ddlTipoFactura.SelectedValue = p.RecepTipoFactura.ToString();
                txtFechaFactura.Text = p.RecepFechaFactura.ToString().Substring(0, 10);
                txtTimbrado.Text = p.RecepTimbrado.ToString();
                txtMontoTotal.Text = p.RecepMontoFactura.ToString();
                ddlMoneda.SelectedValue = p.RecepTipoMoneda.ToString();
                ddlEstadoDocumento.SelectedValue = p.RecepOriginalCopia;
                if (p.CuentaPagarNroAsiento != null)
                {
                    txtNroAsiento.Text = p.CuentaPagarNroAsiento.ToString();
                }

                if (p.DocDepartamento != "CuentaPagar")
                {
                    btnContinuar.Enabled = false;
                    btnDevolver.Enabled = false;
                    btnRechazar.Enabled = false;
                }
                if (p.RecepFacturaAsociadaNotaCredito != null)
                {
                    txtFacturaAsociadaNotaCredito.Text = p.RecepFacturaAsociadaNotaCredito.ToString();
                }
                ddlProveedor.SelectedValue = p.RecepProveedorId.ToString();
                lblSolicitanteNombre.Text = p.ComSolicitanteNombre;
                lblCompradorNombre.Text = p.ComCreadoNombre;
                lblFechaFactura.Text = p.RecepFechaFactura.ToString();
                lblFechaAddFactura.Text = p.RecepFechaAddFactura == null ? DateTime.Now.ToString() : p.RecepFechaAddFactura.ToString();
                lblEmpresa.Text = p.ComEmpresa.ToString();
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            alertError.Visible = false;
            alerCorrecto.Visible = false;

            try
            {
                string mensaje = controlCargaMotivos();
                if (mensaje == "OK")
                {
                    string split = ddlProveedor.SelectedItem.ToString();
                    List<string> list = new List<string>();
                    list = split.Split('/').ToList();

                    sharePoint.ActualizaMotivosCuentaPagar(Convert.ToInt64(lblID.Text), Session["usuario"].ToString(), Session["pass"].ToString(), txtComentarios.Value,
                      Session["NombreApellido"].ToString(), ddlMovimiento.SelectedItem.ToString(), ddlDepartamento.SelectedValue, ddlMovimiento.SelectedValue,
                      txtNroAsiento.Text, ddlDepartamento.SelectedItem.ToString(), "SI", "Cuentas a pagar", lblTipoGestion.Text);

                    lg.insertarComentario(Session["NombreApellido"].ToString(), Convert.ToInt32(lblID.Text), txtComentarios.InnerText, ddlNroOC.SelectedValue, lblTipoGestion.Text, ddlMovimiento.SelectedItem.ToString(), "Cuenta a pagar", ddlDepartamento.SelectedItem.ToString());

                    lg.insertarDatosReporteStatusFacturaPowerBI("Cuenta a pagar", ddlDepartamento.SelectedItem.ToString(), Convert.ToInt32(lblID.Text), Session["NombreApellido"].ToString(),
                    lblTipoGestion.Text, ddlMovimiento.SelectedItem.ToString(), ddlNroOC.SelectedValue, txtNroFactura.Text, lblCompradorNombre.Text, lblSolicitanteNombre.Text,
                    lblFechaFactura.Text, Convert.ToDateTime(lblFechaAddFactura.Text));

                    limpiarCampo();
                    alerCorrectoMotivo.Visible = true;
                    lblMensajeOKMotivo.InnerText = "Registro se guardo con exito.";
                    Response.Redirect("/UI/Workflow/CuentaPagar/CuentaPagar.aspx");

                }
                else
                {
                    alertErrorMotivo.Visible = true;
                    lblMensajeErrorMotivo.InnerText = mensaje;
                }
            }
            catch (Exception ex)
            {
                alertErrorMotivo.Visible = true;
                lblMensajeErrorMotivo.InnerText = "Problemas para guardar registro." + ex.Message.ToString();
            }

        }
        private void limpiarCampo()
        {
            txtNroFactura.Text = "";
            ddlTipoDocumento.SelectedValue = "-1";
            ddlNroOC.SelectedValue = "-1";
            txtTimbrado.Text = "";
            txtMontoTotal.Text = "";
            txtFechaFactura.Text = "";
            txtComentarios.InnerText = "";
            ddlNroOC.SelectedValue = "-1";
            ddlEstadoDocumento.SelectedValue = "Original";
        }
        private string controlCarga()
        {
            string control = "OK";

            if (txtNroFactura.Text == "")
            {
                control = "Cargue número de factura";
                return control;
            }

            if (ddlNroOC.SelectedValue == "-1")
            {
                control = "Seleccion número de orden de compras";
                return control;
            }

            if (ddlTipoDocumento.SelectedValue == "-1")
            {
                control = "Seleccione tipo de documento";
                return control;
            }


            if (txtFechaFactura.Text == "")
            {
                control = "Cargue la fecha";
                return control;
            }


            if (txtTimbrado.Text == "")
            {
                control = "Cargue el timbrado";
                return control;
            }

            if (txtMontoTotal.Text == "")
            {
                control = "Cargue monto";
                return control;
            } 

            return control;
        }

        private string controlCargaMotivos()
        {
            string control = "OK";

            if (ddlMovimiento.SelectedValue == "-1")
            {
                control = "Seleccion un motivo";
                return control;
            }

            if (ddlDepartamento.SelectedValue == "NO")
            {
                control = "No puede procesar esta factura porque no cuenta con un departamento";
                return control;
            }


            //if (txtComentarios.Value == "")
            //{
            //    control = "No puede procesar esta factura porque no cuenta con un motivo";
            //    return control;
            //}
            return control;
        }
        //protected void btnImportaDatos_Click(object sender, EventArgs e)
        //{

        //    if (!fuArchivo.HasFile)
        //    {
        //        alertError.Visible = true;
        //        lblMensajeError.InnerText = "Campo vacio";
        //    }
        //    else
        //    {

        //        ImportaArchivo();
        //    }
        //}
        //private void ImportaArchivo()
        //{
        //    DataTable dr = new DataTable();

        //    try
        //    {
        //        if (ddlNroOC.SelectedValue != "-1")
        //        {
        //            foreach (HttpPostedFile postedFile in fuArchivo.PostedFiles)
        //            {
        //                string PathFisico = ConfigurationManager.AppSettings.Get("RutaFisicaDocumentos") + Session["usuario"].ToString() + @"\";
        //                string PathVirtual = ConfigurationManager.AppSettings.Get("RutaVirtualDocumentos") + Session["usuario"].ToString() + @"/";

        //                if (pro.creaDirectorio(PathFisico))
        //                {
        //                    string direccionArchivoFisico = PathFisico + postedFile.FileName;
        //                    string direccionArchivoVirtual = PathVirtual + postedFile.FileName;

        //                    fuArchivo.SaveAs(direccionArchivoFisico);

        //                    ges.InsertaDatosDocumentos(ddlNroOC.SelectedValue, postedFile.FileName, direccionArchivoFisico, direccionArchivoVirtual, "Recepcion", txtNroFactura.Text, "");

        //                    sharePoint.CargaNuevosAdjuntosSharepoint(Convert.ToInt32(lblID.Text), Session["usuario"].ToString(), Session["pass"].ToString(), direccionArchivoFisico);

        //                    DatosPrincipalesPorIdModels datosPrincipal = new DatosPrincipalesPorIdModels();
        //                    List<DatosPrincipalesPorIdModels> lista = new List<DatosPrincipalesPorIdModels>();

        //                    var json = sharePoint.RetornaDatosPorID(lblID.Text, Session["usuario"].ToString(), Session["pass"].ToString());
        //                    datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesPorIdModels>(json);
        //                    lista.Add(datosPrincipal);

        //                    CargaAdjuntos(lista);

        //                    //lblCantidad.InnerText = this.gvDetalle.Rows.Count.ToString();

        //                }
        //                else
        //                {
        //                    alertError.Visible = true;
        //                    lblMensajeError.InnerText = "No se pudo importar archivos. Verifique";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            alertError.Visible = true;
        //            lblMensajeError.InnerText = "Debe cargar número de Orden de compras para suber archivos";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        alertError.Visible = true;
        //        lblMensajeError.InnerText = "Problemas: " + ex.Message;
        //    }
        //}  

        protected void btnDevolver_Click(object sender, EventArgs e)
        {
            string mensaje = controlCarga();
            if (mensaje == "OK")
            {
                lblTipoGestion.Text = "Devuelto";
                pnlDocumentos.Visible = false;
                pnlGestion.Visible = true;
            }
            else
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = mensaje;
            }
        }



        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            string mensaje = controlCarga();
            if (mensaje == "OK")
            {
                //if (txtNroAsiento.Text == "")
                //{
                //    alertError.Visible = true;
                //    lblMensajeError.InnerText = "Cargue número de asiento para continuar";

                //    return;
                //}

                if (txtNroAsiento.Text == "")
                {
                    //control = "Cargue número de asiento";
                    //return control;
                    alertError.Visible = true;
                    lblMensajeError.InnerText = "Cargue número de asiento";

                    return;
                }
                else
                {
                    var respuesta = sharePoint.VerificaSiExisteNroAsientoCuentaPagar(txtNroAsiento.Text, lblEmpresa.Text, lblUsuario.Text, lblPass.Text, Convert.ToDateTime(lblFechaAddFactura.Text));
                    if (respuesta.estado == true)
                    {
                        alertError.Visible = true;
                        lblMensajeError.InnerText = "Número de asiento ya se encuentra cargado";
                        //control = "Número de asiento ya se encuentra cargado";
                        //return control;

                        return;
                    }

                }

                lblTipoGestion.Text = "Enviado";
                pnlDocumentos.Visible = false;
                pnlGestion.Visible = true;
            }
            else
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = mensaje;

            }
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            string mensaje = controlCarga();
            if (mensaje == "OK")
            {
                lblTipoGestion.Text = "Rechazado";
                pnlDocumentos.Visible = false;
                pnlGestion.Visible = true;
            }
            else
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = mensaje;

            }
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMovimiento.DataBind();
            sdsMovimentos.DataBind();
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            pnlDocumentos.Visible = true;
            pnlGestion.Visible = false;
        }
    }
}
