using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;

namespace YaguareteSystem.UI.Workflow.Compras
{
    public partial class ImportarArchivosDespachante : System.Web.UI.Page
    {
        SharePointBLL sharePoint = new SharePointBLL();
        ProcesosBLL pro = new ProcesosBLL();
        GestionDocumentosBLL ges = new GestionDocumentosBLL();
        LogsBLL lg = new LogsBLL();
        BackgroundWorker tarea = new BackgroundWorker();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("/Default.aspx");
            }

            if (lblCargado.Text == "")
            {
                lblCargado.Text = "CARGADO";
                lblUsuario.Text = Session["usuario"].ToString();
                lblPass.Text = Session["pass"].ToString();
                lblNombreUsuario.Text = Session["NombreApellido"].ToString();
            }

            lblDatoAdjunto.Visible = false;
        }

        protected void btnImportaDatos_Click(object sender, EventArgs e)
        {
            alertError.Visible = false;
            alerCorrecto.Visible = false;

            if (!fuArchivo.HasFile)
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Campo vacio";
            }
            else
            {
                //tarea.DoWork += ImportaArchivo;
                //tarea.RunWorkerAsync();
                ImportaArchivo();
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Workflow/Compras/Compras.aspx");
        }
        //private void ImportaArchivo(object o, DoWorkEventArgs e)
        private void ImportaArchivo()
        {
            DataTable dr = new DataTable();
            alertError.Visible = false;
            alerCorrecto.Visible = false;
            //string url, json = "";
            int contador = 1;
            string listaContador = "";
            ///DatosPrincipalesSharePointModels datosPrincipal = new DatosPrincipalesSharePointModels();

            string FileName = System.IO.Path.GetFileName(fuArchivo.PostedFile.FileName);
            lblExtension.Text = System.IO.Path.GetExtension(fuArchivo.PostedFile.FileName);
            if (lblExtension.Text == ".csv" || lblExtension.Text == ".CSV")
            {
                try
                {
                    string FolderPath = ConfigurationManager.AppSettings.Get("RutaComprasImportacion");
                    if (pro.creaDirectorio(FolderPath))
                    {
                        lblDirArchivo.Text = FolderPath + FileName;
                        fuArchivo.SaveAs(FolderPath + FileName);
                        dr = pro.importarTxtCamp(lblDirArchivo.Text);
                        if (dr.Rows.Count > 100)
                        {
                            alertError.Visible = true;
                            lblMensajeError.InnerText = "Solo puede importar hasta 100 Registros. Verifique el archivo";
                        }
                        else
                        {
                            foreach (DataRow rows in dr.Rows)
                            {
                                try
                                {
                                    contador += 1;
                                    var table = ges.RetornaDatosOrdenComprasXoc(rows["OC_NUM"].ToString());
                                    foreach (DataRow row in table.Rows)
                                    {
                                        DatosPrincipalesSharePointModels datosPrincipal = new DatosPrincipalesSharePointModels();
                                        List<DatosPrincipalesSharePointModels> lista = new List<DatosPrincipalesSharePointModels>();

                                        var tipoDocuemnto = rows["TIPO_DOCUMENTO"].ToString() == "CONTADO" ? "Factura contado" : "Factura crédito";
                                        var tipoDocuemntoID = rows["TIPO_DOCUMENTO"].ToString() == "CONTADO" ? "FacturaContado" : "FacturaCredito";

                                        var json = sharePoint.controlImportarSiFacturaExisteOC(row["provRUC"].ToString(), row["oComNroOC"].ToString(), rows["NUM_FACTURA"].ToString(), Session["usuario"].ToString(), Session["pass"].ToString());
                                        datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesSharePointModels>(json);

                                        if (datosPrincipal.Values.Length == 0)
                                        {
                                            var tipoFactura = rows["TIPO_FACTURA"].ToString() == "LOCAL" ? "Local" : "Importacion";
                                            var rucs = new DatosProveedoresModels();


                                            if (tipoFactura == "Local")
                                            {
                                                rucs = pro.retornaDatosProveedorPorRuc(rows["RUC"].ToString());
                                            }
                                            else
                                            {
                                                rucs.provCodigoSap = row["provCodigoSAP"].ToString();
                                                rucs.provId = row["provID"].ToString();
                                                rucs.provRazonSocial = row["provRazonSocial"].ToString();
                                                rucs.provRuc = row["provRUC"].ToString();
                                            }

                                            var control = sharePoint.verificaSiExisteOcEnRecepcionSinFacturaCargada(rows["OC_NUM"].ToString(), Session["usuario"].ToString(), Session["pass"].ToString());

                                            if (control.estado)
                                            {
                                                sharePoint.updateSharePointComprasParaImpuestos(control.IdSharePoint, Session["usuario"].ToString(), Session["pass"].ToString(), row["oComNroOC"].ToString(),
                                                                                                 "Importado compras", "Impuestos", row["oComTipoDocumentoID"].ToString(), row["provCodigoSAP"].ToString(), row["provRUC"].ToString(),
                                                                                                 row["provRazonSocial"].ToString(), row["oComTipoMonedaID"].ToString().Replace(" ", ""), row["oComMontoTotal"].ToString(), row["oComCodigoEmpresa"].ToString(),
                                                                                                 row["oComNroSP"].ToString(), row["oComComentario"].ToString(), row["oComNombreCreador"].ToString(), Convert.ToDateTime(row["oComFechaAdd"].ToString()), Convert.ToDateTime(row["oComFechaAdd"].ToString()),
                                                                                                 row["oComUsuarioSolicitante"].ToString(), row["oComNombreSolicitante"].ToString(), row["oComUsuarioCreador"].ToString(),
                                                                                                 row["oComNombreCreador"].ToString(), rows["NUM_FACTURA"].ToString(), tipoDocuemntoID, Convert.ToDateTime(rows["FECHA_FACTURA"].ToString()),
                                                                                                 rows["TIMBRADO"].ToString(), rows["MONTO_FACTURA"].ToString(), "Original", DateTime.Now, "Sin comentarios", Session["NombreApellido"].ToString(),
                                                                                                 row["oComNroOrdenInterna"].ToString(), row["oComEsProyecto"].ToString(), "NO", "Compras", "Impuestos",
                                                                                                 row["provRUC"].ToString().Substring(row["provRUC"].ToString().Length - 1, 1), "Enviado",
                                                                                                 tipoFactura, rows["TIPO_MONEDA"].ToString().Replace(" ", ""),
                                                                                                 rucs.provRuc,
                                                                                                 rucs.provRazonSocial,
                                                                                                 rucs.provId,
                                                                                                 "",
                                                                                                 rucs.provCodigoSap,
                                                                                                 row["provEsDireccinado"].ToString(),
                                                                                                 row["esDeg"].ToString(), tipoDocuemnto, Session["NombreApellido"].ToString());

                                                lg.insertarComentario(Session["NombreApellido"].ToString(), Convert.ToInt32(control.IdSharePoint), "Se migro desde compras la factura número: " + rows["NUM_FACTURA"].ToString(),
                                                          row["oComNroOC"].ToString(), "Carga", "Importado compras", "Compras", "Impuestos");
                                                lg.insertarDatosReporteStatusFacturaPowerBI("Impuestos", "", Convert.ToInt32(control.IdSharePoint), Session["NombreApellido"].ToString(),
                                                    "Carga", "Se migro desde compras la factura número: " + rows["NUM_FACTURA"].ToString(), row["oComNroOC"].ToString(), rows["NUM_FACTURA"].ToString(), Session["NombreApellido"].ToString(),
                                                    row["oComNombreSolicitante"].ToString(), rows["FECHA_FACTURA"].ToString(), DateTime.Now);
                                            }
                                            else
                                            {
                                                sharePoint.InsertSharePointComprasParaImpuestos(Session["usuario"].ToString(), Session["pass"].ToString(), row["oComNroOC"].ToString(),
                                                                                                 "Importado compras", "Impuestos", row["oComTipoDocumentoID"].ToString(), row["provCodigoSAP"].ToString(), row["provRUC"].ToString(),
                                                                                                 row["provRazonSocial"].ToString(), row["oComTipoMonedaID"].ToString().Replace(" ", ""), row["oComMontoTotal"].ToString(), row["oComCodigoEmpresa"].ToString(),
                                                                                                 row["oComNroSP"].ToString(), row["oComComentario"].ToString(), row["oComNombreCreador"].ToString(), Convert.ToDateTime(row["oComFechaAdd"].ToString()), Convert.ToDateTime(row["oComFechaAdd"].ToString()),
                                                                                                 row["oComUsuarioSolicitante"].ToString(), row["oComNombreSolicitante"].ToString(), row["oComUsuarioCreador"].ToString(),
                                                                                                 row["oComNombreCreador"].ToString(), rows["NUM_FACTURA"].ToString(), tipoDocuemntoID, Convert.ToDateTime(rows["FECHA_FACTURA"].ToString()),
                                                                                                 rows["TIMBRADO"].ToString(), rows["MONTO_FACTURA"].ToString(), "Original", DateTime.Now, "Sin comentarios", Session["NombreApellido"].ToString(),
                                                                                                 row["oComNroOrdenInterna"].ToString(), row["oComEsProyecto"].ToString(), "NO", "Compras", "Impuestos",
                                                                                                 row["provRUC"].ToString().Substring(row["provRUC"].ToString().Length - 1, 1), "Enviado",
                                                                                                 tipoFactura, rows["TIPO_MONEDA"].ToString().Replace(" ", ""),
                                                                                                 rucs.provRuc,
                                                                                                 rucs.provRazonSocial,
                                                                                                 rucs.provId,
                                                                                                 "",
                                                                                                 rucs.provCodigoSap,
                                                                                                 row["provEsDireccinado"].ToString(),
                                                                                                 row["esDeg"].ToString(), tipoDocuemnto, Session["NombreApellido"].ToString());

                                                List<IdMaximoModel> listaId = new List<IdMaximoModel>();
                                                var idJson = sharePoint.RetornaIdMaximoSharePoint(Session["usuario"].ToString(), Session["pass"].ToString());
                                                var idModel = JsonConvert.DeserializeObject<IdMaximoModel>(idJson);
                                                listaId.Add(idModel);

                                                foreach (IdMaximoModel p in listaId)
                                                {
                                                    foreach (ValueId v in p.value)
                                                    {
                                                        lg.insertarComentario(Session["NombreApellido"].ToString(), Convert.ToInt32(v.ID), "Se migro desde compras la factura número: " + rows["NUM_FACTURA"].ToString(),
                                                            row["oComNroOC"].ToString(), "Carga", "Importado compras", "Compras", "Impuestos");
                                                        lg.insertarDatosReporteStatusFacturaPowerBI("Impuestos", "", Convert.ToInt32(v.ID), Session["NombreApellido"].ToString(),
                                                            "Carga", "Se migro desde compras la factura número: " + rows["NUM_FACTURA"].ToString(), row["oComNroOC"].ToString(), rows["NUM_FACTURA"].ToString(), Session["NombreApellido"].ToString(),
                                                            row["oComNombreSolicitante"].ToString(), rows["FECHA_FACTURA"].ToString(), DateTime.Now);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    listaContador = listaContador + contador + ", ";
                                }
                            }

                            if (listaContador != "")
                            {
                                alertError.Visible = true;
                                alerCorrecto.Visible = false;
                                lblMensajeError.InnerText = "Problemas para importar registro en las lineas: " + listaContador;
                            }
                            else
                            {
                                alerCorrecto.Visible = true;
                                alertError.Visible = false;
                                lblMensajeOK.InnerText = "Registros importados correctamente";
                            }
                        }
                    }

                    else
                    {
                        alertError.Visible = true;
                        lblMensajeError.InnerText = "No se pudo importar registros. Verifique";
                    }

                }
                catch (Exception ex)
                {
                    alertError.Visible = true;
                    lblMensajeError.InnerText = "Problemas al importar registros";
                }
            }
            else
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Archivo No Valido. Verifique";
            }
        }
    }
}