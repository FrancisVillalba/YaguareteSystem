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

namespace YaguareteSystem.UI.Workflow.Recepcion
{
    public partial class ModificarRecepcion : System.Web.UI.Page
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
                lblDepartamento.Text = "Recepcion";

                DatosPrincipalesPorIdModels datosPrincipal = new DatosPrincipalesPorIdModels();
                List<DatosPrincipalesPorIdModels> lista = new List<DatosPrincipalesPorIdModels>();

                var json = sharePoint.RetornaDatosPorID(lblID.Text, Session["usuario"].ToString(), Session["pass"].ToString());
                datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesPorIdModels>(json);
                lista.Add(datosPrincipal);

                CargaFormulario(lista);
                CargaUltimoComentario(lista);
                CargaListaComentario(lista);
                CargaAdjuntos();
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Workflow/Recepcion/Recepcion.aspx");
        }
        private void CargaAdjuntos()
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblID = (Label)clickedRow.FindControl("lblID");

            ges.EliminarDatosDocumentos(Convert.ToInt32(lblID.Text));

            CargaAdjuntos();
            //sdsDocumentos.DataBind();

            lblCantidad.InnerText = this.gvDetalle.Rows.Count.ToString();
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
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
            formato.NumberDecimalSeparator = ",";

            foreach (DatosPrincipalesPorIdModels p in lista)
            {
                ddlNroOC.SelectedValue = p.Title;
                lblNroOC.Text = p.Title;
                ddlMoneda.SelectedValue = p.RecepTipoMoneda.ToString();
                txtNroFactura.Text = p.RecepNroFactura;
                ddlTipoDocumento.SelectedValue = p.RecepTipoDocumento;
                txtFechaFactura.Value = p.RecepFechaFactura.ToString().Substring(0, 10);
                if (p.RecepTimbrado != null)
                {
                    txtTimbrado.Text = p.RecepTimbrado.ToString();
                }

                txtMontoTotal.Text = p.RecepMontoFactura.ToString();
                ddlEstadoDocumento.SelectedValue = p.RecepOriginalCopia;
                lblTipoLegajo.Text = p.ComTipoLegajo.ToString();
                ddlTipoFactura.SelectedValue = p.RecepTipoFactura.ToString();
                if (p.RecepNroDespacho != null)
                {
                    txtNroDespacho.Text = p.RecepNroDespacho.ToString();
                }
                if (p.RecepFacturaAsociadaNotaCredito != null)
                {
                    txtFacturaAsociadaNotaCredito.Text = p.RecepFacturaAsociadaNotaCredito.ToString();
                }
                ddlProveedor.SelectedValue = p.RecepProveedorId.ToString();
                lblSolicitanteNombre.Text = p.ComSolicitanteNombre;
                lblCompradorNombre.Text = p.ComCreadoNombre;
                lblFechaAddFactura.Text = p.RecepFechaAddFactura.ToString();
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
                    string nombreProveedorRecepcion = "";
                    string correoProveedor = "";
                    string rucProveedorRecepcion = "";
                    var codigoSap = ""; 

                    DataTable dt = pro.retornaDatosProveedor(Convert.ToInt32(ddlProveedor.SelectedValue));
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        codigoSap = row["provCodigoSAP"].ToString();
                        nombreProveedorRecepcion = row["provRazonSocial"].ToString();
                        correoProveedor = row["provCorreo"].ToString(); 
                        if (row["provRUC"].ToString().Intersect("-").Count() > 0)
                        {
                            rucProveedorRecepcion = row["provRUC"].ToString();
                        }
                        else
                        {
                            rucProveedorRecepcion = row["provRUC"].ToString() + "-0";
                        }
                    }
                    
                    sharePoint.ActualizaMotivosRecepcion(Convert.ToInt64(lblID.Text), Session["usuario"].ToString(), Session["pass"].ToString(), txtNroFactura.Text, ddlTipoDocumento.SelectedValue,
                    Convert.ToDateTime(txtFechaFactura.Value), txtTimbrado.Text, txtMontoTotal.Text, ddlEstadoDocumento.SelectedValue, DateTime.Now, txtComentarios.Value,
                    Session["NombreApellido"].ToString(), ddlMovimiento.SelectedItem.ToString(), ddlDepartamento.SelectedValue,
                    ddlMovimiento.SelectedValue, lblTipoGestion.Text, ddlDepartamento.SelectedItem.ToString(), "SI", lblDepartamento.Text, lblTipoGestion.Text, txtNroDespacho.Text,
                    ddlTipoFactura.SelectedValue, nombreProveedorRecepcion, rucProveedorRecepcion, ddlProveedor.SelectedValue, codigoSap, ddlNroOC.SelectedValue, txtFacturaAsociadaNotaCredito.Text,
                    ddlMoneda.SelectedValue, correoProveedor);
                     
                    lg.insertarComentario(Session["NombreApellido"].ToString(), Convert.ToInt32(lblID.Text), txtComentarios.InnerText, ddlNroOC.SelectedValue, lblTipoGestion.Text, ddlMovimiento.SelectedItem.ToString(), "Recepción", ddlDepartamento.SelectedItem.ToString());
                    lg.insertarDatosReporteStatusFacturaPowerBI("Recepción", ddlDepartamento.SelectedItem.ToString(), Convert.ToInt32(lblID.Text), Session["NombreApellido"].ToString(),
                    lblTipoGestion.Text , ddlMovimiento.SelectedItem.ToString(), ddlNroOC.SelectedValue, txtNroFactura.Text, lblCompradorNombre.Text, lblSolicitanteNombre.Text, 
                    txtFechaFactura.Value, Convert.ToDateTime(lblFechaAddFactura.Text));
                     
                    limpiarCampo();
                    alerCorrectoMotivo.Visible = true;
                    lblMensajeOKMotivo.InnerText = "Registro se guardo con exito.";
                    Response.Redirect("/UI/Workflow/Recepcion/Recepcion.aspx"); 
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
            txtFechaFactura.Value = "";
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
            else
            {
                if (ddlTipoFactura.SelectedValue == "Local")
                {
                    int length = txtNroFactura.Text.Length;
                    if (length != 15)
                    {
                        control = "Verifique número de factura debe tener 15 caracteres";
                        return control;
                    }
                }
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

            if (txtFechaFactura.Value == "")
            {
                control = "Cargue la fecha";
                return control;
            }


            //if (txtTimbrado.Text == "")
            //{
            //    if (ddlTipoFactura.SelectedValue == "Local")
            //    {
            //        control = "Cargue el timbrado";
            //        return control;
            //    } 
            //}

            if (txtTimbrado.Text == "")
            {
                if (ddlTipoFactura.SelectedValue == "Local")
                {
                    control = "Cargue el timbrado";
                    return control;
                }
            }
            else
            {
                if (ddlTipoFactura.SelectedValue == "Local")
                {
                    int length = txtTimbrado.Text.Length;
                    if (length != 8)
                    {
                        control = "Verifique número de timbrado debe tener 8 caracteres";
                        return control;
                    }
                }
            }



            if (txtMontoTotal.Text == "")
            {
                control = "Cargue monto";
                return control;
            }

            if (ddlProveedor.SelectedValue == "-1")
            {
                control = "Seleccione un proveedor";
                return control;
            }

            if (ddlTipoDocumento.SelectedValue == "Local")
            {
                string cantidad = Convert.ToString(txtNroFactura.Text.Length);

                if (cantidad != "15")
                {
                    control = "Número de factura debe contener 15 caracteres";
                    return control;
                }
            }

            if (ddlTipoDocumento.SelectedValue == "Local")
            {
                string cantidad = Convert.ToString(txtTimbrado.Text.Length);

                if (cantidad != "8")
                {
                    control = "Número de timbrado debe contener 8 caracteres";
                    return control;
                }
            }

            if (lblTipoLegajo.Text == "Importacion")
            {
                if (txtNroDespacho.Text == "")
                {
                    control = "Cargue número de despacho";
                    return control;
                }
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

                            var esActualizacion = "NO";
                            ges.InsertaDatosDocumentos(ddlNroOC.SelectedValue, FileNombre + " " + DateTime.Now.ToString("dd-MM-yyyy") + FileExtension, direccionArchivoFisico,
                                direccionArchivoVirtual, "Recepcion", txtNroFactura.Text, "", esActualizacion);

                            sharePoint.CargaNuevosAdjuntosSharepoint(Convert.ToInt32(lblID.Text), Session["usuario"].ToString(), Session["pass"].ToString(), direccionArchivoFisico);

                            DatosPrincipalesPorIdModels datosPrincipal = new DatosPrincipalesPorIdModels();
                            List<DatosPrincipalesPorIdModels> lista = new List<DatosPrincipalesPorIdModels>();

                            var json = sharePoint.RetornaDatosPorID(lblID.Text, Session["usuario"].ToString(), Session["pass"].ToString());
                            datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesPorIdModels>(json);
                            lista.Add(datosPrincipal);

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
                lblMensajeError.InnerText = "Problemas: " + ex.Message;
            }
        }

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
