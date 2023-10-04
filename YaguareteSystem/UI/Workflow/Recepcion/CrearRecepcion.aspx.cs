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

namespace YaguareteSystem.UI.Workflow.Recepcion
{
    public partial class CrearRecepcion : System.Web.UI.Page
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

            if (lblControl.InnerText == "")
            {
                lblControl.InnerText = "cargado";
                lblDepartamento.Text = "Recepcion";
                lblUsuario.Text = Session["usuario"].ToString();
                lblPass.Text = Session["pass"].ToString();
            }

        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Workflow/Recepcion/Recepcion.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            alertError.Visible = false;
            alerCorrecto.Visible = false;
            DataTable dtCompras = new DataTable();
            DataTable dtRecepcion = new DataTable();
            long IdLista = 0;
            string tipoLegajo = "";
            string tipoFactura = "";
            string codigoSAP = "";
            string ruc = "";
            string nombreProveedor = "";
            string tipoMoneda = "";
            string montoTotal = "";
            string nombreEmpresa = "";
            string nroSP = "";
            string comentarioCompras = "";
            string aprobadoCompras = "";
            string usuarioSolicitante = "";
            string nombreSolicitante = "";
            string usuCreado = "";
            string nombreCreado = "";
            string nroOrdenInterna = "";
            string comProveedorDireccinado = "";
            string comEsDeg = "";
            string esProyecto = "";
            DateTime fechaRecibidoCompras = DateTime.Now;
            DateTime fechaProcesadoCompras = DateTime.Now;
            int contador = 0;
            try
            {
                string mensaje = controlCarga();
                if (mensaje == "OK")
                {
                    DatosPrincipalesSharePointModels datosPrincipal = new DatosPrincipalesSharePointModels();
                    List<DatosPrincipalesSharePointModels> lista = new List<DatosPrincipalesSharePointModels>();
                    // //Crea la tabla para cargar la grilla
                    DataTable dt = new DataTable();
                    dt.Clear();
                    dt.Columns.Add("ID");
                    dt.Columns.Add("FILTRODEPARTAMENTO");
                    dt.Columns.Add("NROOC");
                    dt.Columns.Add("NROFACTURA");
                    dt.Columns.Add("DEPARTAMENTO");
                    dt.Columns.Add("TIPODOCUMENTO");
                    dt.Columns.Add("PROVEEDOR");
                    dt.Columns.Add("ACCION");
                    dt.Columns.Add("ULTIMOCOMENTARIO");

                    var json = sharePoint.getVerificaSiExisteNroOC(ddlNroOC.SelectedValue, Session["usuario"].ToString(), Session["pass"].ToString());
                    datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesSharePointModels>(json);

                    lista.Add(datosPrincipal);
                    //List<DatosPrincipalesSharePointModels> lista = (List<DatosPrincipalesSharePointModels>)Session["listaSharepoint"];

                    foreach (DatosPrincipalesSharePointModels p in lista)
                    {
                        foreach (Value v in p.Values)
                        {
                            if (v.Title == ddlNroOC.SelectedValue)
                            {
                                if (contador == 0)
                                {
                                    //Si no existe nro factura
                                    if (v.RecepNroFactura == null)
                                    {
                                        IdLista = v.Id;
                                    }

                                    tipoLegajo = v.ComTipoLegajo;
                                    if (v.recepTipoFactura != null)
                                    {
                                        tipoFactura = v.recepTipoFactura.ToString();
                                    }

                                    codigoSAP = v.ComProveedorCodSap.ToString();
                                    ruc = v.ComProveedorRuc;
                                    nombreProveedor = v.ComProveedorNombre;
                                    tipoMoneda = v.ComTipoMoneda;
                                    montoTotal = v.ComMontoTotal;
                                    nombreEmpresa = v.ComEmpresa;
                                    nroSP = v.ComNroSp;
                                    comentarioCompras = v.ComComentarioCompras;
                                    aprobadoCompras = v.ComAprobado;
                                    fechaRecibidoCompras = Convert.ToDateTime(v.ComFechaRecibido.ToString().Substring(10));
                                    fechaProcesadoCompras = Convert.ToDateTime(v.ComFechaProcesado.ToString().Substring(10));
                                    nombreSolicitante = v.ComSolicitanteNombre;
                                    usuCreado = v.ComCreadoPor;
                                    nombreCreado = v.ComCreadoNombre;
                                    usuarioSolicitante = v.ComSolicitante;
                                    if (v.ComOrdenInterna != null)
                                    {
                                        nroOrdenInterna = v.ComOrdenInterna.ToString();
                                    }

                                    if (v.ComEsProyecto != null)
                                    {
                                        esProyecto = v.ComEsProyecto.ToString();
                                    }

                                    comProveedorDireccinado = v.ComProveedorDireccionado == null ? "NO" : v.ComProveedorDireccionado.ToString();
                                    comEsDeg = v.EsDeg == null ? "NO" : v.EsDeg.ToString();

                                    contador = contador + 1;
                                }
                            }
                        }
                    }
                    //CONTROL DE MONEDA
                    if (ddlMoneda.SelectedValue == "PYG")
                    {
                        if (txtMontoTotal.Text.Contains(".") == false)
                        {
                            alertError.Visible = true;
                            lblMensajeError.InnerText = "El monto total debe tener puntos (.) Ej: 11.111";

                            return;
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
                            alertError.Visible = true;
                            lblMensajeError.InnerText = "El monto total debe tener coma (,) Ej: 11.111,44";

                            return;
                        }
                        else
                        {
                            if (txtMontoTotal.Text.Length > 6)
                            {
                                if (txtMontoTotal.Text.Contains(".") == false)
                                {
                                    alertError.Visible = true;
                                    lblMensajeError.InnerText = "El monto total debe tener puntos (.) Ej: 11.111,44";

                                    return;
                                }
                                else
                                {
                                    alertError.Visible = false;
                                    lblMensajeError.InnerText = "";
                                }
                            }
                            else
                            {
                                alertError.Visible = false;
                                lblMensajeError.InnerText = "";
                            }
                        }
                    }

                    string nombreProveedorRecepcion = "";
                    string rucProveedorRecepcion = "";

                    DataTable dtproveedor = pro.retornaDatosProveedor(Convert.ToInt32(ddlProveedor.SelectedValue));
                    foreach (DataRow row in dtproveedor.Rows)
                    {
                        nombreProveedorRecepcion = row["provRazonSocial"].ToString();
                        if (row["provRUC"].ToString().Intersect("-").Count() > 0)
                        {
                            rucProveedorRecepcion = row["provRUC"].ToString();
                        }
                        else
                        {
                            rucProveedorRecepcion = row["provRUC"].ToString() + "-0";
                        }
                    }

                    if (IdLista == 0)
                    {
                        if (nombreEmpresa != "")
                        {
                            sharePoint.InsertSharePointComprasRecepcionAdjuntos(Session["usuario"].ToString(), Session["pass"].ToString(), ddlNroOC.SelectedValue, "NoProcesado", "Recepcion", tipoLegajo, codigoSAP,
                            ruc, nombreProveedor, tipoMoneda, montoTotal, nombreEmpresa, nroSP, comentarioCompras, aprobadoCompras, fechaRecibidoCompras, fechaProcesadoCompras, usuarioSolicitante,
                            nombreSolicitante, usuCreado, nombreCreado, txtNroFactura.Text, ddlTipoDocumento.SelectedValue, Convert.ToDateTime(txtFechaFactura.Value), txtTimbrado.Text,
                            txtMontoTotal.Text, ddlEstadoDocumento.SelectedValue, DateTime.Now, txtComentario.Value, Session["NombreApellido"].ToString(),
                            nroOrdenInterna, esProyecto, "NO", "Recepción", "Recepción", ruc.Substring(ruc.Length - 1, 1), "Pendiente", ddlTipoFactura.SelectedValue, ddlMoneda.SelectedValue
                            , rucProveedorRecepcion, nombreProveedorRecepcion, ddlProveedor.SelectedValue, txtFacturaAsociadaNotaCredito.Text, comProveedorDireccinado, comEsDeg);
                        }
                        else
                        {
                            DataTable dtoc = ges.RetornaDatosOrdenComprasXoc(ddlNroOC.SelectedValue);

                            foreach (DataRow row in dtoc.Rows)
                            {
                                sharePoint.InsertSharePointComprasRecepcionAdjuntos(Session["usuario"].ToString(), Session["pass"].ToString(), ddlNroOC.SelectedValue, "NoProcesado", "Recepcion",
                                row["oComTipoDocumentoID"].ToString(), row["provCodigoSAP"].ToString(),
                                row["provRUC"].ToString(), row["provRazonSocial"].ToString(), row["oComTipoMonedaID"].ToString(), row["oComMontoTotal"].ToString(), row["oComCodigoEmpresa"].ToString(),
                                row["oComNroSP"].ToString(), row["oComComentario"].ToString(), row["oComNombreCreador"].ToString(), Convert.ToDateTime(row["oComFechaAdd"].ToString()), Convert.ToDateTime(row["oComFechaAdd"].ToString())
                                , row["oComUsuarioSolicitante"].ToString(),
                                row["oComNombreSolicitante"].ToString(), row["oComUsuarioCreador"].ToString(), row["oComNombreCreador"].ToString(), txtNroFactura.Text, ddlTipoDocumento.SelectedValue, Convert.ToDateTime(txtFechaFactura.Value), txtTimbrado.Text,
                                txtMontoTotal.Text, ddlEstadoDocumento.SelectedValue, DateTime.Now, txtComentario.Value, Session["NombreApellido"].ToString(),
                                row["oComNroOrdenInterna"].ToString(), row["oComEsProyecto"].ToString(), "NO", "Recepción", "Recepción", row["provRUC"].ToString().Substring(row["provRUC"].ToString().Length - 1, 1), "Pendiente", ddlTipoFactura.SelectedValue, ddlMoneda.SelectedValue
                                , rucProveedorRecepcion, nombreProveedorRecepcion, ddlProveedor.SelectedValue, txtFacturaAsociadaNotaCredito.Text, row["provEsDireccinado"].ToString(),
                                row["esDeg"].ToString());
                            }
                        }


                        List<IdMaximoModel> listaId = new List<IdMaximoModel>();
                        var idJson = sharePoint.RetornaIdMaximoSharePoint(Session["usuario"].ToString(), Session["pass"].ToString());
                        var idModel = JsonConvert.DeserializeObject<IdMaximoModel>(idJson);
                        listaId.Add(idModel);

                        foreach (IdMaximoModel p in listaId)
                        {
                            foreach (ValueId v in p.value)
                            {
                                lg.insertarComentario(Session["NombreApellido"].ToString(), Convert.ToInt32(v.ID), "Se cargo la factura número: " + txtNroFactura.Text, ddlNroOC.SelectedValue, "Carga", "Carga factura", "Recepción", "Recepción");
                                lg.insertarDatosReporteStatusFacturaPowerBI("Recepción", "", Convert.ToInt32(v.ID), Session["NombreApellido"].ToString(),
                                    "Carga", "Carga factura", ddlNroOC.SelectedValue, txtNroFactura.Text, nombreCreado, nombreSolicitante, txtFechaFactura.Value, DateTime.Now);
                            }
                        }
                    }
                    else
                    {
                        //DataTable dtt = ges.RetornaDatosDocumentos(ddlNroOC.SelectedValue, "Recepcion", txtNroFactura.Text);
                        sharePoint.ActualizarRecepcionSharepointAjuntos(IdLista, Session["usuario"].ToString(), Session["pass"].ToString(), txtNroFactura.Text, ddlTipoDocumento.SelectedValue,
                                    Convert.ToDateTime(txtFechaFactura.Value), txtTimbrado.Text, txtMontoTotal.Text, ddlEstadoDocumento.SelectedValue, DateTime.Now,
                                    txtComentario.Value, Session["usuario"].ToString(), Session["NombreApellido"].ToString(), "NoProcesado", "Recepcion", "NO", "Recepción", "Recepción",
                                    ruc.Substring(ruc.Length - 1, 1), "Pendiente", ddlTipoFactura.SelectedValue, ddlMoneda.SelectedValue, nombreProveedorRecepcion, rucProveedorRecepcion,
                                    ddlProveedor.SelectedValue, txtFacturaAsociadaNotaCredito.Text);

                        lg.insertarComentario(Session["NombreApellido"].ToString(), Convert.ToInt32(IdLista), "Se cargo la factura número: " + txtNroFactura.Text, ddlNroOC.SelectedValue, "Carga", "Carga factura", "Recepción", "Recepción");
                        lg.insertarDatosReporteStatusFacturaPowerBI("Recepción", "", Convert.ToInt32(IdLista), Session["NombreApellido"].ToString(),
                                    "Carga", "Carga factura", ddlNroOC.SelectedValue, txtNroFactura.Text, nombreCreado, nombreSolicitante, txtFechaFactura.Value, DateTime.Now);
                        ges.ActualizaUsoRecepcion(ddlNroOC.SelectedValue);


                    }



                    limpiarCampo();
                    //alerCorrecto.Visible = true;
                    //lblMensajeOK.InnerText = "Registro se guardo con exito.";
                    Response.Redirect("/UI/Workflow/Recepcion/Recepcion.aspx");
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
            txtNroFactura.Text = "";
            ddlTipoDocumento.SelectedValue = "-1";
            ddlNroOC.SelectedValue = "-1";
            txtTimbrado.Text = "";
            txtMontoTotal.Text = "";
            txtFechaFactura.Value = "";
            txtComentario.InnerText = "";
            ddlNroOC.SelectedValue = "-1";
            ddlEstadoDocumento.SelectedValue = "Original";
            ddlTipoFactura.SelectedValue = "Local";
            ddlMoneda.SelectedValue = "-1";
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
            else if (ddlTipoDocumento.SelectedValue == "NotaCredito")
            {
                if (txtFacturaAsociadaNotaCredito.Text == "")
                {
                    txtFacturaAsociadaNotaCredito.Visible = true;
                    control = "Cargue número de factura asociada a la nota de crédito.";
                    return control;
                }
            }

            if (txtFechaFactura.Value == "")
            {
                control = "Cargue la fecha";
                return control;
            }
            else
            {
                var fechaCargada = Convert.ToDateTime(txtFechaFactura.Value);
                var fechaActual = DateTime.Now;
                var respuesta = (fechaActual - fechaCargada).Days;

                if (respuesta > 1588886)
                {
                    control = "La fecha de la factura supera los 7 días de recepción";
                    return control;
                }
            }

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

            if (ddlMoneda.Text == "-1")
            {
                control = "Seleccione el tipo de moneda";
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

                            gvDetalle.DataBind();
                            sdsDocumentos.DataBind();
                            lblCantidad.InnerText = this.gvDetalle.Rows.Count.ToString();

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

            ges.EliminarDatosDocumentos(Convert.ToInt32(lblID.Text));

            gvDetalle.DataBind();
            sdsDocumentos.DataBind();

            lblCantidad.InnerText = this.gvDetalle.Rows.Count.ToString();
        }
        //protected void ddlNroOC_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    gvDetalle.DataBind();
        //    sdsDocumentos.DataBind();

        //    lblCantidad.InnerText = this.gvDetalle.Rows.Count.ToString();
        //}

        protected void btnAltaProveedores_Click(object sender, EventArgs e)
        {
            if (controlaCampos())
            {
                DataTable dt = ges.InsertaProveedorRecepcion(txtRuc.Value, txtRazonSocial.Value);

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
                            //sdsProveedorRecepcion.DataBind();
                            ddlProveedor.SelectedValue = row["ID"].ToString();
                        }
                        limpiarCampos();
                    }
                }
            }
        }
        private bool controlaCampos()
        {

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
            txtRazonSocial.Value = "";
            txtRuc.Value = "";
        }

        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoDocumento.SelectedValue == "FacturaCredito")
            {
                txtFacturaAsociadaNotaCredito.Visible = true;
            }
        }
    }
}