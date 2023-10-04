using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using YaguareteSystem.Models;

namespace YaguareteSystem.Clases
{
    public class SharePointBLL
    {
        public class MyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest request = base.GetWebRequest(address);

                if (request is HttpWebRequest)
                {
                    var myWebRequest = request as HttpWebRequest;
                    myWebRequest.UnsafeAuthenticatedConnectionSharing = true;
                    myWebRequest.KeepAlive = true;
                }

                return request;
            }
        }

        public void InsertSharePointComprasRecepcionAdjuntos(string usu, string pass, string nroOc, string aprobadoRecepcion, string departamento, string tipoLegajo,
                   string codigoSAP, string ruc, string nombreProveedor, string tipoMoneda, string montoTotal, string nombreEmpresa, string nroSP, string comentarioCompras,
                   string aprobadoCompras, DateTime fechaRecibido, DateTime fechaProcesadoCompras, string usuarioSolicitante, string nombreSolicitante, string usuCreado, string nombreUsuario,
                   string nroFactura, string tipoDocumento, DateTime fechaFactura, string timbrado, string montoFactura, string estadoDocumento, DateTime fechaProcesadoRecepcion,
                   string comentarioRecepcion, string usuRecepcionNombre, string nroOrdenInterna, string esProyecto, string enviarCorreo,
                   string nombreDepartamentoAnterior, string nombreDepartamento, string cv, string tipoMovimiento, string tipoFactura, string recepTipoMoeda,
                   string rucProveedorRecepcion, string nombreProveedorRecepcion, string idProveedorRecepcion, string facturaAsociadaNotaCredito, string proveedorDireccionado,
                   string esDeg)
        {

            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");

            try
            {
                using (ClientContext clientContext = new ClientContext(URL_status))
                {
                    // SharePoint Online Credentials    
                    //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                    clientContext.Credentials = new NetworkCredential(usu, pass);
                    Web web = clientContext.Web;

                    clientContext.Load(web);
                    clientContext.ExecuteQuery();

                    Web myweb = clientContext.Web;
                    List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem oListItem = oList.AddItem(itemCreateInfo);
                    oListItem["Title"] = nroOc;
                    oListItem["docUltimaAccion"] = aprobadoRecepcion;
                    oListItem["docUltimoComentario"] = comentarioRecepcion;
                    oListItem["docIdUltimaAccion"] = "27";
                    oListItem["docNombreDepartamento"] = nombreDepartamento;
                    oListItem["docDepartamentoAnterior"] = nombreDepartamentoAnterior;
                    oListItem["docDepartamento"] = departamento;
                    oListItem["comTipoLegajo"] = tipoLegajo;
                    oListItem["comProveedorCodSap"] = codigoSAP;
                    oListItem["comProveedorRuc"] = ruc;
                    oListItem["comProveedorNombre"] = nombreProveedor;
                    oListItem["comTipoMoneda"] = tipoMoneda;
                    oListItem["comMontoTotal"] = montoTotal;
                    oListItem["comEmpresa"] = nombreEmpresa;
                    oListItem["comNroSP"] = nroSP;
                    oListItem["comComentarioCompras"] = comentarioCompras;
                    oListItem["comAprobado"] = aprobadoCompras;
                    oListItem["comFechaRecibido"] = fechaRecibido;
                    oListItem["comFechaProcesado"] = fechaProcesadoCompras;
                    oListItem["comSolicitante"] = usuarioSolicitante;
                    oListItem["comSolicitanteNombre"] = nombreSolicitante;
                    oListItem["comCreadoPor"] = usuCreado;
                    oListItem["comCreadoNombre"] = nombreUsuario;
                    oListItem["recepNroFactura"] = nroFactura;
                    oListItem["recepTipoDocumento"] = tipoDocumento;
                    oListItem["recepFechaFactura"] = fechaFactura;
                    oListItem["recepFacturaAsociadaNotaCredito"] = facturaAsociadaNotaCredito;
                    if (timbrado == "")
                    {
                        oListItem["recepTimbrado"] = "0";
                    }
                    else
                    {
                        oListItem["recepTimbrado"] = timbrado;
                    }
                    oListItem["recepTipoMoneda"] = recepTipoMoeda;
                    oListItem["recepMontoFactura"] = montoFactura;
                    oListItem["recepProveedorNombre"] = nombreProveedorRecepcion;
                    oListItem["recepProveedorId"] = idProveedorRecepcion;
                    oListItem["recepProveedorRuc"] = rucProveedorRecepcion;
                    oListItem["recepTipoFactura"] = tipoFactura;
                    oListItem["recepOriginalCopia"] = estadoDocumento;
                    oListItem["recepFechaProcesado"] = fechaProcesadoRecepcion.ToString();
                    oListItem["recepComentario"] = comentarioRecepcion;
                    oListItem["recepAccion"] = aprobadoRecepcion;
                    oListItem["recepRecibido"] = DateTime.Now;
                    oListItem["recepProcesado"] = DateTime.Now;
                    oListItem["docUltimoUsuarioAccion"] = nombreUsuario;
                    oListItem["docFechaUltimaAccion"] = DateTime.Now;
                    oListItem["recepProcesadoPor"] = usu;
                    oListItem["recepProcesadoNombre"] = usuRecepcionNombre;
                    oListItem["comOrdenInterna"] = nroOrdenInterna;
                    oListItem["comEsProyecto"] = esProyecto;
                    oListItem["envioCorreo"] = enviarCorreo;
                    oListItem["comCvProveedor"] = cv;
                    oListItem["docUltimoTipoMovimiento"] = tipoMovimiento;
                    oListItem["recepFechaAddFactura"] = DateTime.Now;
                    oListItem["comProveedorDireccionado"] = proveedorDireccionado;
                    oListItem["esDeg"] = esDeg;


                    oListItem.Update();
                    clientContext.Load(oListItem);
                    clientContext.ExecuteQuery();

                    //foreach apra insertar los documentos  
                    //if (dtCompras.Rows.Count > 0)
                    //{
                    //    foreach (DataRow row in dtCompras.Rows)
                    //    {
                    //        string path = row["docRutaFisica"].ToString();

                    //        FileStream stream = new FileStream(path, FileMode.Open);
                    //        AttachmentCreationInformation attInfo = new AttachmentCreationInformation();
                    //        attInfo.FileName = stream.Name;
                    //        attInfo.ContentStream = stream;

                    //        oListItem.AttachmentFiles.Add(attInfo);
                    //    }
                    //}

                    //if (dtRecepcion.Rows.Count > 0)
                    //{
                    //    foreach (DataRow row in dtRecepcion.Rows)
                    //    {
                    //        string path = row["docRutaFisica"].ToString();

                    //        FileStream stream = new FileStream(path, FileMode.Open);
                    //        AttachmentCreationInformation attInfo = new AttachmentCreationInformation();
                    //        attInfo.FileName = stream.Name;
                    //        attInfo.ContentStream = stream;

                    //        oListItem.AttachmentFiles.Add(attInfo);
                    //    }
                    //}

                    clientContext.Load(oListItem);
                    clientContext.ExecuteQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void InsertSharePointAdjuntos(string usu, string pass, string nroOc, string accion, string ultimoComentario, string departamento, string tipoLegajo,
                    string codigoSAP, string ruc, string nombreProveedor, string tipoMoneda, string montoTotal, string nombreEmpresa, string nroSP, string comentarioCompras,
                    string aprobado, DateTime fechaRecibido, DateTime fechaProcesado, string usuarioSolicitante, string nombreSolicitante, string usuCreado, string nombreUsuario,
                    //DataTable dt, 
                    string esProyecto, string nroOrdenInterna, string enviarCorreo, string nombreDepartamentoAnterior, string cv, string tipoMovimiento, string esDireccionado,
                    string esDeg, string nroMarco//, string correoProveedor
            )
        {

            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");

            using (ClientContext clientContext = new ClientContext(URL_status))
            {
                // SharePoint Online Credentials    
                //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                ListItem oListItem = oList.AddItem(itemCreateInfo);
                oListItem["Title"] = nroOc;
                oListItem["docUltimaAccion"] = accion;
                oListItem["docDepartamentoAnterior"] = nombreDepartamentoAnterior;
                oListItem["docUltimoComentario"] = ultimoComentario;
                oListItem["docDepartamento"] = departamento;
                oListItem["docNombreDepartamento"] = "Recepción";
                oListItem["comTipoLegajo"] = tipoLegajo;
                oListItem["comProveedorCodSap"] = codigoSAP;
                oListItem["comProveedorRuc"] = ruc;
                oListItem["comProveedorNombre"] = nombreProveedor;
                oListItem["comTipoMoneda"] = tipoMoneda;
                oListItem["comMontoTotal"] = montoTotal;
                oListItem["comEmpresa"] = nombreEmpresa;
                oListItem["comNroSP"] = nroSP;
                oListItem["esDeg"] = esDeg;
                oListItem["comComentarioCompras"] = comentarioCompras;
                oListItem["comAprobado"] = aprobado;
                oListItem["comFechaRecibido"] = fechaRecibido;
                oListItem["comFechaProcesado"] = fechaProcesado;
                oListItem["comSolicitante"] = usuarioSolicitante;
                oListItem["comSolicitanteNombre"] = nombreSolicitante;
                oListItem["comCreadoPor"] = usuCreado;
                oListItem["comCreadoNombre"] = nombreUsuario;
                oListItem["comEsProyecto"] = esProyecto;
                oListItem["comOrdenInterna"] = nroOrdenInterna;
                oListItem["comCvProveedor"] = cv;
                oListItem["docUltimoTipoMovimiento"] = tipoMovimiento;
                oListItem["comProveedorDireccionado"] = esDireccionado;
                oListItem["docUltimoUsuarioAccion"] = nombreUsuario;
                oListItem["docFechaUltimaAccion"] = DateTime.Now;
                oListItem["envioCorreo"] = enviarCorreo;
                oListItem["comNroContratoMarco"] = nroMarco;
                //oListItem["recepCorreoProveedor"] = correoProveedor;

                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();

                //foreach apra insertar los documentos  
                //if (dt.Rows.Count > 0)
                //{
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        string path = row["docRutaFisica"].ToString();

                //        FileStream stream = new FileStream(path, FileMode.Open);
                //        AttachmentCreationInformation attInfo = new AttachmentCreationInformation();
                //        attInfo.FileName = stream.Name;
                //        attInfo.ContentStream = stream;

                //        oListItem.AttachmentFiles.Add(attInfo);
                //    }
                //}

                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }
        public void ActualizarRecepcionSharepointAjuntos(long ID, string usu, string pass, string nroFactura, string tipoDocumento, DateTime fechaFactura, string timbrado, string montoFactura,
                                string estadoDocumento, DateTime fechaProcesado, string comentario, string usuProcesado, string nombreProcesado, string aprobado, string departamento,
                                string enviarCorreo, string nombreDepartamentoAnterior, string nombreDepartamento, string cv, string tipoMovimiento, string tipoFactura,
                                string recepTipoMoneda, string nombreProveedorRecepcion, string rucProveedorRecepcion, string idProveedorRecepcion, string facturaAsociadaNotaCredito)
        {
            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
            using (ClientContext clientContext = new ClientContext(URL_status))
            {
                // SharePoint Online Credentials    
                //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                // ListItem oListItem = oList.AddItem(itemCreateInfo);
                ListItem oListItem = oList.GetItemById(ID.ToString());
                oListItem["docUltimaAccion"] = aprobado;
                oListItem["docUltimoComentario"] = comentario;
                oListItem["docIdUltimaAccion"] = "27";
                oListItem["docDepartamentoAnterior"] = nombreDepartamentoAnterior;
                oListItem["docDepartamento"] = departamento;
                oListItem["docUltimoTipoMovimiento"] = tipoMovimiento;
                oListItem["docNombreDepartamento"] = nombreDepartamento;
                oListItem["envioCorreo"] = enviarCorreo;
                oListItem["recepNroFactura"] = nroFactura;
                oListItem["recepTipoDocumento"] = tipoDocumento;
                oListItem["recepFechaFactura"] = fechaFactura;
                oListItem["recepFacturaAsociadaNotaCredito"] = facturaAsociadaNotaCredito;
                if (timbrado == "")
                {
                    oListItem["recepTimbrado"] = "0";
                }
                else
                {
                    oListItem["recepTimbrado"] = timbrado;
                }
                oListItem["recepTipoFactura"] = tipoFactura;
                oListItem["recepMontoFactura"] = montoFactura;
                oListItem["recepOriginalCopia"] = estadoDocumento;
                oListItem["recepFechaProcesado"] = fechaProcesado.ToString();
                oListItem["recepComentario"] = comentario;
                oListItem["recepAccion"] = aprobado;
                oListItem["recepRecibido"] = DateTime.Now;
                oListItem["recepProcesado"] = DateTime.Now;
                oListItem["docFechaUltimaAccion"] = DateTime.Now;
                oListItem["docUltimoUsuarioAccion"] = nombreProcesado;
                oListItem["recepProcesadoNombre"] = nombreProcesado;
                oListItem["recepProcesadoPor"] = usuProcesado;
                oListItem["recepTipoMoneda"] = recepTipoMoneda;
                oListItem["comCvProveedor"] = cv;
                oListItem["recepFechaAddFactura"] = DateTime.Now;

                oListItem["recepProveedorNombre"] = nombreProveedorRecepcion;
                oListItem["recepProveedorRuc"] = rucProveedorRecepcion;
                oListItem["recepProveedorId"] = idProveedorRecepcion;

                //if (dt.Rows.Count > 0)
                //{
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        string path = row["docRutaFisica"].ToString();

                //        FileStream stream = new FileStream(path, FileMode.Open);
                //        AttachmentCreationInformation attInfo = new AttachmentCreationInformation();
                //        attInfo.FileName = stream.Name;
                //        attInfo.ContentStream = stream;
                //        oListItem.AttachmentFiles.Add(attInfo);
                //    }


                //}

                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }
        public void CargaNuevosAdjuntosSharepoint(long ID, string usu, string pass, string path)
        {
            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
            using (ClientContext clientContext = new ClientContext(URL_status))
            {
                // SharePoint Online Credentials    
                //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                // ListItem oListItem = oList.AddItem(itemCreateInfo);
                ListItem oListItem = oList.GetItemById(ID.ToString());

                FileStream stream = new FileStream(path, FileMode.Open);
                AttachmentCreationInformation attInfo = new AttachmentCreationInformation();
                attInfo.FileName = stream.Name;
                attInfo.ContentStream = stream;
                oListItem.AttachmentFiles.Add(attInfo);

                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }
        public string RetornaDatosPorID(string ID, string usu, string pass)
        {
            string siteUrl = @"https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items(" + ID + ")?$expand=AttachmentFiles";

            try
            {

                var credential = new CredentialCache
                {
                  {
                   new Uri(siteUrl), "NTLM",  new NetworkCredential(usu, pass)
                  }
                };
                var client = new RestClient(siteUrl)
                {
                    Authenticator = new NtlmAuthenticator(credential)
                };
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                var body = @"";
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);

                return response.Content;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void ActualizaMotivosSharepoint(long ID, string usu, string pass, string nroFactura, string tipoDocumento, DateTime fechaFactura, string timbrado, string montoFactura,
                                string estadoDocumento, DateTime fechaProcesado, string comentario, string nombreProcesado, string motivo, string departamentoActual,
                                string departamentoSigueinte, string idMotivo, string rechazado, string cuentaPagarNroAsiento, string nombreDepartamentoSiguiente,
                                string enviarCorreo, string nombreDepartamentoAnterior, string tipoMotivo, string nroDespacho, string tipoFactura,
                                string nombreProveedorRecepcion, string rucProveedorRecepcion, string idProveedorRecepcion)
        {
            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
            using (ClientContext clientContext = new ClientContext(URL_status))
            {
                // SharePoint Online Credentials    
                //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                // ListItem oListItem = oList.AddItem(itemCreateInfo);
                ListItem oListItem = oList.GetItemById(ID.ToString());
                oListItem["docUltimaAccion"] = motivo;
                if (comentario == "")
                {
                    comentario = "Sin comentario";
                }
                oListItem["docUltimoComentario"] = comentario;
                oListItem["docDepartamento"] = departamentoSigueinte;
                oListItem["docFechaUltimaAccion"] = DateTime.Now;
                oListItem["docUltimoUsuarioAccion"] = nombreProcesado;
                oListItem["docIdUltimaAccion"] = idMotivo;
                oListItem["docDepartamentoAnterior"] = nombreDepartamentoAnterior;
                oListItem["docNombreDepartamento"] = nombreDepartamentoSiguiente;
                oListItem["envioCorreo"] = enviarCorreo;
                oListItem["docUltimoTipoMovimiento"] = tipoMotivo;


                if (departamentoActual == "Recepcion")
                {
                    oListItem["recepNroFactura"] = nroFactura;
                    oListItem["recepTipoDocumento"] = tipoDocumento;
                    oListItem["recepFechaFactura"] = fechaFactura;
                    oListItem["recepTimbrado"] = timbrado;
                    oListItem["recepMontoFactura"] = montoFactura;
                    oListItem["recepOriginalCopia"] = estadoDocumento;
                    oListItem["recepFechaProcesado"] = fechaProcesado.ToString();
                    oListItem["recepComentario"] = comentario;
                    oListItem["recepAccion"] = motivo;
                    oListItem["recepRecibido"] = DateTime.Now;
                    oListItem["recepProcesado"] = DateTime.Now;
                    oListItem["recepProcesadoNombre"] = nombreProcesado;
                    oListItem["recepProcesadoPor"] = usu;
                    oListItem["recepNroDespacho"] = nroDespacho;
                    oListItem["recepTipoFactura"] = tipoFactura;
                    oListItem["recepProveedorNombre"] = nombreProveedorRecepcion;
                    oListItem["recepProveedorRuc"] = rucProveedorRecepcion;
                    oListItem["recepProveedorId"] = idProveedorRecepcion;
                }

                if (departamentoActual == "Impuestos")
                {
                    oListItem["impProcesadoPor"] = usu;
                    oListItem["impProcesadoNombre"] = nombreProcesado;
                    oListItem["impAccion"] = motivo;
                    oListItem["impProcesado"] = DateTime.Now;
                    oListItem["imprComentario"] = comentario;
                }

                if (departamentoActual == "CuentaPagar")
                {
                    oListItem["cuentaPagarProcesadoPor"] = usu;
                    oListItem["cuentaPagarProcesadoNombre"] = nombreProcesado;
                    oListItem["cuentaPagarAccion"] = motivo;
                    oListItem["cuentaPagarComentario"] = comentario;
                    oListItem["cuentaPagarProcesado"] = DateTime.Now;
                    oListItem["cuentaPagarNroAsiento"] = cuentaPagarNroAsiento;
                    oListItem["cuentaPagarYaLoProceso"] = "SI";
                }

                if (departamentoActual == "RevisionCompras")
                {
                    oListItem["revComprasProcesadoPor"] = usu;
                    oListItem["revComprasProcesadoNombre"] = nombreProcesado;
                    oListItem["revComprasComentario"] = comentario;
                    oListItem["revComprasPagarAccion"] = motivo;
                    oListItem["revComprasProcesado"] = DateTime.Now;
                    if (rechazado == "Rechazado")
                    {
                        oListItem["revComprasRechazada"] = rechazado;
                    }

                }

                if (departamentoActual == "Solicitante")
                {
                    oListItem["solProcesadoPor"] = usu;
                    oListItem["solProcesadoNombre"] = nombreProcesado;
                    oListItem["solAccion"] = motivo;
                    oListItem["solComentario"] = comentario;
                    oListItem["solProcesado"] = DateTime.Now;

                }

                if (departamentoActual == "Tesoreria")
                {
                    oListItem["tesoProcesadoPor"] = usu;
                    oListItem["tesoProcesadoNombre"] = nombreProcesado;
                    oListItem["tesoAccion"] = motivo;
                    oListItem["tesoComentario"] = comentario;
                    oListItem["tesoProcesado"] = DateTime.Now;
                    //oListItem["tesoNroAsiento"] = nroAsiento;
                }


                if (departamentoSigueinte == "Recepcion")
                {
                    oListItem["recepRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Impuestos")
                {
                    oListItem["impRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "CuentaPagar")
                {
                    oListItem["cuentaPagarRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "RevisionCompras")
                {
                    oListItem["revComprasRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Solicitante")
                {
                    oListItem["solRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Tesoreria")
                {
                    oListItem["tesoRecibido"] = DateTime.Now;
                }

                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }

        public string getVerificaSiExisteNroOC(string nroOc, string usu, string pass)
        {
            string siteUrl = @"https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$expand=AttachmentFiles&$filter=Title eq '" + nroOc + "'";

            try
            {

                //var client = new RestClient(siteUrl)
                //{
                //    Authenticator = new RestSharp.Authenticators.NtlmAuthenticator()
                //};
                var credential = new CredentialCache
                {
                  {
                   new Uri(siteUrl), "NTLM",  new NetworkCredential(usu, pass)
                  }
                };
                var client = new RestClient(siteUrl)
                {
                    Authenticator = new NtlmAuthenticator(credential)
                };
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                var body = @"";
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);

                return response.Content;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public string RetornaDatosBuscadorGeneral(string url, string usu, string pass)
        {

            try
            {
                var credential = new CredentialCache
                {
                  {
                   new Uri(url), "NTLM",  new NetworkCredential(usu, pass)
                  }
                };
                var client = new RestClient(url)
                {
                    Authenticator = new NtlmAuthenticator(credential)
                };
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                var body = @"";
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);

                return response.Content;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public int ActualizaCamposSubaMasivaTesoreria(long ID, string usu, string pass, string nroRetencion, string compensacion_pago, string enviarCorreo, string faltraProcesar,
            string fechaRealPago)
        {

            try
            {
                string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
                using (ClientContext clientContext = new ClientContext(URL_status))
                {
                    // SharePoint Online Credentials    
                    //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                    clientContext.Credentials = new NetworkCredential(usu, pass);
                    Web web = clientContext.Web;

                    clientContext.Load(web);
                    clientContext.ExecuteQuery();

                    Web myweb = clientContext.Web;
                    List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem oListItem = oList.GetItemById(ID.ToString());
                    oListItem["tesoNroRetencion"] = nroRetencion;
                    oListItem["tesoCompensacionPago"] = compensacion_pago;
                    oListItem["envioCorreo"] = enviarCorreo;
                    oListItem["tesoFaltaProcesarMasivo"] = faltraProcesar;
                    oListItem["tesoFechaRealPago"] = fechaRealPago;

                    oListItem.Update();
                    clientContext.Load(oListItem);
                    clientContext.ExecuteQuery();
                }

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void RechazaFacturaFisicaImpuestos(long ID, string usu, string pass, string comentario, string nombreProcesado, string motivo,
                                string departamentoSigueinte, string idMotivo, string nombreDepartamentoSiguiente,
                                string enviarCorreo, string nombreDepartamentoAnterior, string tipoMotivo)
        {
            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
            using (ClientContext clientContext = new ClientContext(URL_status))
            {
                // SharePoint Online Credentials    
                //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                // ListItem oListItem = oList.AddItem(itemCreateInfo);
                ListItem oListItem = oList.GetItemById(ID.ToString());

                oListItem["docUltimaAccion"] = motivo;
                if (comentario == "")
                {
                    comentario = "Sin comentario";
                }
                oListItem["docUltimoComentario"] = comentario;
                oListItem["docDepartamento"] = departamentoSigueinte;
                oListItem["docFechaUltimaAccion"] = DateTime.Now;
                oListItem["docUltimoUsuarioAccion"] = nombreProcesado;
                oListItem["docIdUltimaAccion"] = idMotivo;
                oListItem["docDepartamentoAnterior"] = nombreDepartamentoAnterior;
                oListItem["docNombreDepartamento"] = nombreDepartamentoSiguiente;
                oListItem["envioCorreo"] = enviarCorreo;
                oListItem["docUltimoTipoMovimiento"] = tipoMotivo;


                oListItem["impProcesadoPor"] = usu;
                oListItem["impProcesadoNombre"] = nombreProcesado;
                oListItem["impAccion"] = motivo;
                oListItem["impProcesado"] = DateTime.Now;
                oListItem["imprComentario"] = comentario;

                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }

        public void ActualizaMotivosRecepcion(long ID, string usu, string pass, string nroFactura, string tipoDocumento, DateTime fechaFactura, string timbrado, string montoFactura,
                                string estadoDocumento, DateTime fechaProcesado, string comentario, string nombreProcesado, string motivo,
                                string departamentoSigueinte, string idMotivo, string rechazado, string nombreDepartamentoSiguiente,
                                string enviarCorreo, string nombreDepartamentoAnterior, string tipoMotivo, string nroDespacho, string tipoFactura,
                                string nombreProveedorRecepcion, string rucProveedorRecepcion, string idProveedorRecepcion, string codigoSap, string nroOC,
                                string facturaAsociadaNotaCredito, string moneda, string correoProveedor)
        {
            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
            using (ClientContext clientContext = new ClientContext(URL_status))
            {
                // SharePoint Online Credentials    
                //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                // ListItem oListItem = oList.AddItem(itemCreateInfo);
                ListItem oListItem = oList.GetItemById(ID.ToString());
                oListItem["docUltimaAccion"] = motivo;
                if (comentario == "")
                {
                    comentario = "Sin comentario";
                }

                oListItem["docUltimoComentario"] = comentario;
                oListItem["docDepartamento"] = departamentoSigueinte;
                oListItem["docFechaUltimaAccion"] = DateTime.Now;
                oListItem["docUltimoUsuarioAccion"] = nombreProcesado;
                oListItem["docIdUltimaAccion"] = idMotivo;
                oListItem["docDepartamentoAnterior"] = nombreDepartamentoAnterior;
                oListItem["docNombreDepartamento"] = nombreDepartamentoSiguiente;
                oListItem["envioCorreo"] = enviarCorreo;
                oListItem["docUltimoTipoMovimiento"] = tipoMotivo;
                oListItem["recepNroFactura"] = nroFactura;
                oListItem["recepTipoDocumento"] = tipoDocumento;
                oListItem["recepFechaFactura"] = fechaFactura;
                oListItem["Title"] = nroOC;
                oListItem["recepFacturaAsociadaNotaCredito"] = facturaAsociadaNotaCredito;
                oListItem["recepProveedorSap"] = codigoSap;
                oListItem["recepTipoMoneda"] = moneda;
                if (timbrado == "")
                {
                    oListItem["recepTimbrado"] = "0";
                }
                else
                {
                    oListItem["recepTimbrado"] = timbrado;
                }
                oListItem["recepMontoFactura"] = montoFactura;
                oListItem["recepOriginalCopia"] = estadoDocumento;
                oListItem["recepFechaProcesado"] = fechaProcesado.ToString();
                oListItem["recepComentario"] = comentario;
                oListItem["recepAccion"] = motivo;
                oListItem["recepRecibido"] = DateTime.Now;
                oListItem["recepProcesado"] = DateTime.Now;
                oListItem["recepProcesadoNombre"] = nombreProcesado;
                oListItem["recepProcesadoPor"] = usu;
                oListItem["recepNroDespacho"] = nroDespacho;
                oListItem["recepTipoFactura"] = tipoFactura;
                oListItem["recepProveedorNombre"] = nombreProveedorRecepcion;
                oListItem["recepProveedorRuc"] = rucProveedorRecepcion;
                oListItem["recepProveedorId"] = idProveedorRecepcion;
                oListItem["recepCorreoProveedor"] = correoProveedor;

                if (departamentoSigueinte == "Recepcion")
                {
                    oListItem["recepRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Impuestos")
                {
                    oListItem["impRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "CuentaPagar")
                {
                    oListItem["cuentaPagarRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "RevisionCompras")
                {
                    oListItem["revComprasRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Solicitante")
                {
                    oListItem["solRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Tesoreria")
                {
                    oListItem["tesoRecibido"] = DateTime.Now;
                }


                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }

        public void ActualizaMotivosImpuestos(long ID, string usu, string pass, string comentario, string nombreProcesado, string motivo,
                                string departamentoSigueinte, string idMotivo, string nombreDepartamentoSiguiente,
                                string enviarCorreo, string nombreDepartamentoAnterior, string tipoMotivo)
        {
            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
            using (ClientContext clientContext = new ClientContext(URL_status))
            {
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                ListItem oListItem = oList.GetItemById(ID.ToString());
                oListItem["docUltimaAccion"] = motivo;
                if (comentario == "")
                {
                    comentario = "Sin comentario";
                }
                oListItem["docUltimoComentario"] = comentario;
                oListItem["docDepartamento"] = departamentoSigueinte;
                oListItem["docFechaUltimaAccion"] = DateTime.Now;
                oListItem["docUltimoUsuarioAccion"] = nombreProcesado;
                oListItem["docIdUltimaAccion"] = idMotivo;
                oListItem["docDepartamentoAnterior"] = nombreDepartamentoAnterior;
                oListItem["docNombreDepartamento"] = nombreDepartamentoSiguiente;
                oListItem["envioCorreo"] = enviarCorreo;
                oListItem["docUltimoTipoMovimiento"] = tipoMotivo;


                oListItem["impProcesadoPor"] = usu;
                oListItem["impProcesadoNombre"] = nombreProcesado;
                oListItem["impAccion"] = motivo;
                oListItem["impProcesado"] = DateTime.Now;
                oListItem["imprComentario"] = comentario;

                if (departamentoSigueinte == "Recepcion")
                {
                    oListItem["recepRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Impuestos")
                {
                    oListItem["impRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "CuentaPagar")
                {
                    oListItem["cuentaPagarRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "RevisionCompras")
                {
                    oListItem["revComprasRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Solicitante")
                {
                    oListItem["solRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Tesoreria")
                {
                    oListItem["tesoRecibido"] = DateTime.Now;
                }


                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }

        public void ActualizaMotivosRevisionCompras(long ID, string usu, string pass, string comentario, string nombreProcesado,
                                string motivo, string departamentoSigueinte, string idMotivo, string nombreDepartamentoSiguiente,
                                string enviarCorreo, string nombreDepartamentoAnterior, string tipoMotivo)
        {
            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
            using (ClientContext clientContext = new ClientContext(URL_status))
            {
                // SharePoint Online Credentials    
                //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                // ListItem oListItem = oList.AddItem(itemCreateInfo);
                ListItem oListItem = oList.GetItemById(ID.ToString());
                oListItem["docUltimaAccion"] = motivo;
                if (comentario == "")
                {
                    comentario = "Sin comentario";
                }
                oListItem["docUltimoComentario"] = comentario;
                oListItem["docDepartamento"] = departamentoSigueinte;
                oListItem["docFechaUltimaAccion"] = DateTime.Now;
                oListItem["docUltimoUsuarioAccion"] = nombreProcesado;
                oListItem["docIdUltimaAccion"] = idMotivo;
                oListItem["docDepartamentoAnterior"] = nombreDepartamentoAnterior;
                oListItem["docNombreDepartamento"] = nombreDepartamentoSiguiente;
                oListItem["envioCorreo"] = enviarCorreo;
                oListItem["docUltimoTipoMovimiento"] = tipoMotivo;
                oListItem["revComprasProcesadoPor"] = usu;
                oListItem["revComprasProcesadoNombre"] = nombreProcesado;
                oListItem["revComprasComentario"] = comentario;
                oListItem["revComprasPagarAccion"] = motivo;
                oListItem["revComprasProcesado"] = DateTime.Now;

                if (departamentoSigueinte == "GerenciaFinanciera")
                {
                    oListItem["comGerenciaUsuarioAprobacion"] = "m.miltos";
                    oListItem["comGerenciaNombreAprobacion"] = "Matias Miltos";
                }

                if (departamentoSigueinte == "Recepcion")
                {
                    oListItem["recepRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Impuestos")
                {
                    oListItem["impRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "CuentaPagar")
                {
                    oListItem["cuentaPagarRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "RevisionCompras")
                {
                    oListItem["revComprasRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Solicitante")
                {
                    oListItem["solRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Tesoreria")
                {
                    oListItem["tesoRecibido"] = DateTime.Now;
                }

                if (tipoMotivo == "Rechazado")
                {
                    oListItem["revComprasRechazada"] = tipoMotivo;
                }

                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }

        public void ActualizaMotivosCuentaPagar(long ID, string usu, string pass, string comentario, string nombreProcesado, string motivo,
                                string departamentoSigueinte, string idMotivo, string cuentaPagarNroAsiento, string nombreDepartamentoSiguiente,
                                string enviarCorreo, string nombreDepartamentoAnterior, string tipoMotivo)
        {
            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
            using (ClientContext clientContext = new ClientContext(URL_status))
            {
                // SharePoint Online Credentials    
                //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                // ListItem oListItem = oList.AddItem(itemCreateInfo);
                ListItem oListItem = oList.GetItemById(ID.ToString());
                oListItem["docUltimaAccion"] = motivo;
                if (comentario == "")
                {
                    comentario = "Sin comentario";
                }
                oListItem["docUltimoComentario"] = comentario;
                oListItem["docDepartamento"] = departamentoSigueinte;
                oListItem["docFechaUltimaAccion"] = DateTime.Now;
                oListItem["docUltimoUsuarioAccion"] = nombreProcesado;
                oListItem["docIdUltimaAccion"] = idMotivo;
                oListItem["docDepartamentoAnterior"] = nombreDepartamentoAnterior;
                oListItem["docNombreDepartamento"] = nombreDepartamentoSiguiente;
                oListItem["envioCorreo"] = enviarCorreo;
                oListItem["docUltimoTipoMovimiento"] = tipoMotivo;

                oListItem["cuentaPagarProcesadoPor"] = usu;
                oListItem["cuentaPagarProcesadoNombre"] = nombreProcesado;
                oListItem["cuentaPagarAccion"] = motivo;
                oListItem["cuentaPagarComentario"] = comentario;
                oListItem["cuentaPagarProcesado"] = DateTime.Now;
                oListItem["cuentaPagarNroAsiento"] = cuentaPagarNroAsiento;
                oListItem["cuentaPagarYaLoProceso"] = "SI";


                //if (departamentoActual == "Solicitante")
                //{
                //    oListItem["solProcesadoPor"] = usu;
                //    oListItem["solProcesadoNombre"] = nombreProcesado;
                //    oListItem["solAccion"] = motivo;
                //    oListItem["solComentario"] = comentario;
                //    oListItem["solProcesado"] = DateTime.Now;

                //}


                if (departamentoSigueinte == "Recepcion")
                {
                    oListItem["recepRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Impuestos")
                {
                    oListItem["impRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "CuentaPagar")
                {
                    oListItem["cuentaPagarRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "RevisionCompras")
                {
                    oListItem["revComprasRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Solicitante")
                {
                    oListItem["solRecibido"] = DateTime.Now;
                }

                if (departamentoSigueinte == "Tesoreria")
                {
                    oListItem["tesoRecibido"] = DateTime.Now;
                }

                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }

        public void ActualizaMotivosTesoreria(long ID, string usu, string pass, string comentario, string nombreProcesado, string motivo,
                               string departamentoSigueinte, string idMotivo, string nombreDepartamentoSiguiente,
                               string enviarCorreo, string nombreDepartamentoAnterior, string tipoMotivo)
        {
            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
            using (ClientContext clientContext = new ClientContext(URL_status))
            {
                // SharePoint Online Credentials    
                //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                // ListItem oListItem = oList.AddItem(itemCreateInfo);
                ListItem oListItem = oList.GetItemById(ID.ToString());
                oListItem["docUltimaAccion"] = motivo;
                if (comentario == "")
                {
                    comentario = "Sin comentario";
                }
                oListItem["docUltimoComentario"] = comentario;
                oListItem["docDepartamento"] = departamentoSigueinte;
                oListItem["docFechaUltimaAccion"] = DateTime.Now;
                oListItem["docUltimoUsuarioAccion"] = nombreProcesado;
                oListItem["docIdUltimaAccion"] = idMotivo;
                oListItem["docDepartamentoAnterior"] = nombreDepartamentoAnterior;
                oListItem["docNombreDepartamento"] = nombreDepartamentoSiguiente;
                oListItem["envioCorreo"] = enviarCorreo;
                oListItem["docUltimoTipoMovimiento"] = tipoMotivo;

                oListItem["tesoProcesadoPor"] = usu;
                oListItem["tesoProcesadoNombre"] = nombreProcesado;
                oListItem["tesoAccion"] = motivo;
                oListItem["tesoComentario"] = comentario;
                oListItem["tesoProcesado"] = DateTime.Now;
                //oListItem["tesoNroAsiento"] = nroAsiento;

                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }

        public void ActualizaComprasSharePointAdjuntos(string ID, string usu, string pass, string nroOc, string accion, string ultimoComentario, string departamento, string tipoLegajo,
                   string codigoSAP, string ruc, string nombreProveedor, string tipoMoneda, string montoTotal, string nombreEmpresa, string nroSP, string comentarioCompras,
                   string aprobado, DateTime fechaRecibido, DateTime fechaProcesado, string usuarioSolicitante, string nombreSolicitante, string usuCreado, string nombreUsuario,
                   DataTable dt, string esProyecto, string nroOrdenInterna, string enviarCorreo, string nombreDepartamentoAnterior, string cv, string tipoMovimiento, string esDeg,
                   string contratoMarco, string esDireccionado)
        {

            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");

            using (ClientContext clientContext = new ClientContext(URL_status))
            {
                // SharePoint Online Credentials    
                //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                //ListItem oListItem = oList.AddItem(itemCreateInfo);
                ListItem oListItem = oList.GetItemById(ID.ToString());
                oListItem["Title"] = nroOc;
                oListItem["comTipoLegajo"] = tipoLegajo;
                oListItem["comProveedorCodSap"] = codigoSAP;
                oListItem["comProveedorRuc"] = ruc;
                oListItem["comProveedorNombre"] = nombreProveedor;
                oListItem["comTipoMoneda"] = tipoMoneda;
                oListItem["comMontoTotal"] = montoTotal;
                oListItem["comEmpresa"] = nombreEmpresa;
                oListItem["comNroSP"] = nroSP;
                oListItem["comComentarioCompras"] = comentarioCompras;
                oListItem["comAprobado"] = aprobado;
                oListItem["comFechaRecibido"] = fechaRecibido;
                oListItem["comFechaProcesado"] = fechaProcesado;
                oListItem["comSolicitante"] = usuarioSolicitante;
                oListItem["comSolicitanteNombre"] = nombreSolicitante;
                oListItem["comCreadoPor"] = usuCreado;
                oListItem["comCreadoNombre"] = nombreUsuario;
                oListItem["comEsProyecto"] = esProyecto;
                oListItem["comOrdenInterna"] = nroOrdenInterna;
                oListItem["comCvProveedor"] = cv;
                oListItem["envioCorreo"] = enviarCorreo;
                oListItem["esDeg"] = esDeg;
                oListItem["comProveedorDireccionado"] = esDireccionado;
                oListItem["comNroContratoMarco"] = contratoMarco;

                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();

                //foreach apra insertar los documentos  
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string path = row["docRutaFisica"].ToString();

                        FileStream stream = new FileStream(path, FileMode.Open);
                        AttachmentCreationInformation attInfo = new AttachmentCreationInformation();
                        attInfo.FileName = stream.Name;
                        attInfo.ContentStream = stream;

                        oListItem.AttachmentFiles.Add(attInfo);
                    }
                }

                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }


        //public string getVerificaSiExisteNroOcCompras(string nroOc, string usu, string pass)
        //{
        //    string siteUrl = @"https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$expand=AttachmentFiles?$select=ID, Title&$filter=Title eq '" + nroOc + "'";

        //    try
        //    {

        //        //var client = new RestClient(siteUrl)
        //        //{
        //        //    Authenticator = new RestSharp.Authenticators.NtlmAuthenticator()
        //        //};
        //        var credential = new CredentialCache
        //        {
        //          {
        //           new Uri(siteUrl), "NTLM",  new NetworkCredential(usu, pass)
        //          }
        //        };
        //        var client = new RestClient(siteUrl)
        //        {
        //            Authenticator = new NtlmAuthenticator(credential)
        //        };
        //        client.Timeout = -1;
        //        var request = new RestRequest(Method.GET);
        //        var body = @"";
        //        request.AddParameter("text/plain", body, ParameterType.RequestBody);
        //        IRestResponse response = client.Execute(request);
        //        //Console.WriteLine(response.Content);

        //        return response.Content;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //}


        public void insertNuevoAdjunto(string ID, string usu, string pass, string ruta)
        {
            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
            using (ClientContext clientContext = new ClientContext(URL_status))
            {
                // SharePoint Online Credentials    
                //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                // ListItem oListItem = oList.AddItem(itemCreateInfo);
                ListItem oListItem = oList.GetItemById(ID.ToString());

                FileStream stream = new FileStream(ruta, FileMode.Open);
                AttachmentCreationInformation attInfo = new AttachmentCreationInformation();
                attInfo.FileName = stream.Name;
                attInfo.ContentStream = stream;
                oListItem.AttachmentFiles.Add(attInfo);

                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }

        public string RetornaIdMaximoSharePoint(string usu, string pass)
        {
            var url = "https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$select=ID&$top=1&$orderby=Id desc";

            try
            {
                var credential = new CredentialCache
                {
                  {
                   new Uri(url), "NTLM",  new NetworkCredential(usu, pass)
                  }
                };
                var client = new RestClient(url)
                {
                    Authenticator = new NtlmAuthenticator(credential)
                };
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                var body = @"";
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);

                return response.Content;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void InsertSharePointComprasParaImpuestos(string usu, string pass, string nroOc, string aprobadoRecepcion, string departamento, string tipoLegajo,
                   string codigoSAP, string ruc, string nombreProveedor, string tipoMoneda, string montoTotal, string nombreEmpresa, string nroSP, string comentarioCompras,
                   string aprobadoCompras, DateTime fechaRecibido, DateTime fechaProcesadoCompras, string usuarioSolicitante, string nombreSolicitante, string usuCreado, string nombreUsuario,
                   string nroFactura, string tipoDocumento, DateTime fechaFactura, string timbrado, string montoFactura, string estadoDocumento, DateTime fechaProcesadoRecepcion,
                   string comentarioRecepcion, string usuRecepcionNombre, string nroOrdenInterna, string esProyecto, string enviarCorreo,
                   string nombreDepartamentoAnterior, string nombreDepartamento, string cv, string tipoMovimiento, string tipoFactura, string recepTipoMoeda,
                   string rucProveedorRecepcion, string nombreProveedorRecepcion, string idProveedorRecepcion, string facturaAsociadaNotaCredito, string recepProveedorSap,
                   string esProveedorDireccionado, string esDeg, string recepTipoDocumentoReporte, string nombreUsuarioAccion)
        {

            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");

            try
            {
                using (ClientContext clientContext = new ClientContext(URL_status))
                {
                    // SharePoint Online Credentials    
                    //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                    clientContext.Credentials = new NetworkCredential(usu, pass);
                    Web web = clientContext.Web;

                    clientContext.Load(web);
                    clientContext.ExecuteQuery();

                    Web myweb = clientContext.Web;
                    List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem oListItem = oList.AddItem(itemCreateInfo);
                    oListItem["Title"] = nroOc;
                    oListItem["docUltimaAccion"] = aprobadoRecepcion;
                    oListItem["docUltimoComentario"] = comentarioRecepcion;
                    oListItem["docIdUltimaAccion"] = "67";
                    oListItem["docNombreDepartamento"] = nombreDepartamento;
                    oListItem["docDepartamentoAnterior"] = nombreDepartamentoAnterior;
                    oListItem["docDepartamento"] = departamento;
                    oListItem["comTipoLegajo"] = tipoLegajo;
                    oListItem["comProveedorCodSap"] = codigoSAP;
                    oListItem["comProveedorRuc"] = ruc;
                    oListItem["comProveedorNombre"] = nombreProveedor;
                    oListItem["comTipoMoneda"] = tipoMoneda;
                    oListItem["comMontoTotal"] = montoTotal;
                    oListItem["comEmpresa"] = nombreEmpresa;
                    oListItem["comNroSP"] = nroSP;
                    oListItem["comComentarioCompras"] = comentarioCompras;
                    oListItem["comAprobado"] = aprobadoCompras;
                    oListItem["comFechaRecibido"] = fechaRecibido;
                    oListItem["comFechaProcesado"] = fechaProcesadoCompras;
                    oListItem["comSolicitante"] = usuarioSolicitante;
                    oListItem["comSolicitanteNombre"] = nombreSolicitante;
                    oListItem["comCreadoPor"] = usuCreado;
                    oListItem["comCreadoNombre"] = nombreUsuario;
                    oListItem["recepNroFactura"] = nroFactura;
                    oListItem["recepTipoDocumento"] = tipoDocumento;
                    oListItem["recepFechaFactura"] = fechaFactura;
                    oListItem["recepFacturaAsociadaNotaCredito"] = facturaAsociadaNotaCredito;
                    if (timbrado == "")
                    {
                        oListItem["recepTimbrado"] = "0";
                    }
                    else
                    {
                        oListItem["recepTimbrado"] = timbrado;
                    }
                    oListItem["recepTipoMoneda"] = recepTipoMoeda;
                    oListItem["recepMontoFactura"] = montoFactura;
                    oListItem["recepProveedorNombre"] = nombreProveedorRecepcion;
                    oListItem["recepProveedorId"] = idProveedorRecepcion;
                    oListItem["recepProveedorRuc"] = rucProveedorRecepcion;
                    oListItem["recepTipoFactura"] = tipoFactura;
                    oListItem["recepOriginalCopia"] = estadoDocumento;
                    oListItem["recepFechaProcesado"] = fechaProcesadoRecepcion.ToString();
                    oListItem["recepComentario"] = comentarioRecepcion;
                    oListItem["recepAccion"] = aprobadoRecepcion;
                    oListItem["recepRecibido"] = DateTime.Now;
                    oListItem["recepProcesado"] = DateTime.Now;
                    oListItem["docUltimoUsuarioAccion"] = nombreUsuarioAccion;
                    oListItem["docFechaUltimaAccion"] = DateTime.Now;
                    oListItem["recepProcesadoPor"] = usu;
                    oListItem["recepProcesadoNombre"] = usuRecepcionNombre;
                    oListItem["comOrdenInterna"] = nroOrdenInterna;
                    oListItem["comEsProyecto"] = esProyecto;
                    oListItem["envioCorreo"] = enviarCorreo;
                    oListItem["comCvProveedor"] = cv;
                    oListItem["docUltimoTipoMovimiento"] = tipoMovimiento;
                    oListItem["recepFechaAddFactura"] = DateTime.Now;
                    oListItem["recepProveedorSap"] = recepProveedorSap;
                    oListItem["comProveedorDireccionado"] = esProveedorDireccionado;
                    oListItem["esDeg"] = esDeg;
                    oListItem["recepTipoDocumentoReporte"] = recepTipoDocumentoReporte;
                    oListItem["impRecibido"] = DateTime.Now;


                    oListItem.Update();
                    clientContext.Load(oListItem);
                    clientContext.ExecuteQuery();

                    //foreach apra insertar los documentos  
                    //if (dtCompras.Rows.Count > 0)
                    //{
                    //    foreach (DataRow row in dtCompras.Rows)
                    //    {
                    //        string path = row["docRutaFisica"].ToString();

                    //        FileStream stream = new FileStream(path, FileMode.Open);
                    //        AttachmentCreationInformation attInfo = new AttachmentCreationInformation();
                    //        attInfo.FileName = stream.Name;
                    //        attInfo.ContentStream = stream;

                    //        oListItem.AttachmentFiles.Add(attInfo);
                    //    }
                    //}

                    //if (dtRecepcion.Rows.Count > 0)
                    //{
                    //    foreach (DataRow row in dtRecepcion.Rows)
                    //    {
                    //        string path = row["docRutaFisica"].ToString();

                    //        FileStream stream = new FileStream(path, FileMode.Open);
                    //        AttachmentCreationInformation attInfo = new AttachmentCreationInformation();
                    //        attInfo.FileName = stream.Name;
                    //        attInfo.ContentStream = stream;

                    //        oListItem.AttachmentFiles.Add(attInfo);
                    //    }
                    //}

                    clientContext.Load(oListItem);
                    clientContext.ExecuteQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ActualizaEstadoDocumento(long ID, string usu, string pass, string estadoDocumento, string enviarCorreo)
        {
            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
            using (ClientContext clientContext = new ClientContext(URL_status))
            {
                // SharePoint Online Credentials    
                //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                // ListItem oListItem = oList.AddItem(itemCreateInfo);
                ListItem oListItem = oList.GetItemById(ID.ToString());

                oListItem["recepOriginalCopia"] = estadoDocumento;
                oListItem["envioCorreo"] = enviarCorreo;

                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }

        public string controlImportarSiFacturaExisteOC(string rucProveedor, string nroOc, string nroFactura, string usu, string pass)
        {
            try
            {
                var url = "https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$select=docDepartamento&$filter=((Title eq '" + nroOc + "' ) and (recepNroFactura eq '" + nroFactura + "' ))";
                var credential = new CredentialCache
                {
                  {
                   new Uri(url), "NTLM",  new NetworkCredential(usu, pass)
                  }
                };
                var client = new RestClient(url)
                {
                    Authenticator = new NtlmAuthenticator(credential)
                };
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                var body = @"";
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                return response.Content;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public MensajesModels VerificaSiExisteNroAsientoCuentaPagar(string nroAsientoCuentaPagar, string empresa, string usu, string pass, DateTime fechaAddFactura)
        {
            try
            {

                FacturaBusquedaAsientoModels datosPrincipal = new FacturaBusquedaAsientoModels();
                MensajesModels mensajes = new MensajesModels();

                var url = "https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$select=Title&$top=1000000" +
                    "&$filter=((cuentaPagarNroAsiento eq '" + nroAsientoCuentaPagar + "') " +
                    "and (comEmpresa eq '" + empresa + "') " +
                    "and ((recepFechaAddFactura ge datetime'" + fechaAddFactura.Year + "-01-01T00:00:00.000Z')) " +
                    "and ((recepFechaAddFactura le datetime'" + fechaAddFactura.Year + "-12-31T23:59:00.000Z')))";

                var credential = new CredentialCache
                {
                  {
                   new Uri(url), "NTLM",  new NetworkCredential(usu, pass)
                  }
                };
                var client = new RestClient(url)
                {
                    Authenticator = new NtlmAuthenticator(credential)
                };
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                var body = @"";
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string json = response.Content;
                datosPrincipal = JsonConvert.DeserializeObject<FacturaBusquedaAsientoModels>(json);

                if (datosPrincipal.value.Count > 0)
                {
                    mensajes.estado = true;
                }
                else
                {
                    mensajes.estado = false;
                }

                return mensajes;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public MensajesModels verificaSiExisteOcEnRecepcionSinFacturaCargada(string nroOc, string usu, string pass)
        {
            try
            {
                FacturaBusquedaAsientoModels datosPrincipal = new FacturaBusquedaAsientoModels();
                MensajesModels mensajes = new MensajesModels();
                var url = "https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$select=ID,Title&$filter=((Title eq '" + nroOc + "') and (docDepartamento eq 'Recepcion') and (recepNroFactura eq null))";
                var credential = new CredentialCache
                {
                  {
                   new Uri(url), "NTLM",  new NetworkCredential(usu, pass)
                  }
                };
                var client = new RestClient(url)
                {
                    Authenticator = new NtlmAuthenticator(credential)
                };
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                var body = @"";
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string json = response.Content;
                datosPrincipal = JsonConvert.DeserializeObject<FacturaBusquedaAsientoModels>(json);

                if (datosPrincipal.value.Count > 0)
                {
                    mensajes.estado = true;
                    foreach (var v in datosPrincipal.value)
                    {
                        mensajes.IdSharePoint = v.Id.ToString();
                    }
                }
                else
                {
                    mensajes.estado = false;
                }

                return mensajes;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void updateSharePointComprasParaImpuestos(string Id, string usu, string pass, string nroOc, string aprobadoRecepcion, string departamento, string tipoLegajo,
             string codigoSAP, string ruc, string nombreProveedor, string tipoMoneda, string montoTotal, string nombreEmpresa, string nroSP, string comentarioCompras,
             string aprobadoCompras, DateTime fechaRecibido, DateTime fechaProcesadoCompras, string usuarioSolicitante, string nombreSolicitante, string usuCreado, string nombreUsuario,
             string nroFactura, string tipoDocumento, DateTime fechaFactura, string timbrado, string montoFactura, string estadoDocumento, DateTime fechaProcesadoRecepcion,
             string comentarioRecepcion, string usuRecepcionNombre, string nroOrdenInterna, string esProyecto, string enviarCorreo,
             string nombreDepartamentoAnterior, string nombreDepartamento, string cv, string tipoMovimiento, string tipoFactura, string recepTipoMoeda,
             string rucProveedorRecepcion, string nombreProveedorRecepcion, string idProveedorRecepcion, string facturaAsociadaNotaCredito, string recepProveedorSap,
             string esProveedorDireccionado, string esDeg, string recepTipoDocumentoReporte, string nombreUsuarioAccion)
        {

            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");

            try
            {
                using (ClientContext clientContext = new ClientContext(URL_status))
                {
                    // SharePoint Online Credentials     
                    clientContext.Credentials = new NetworkCredential(usu, pass);
                    Web web = clientContext.Web;

                    clientContext.Load(web);
                    clientContext.ExecuteQuery();

                    Web myweb = clientContext.Web;
                    List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem oListItem = oList.GetItemById(Id.ToString());
                    oListItem["Title"] = nroOc;
                    oListItem["docUltimaAccion"] = aprobadoRecepcion;
                    oListItem["docUltimoComentario"] = comentarioRecepcion;
                    oListItem["docIdUltimaAccion"] = "67";
                    oListItem["docNombreDepartamento"] = nombreDepartamento;
                    oListItem["docDepartamentoAnterior"] = nombreDepartamentoAnterior;
                    oListItem["docDepartamento"] = departamento;
                    oListItem["comTipoLegajo"] = tipoLegajo;
                    oListItem["comProveedorCodSap"] = codigoSAP;
                    oListItem["comProveedorRuc"] = ruc;
                    oListItem["comProveedorNombre"] = nombreProveedor;
                    oListItem["comTipoMoneda"] = tipoMoneda;
                    oListItem["comMontoTotal"] = montoTotal;
                    oListItem["comEmpresa"] = nombreEmpresa;
                    oListItem["comNroSP"] = nroSP;
                    oListItem["comComentarioCompras"] = comentarioCompras;
                    oListItem["comAprobado"] = aprobadoCompras;
                    oListItem["comFechaRecibido"] = fechaRecibido;
                    oListItem["comFechaProcesado"] = fechaProcesadoCompras;
                    oListItem["comSolicitante"] = usuarioSolicitante;
                    oListItem["comSolicitanteNombre"] = nombreSolicitante;
                    oListItem["comCreadoPor"] = usuCreado;
                    oListItem["comCreadoNombre"] = nombreUsuario;
                    oListItem["recepNroFactura"] = nroFactura;
                    oListItem["recepTipoDocumento"] = tipoDocumento;
                    oListItem["recepFechaFactura"] = fechaFactura;
                    oListItem["recepFacturaAsociadaNotaCredito"] = facturaAsociadaNotaCredito;
                    if (timbrado == "")
                    {
                        oListItem["recepTimbrado"] = "0";
                    }
                    else
                    {
                        oListItem["recepTimbrado"] = timbrado;
                    }
                    oListItem["recepTipoMoneda"] = recepTipoMoeda;
                    oListItem["recepMontoFactura"] = montoFactura;
                    oListItem["recepProveedorNombre"] = nombreProveedorRecepcion;
                    oListItem["recepProveedorId"] = idProveedorRecepcion;
                    oListItem["recepProveedorRuc"] = rucProveedorRecepcion;
                    oListItem["recepTipoFactura"] = tipoFactura;
                    oListItem["recepOriginalCopia"] = estadoDocumento;
                    oListItem["recepFechaProcesado"] = fechaProcesadoRecepcion.ToString();
                    oListItem["recepComentario"] = comentarioRecepcion;
                    oListItem["recepAccion"] = aprobadoRecepcion;
                    oListItem["recepRecibido"] = DateTime.Now;
                    oListItem["recepProcesado"] = DateTime.Now;
                    oListItem["docUltimoUsuarioAccion"] = nombreUsuarioAccion;
                    oListItem["docFechaUltimaAccion"] = DateTime.Now;
                    oListItem["recepProcesadoPor"] = usu;
                    oListItem["recepProcesadoNombre"] = usuRecepcionNombre;
                    oListItem["comOrdenInterna"] = nroOrdenInterna;
                    oListItem["comEsProyecto"] = esProyecto;
                    oListItem["envioCorreo"] = enviarCorreo;
                    oListItem["comCvProveedor"] = cv;
                    oListItem["docUltimoTipoMovimiento"] = tipoMovimiento;
                    oListItem["recepFechaAddFactura"] = DateTime.Now;
                    oListItem["recepProveedorSap"] = recepProveedorSap;
                    oListItem["comProveedorDireccionado"] = esProveedorDireccionado;
                    oListItem["esDeg"] = esDeg;
                    oListItem["recepTipoDocumentoReporte"] = recepTipoDocumentoReporte;
                    oListItem["impRecibido"] = DateTime.Now;


                    oListItem.Update();
                    clientContext.Load(oListItem);
                    clientContext.ExecuteQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public OcsFiltradosPorSolicitanteModels get_ocs_filtrados_por_solicitante(string solicitante, string usu, string pass)
        {
            try
            { 
                MensajesModels mensajes = new MensajesModels();
                var url = "https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$select=ID,Title&$top=1000000&$filter=((comSolicitante eq '" + solicitante + "'))"; 
                var credential = new CredentialCache
                {
                  {
                   new Uri(url), "NTLM",  new NetworkCredential(usu, pass)
                  }
                };
                var client = new RestClient(url)
                {
                    Authenticator = new NtlmAuthenticator(credential)
                };
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                var body = @"";
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                string json = response.Content;
                var ocs = JsonConvert.DeserializeObject<OcsFiltradosPorSolicitanteModels>(json);
                 
                return ocs;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void ActualizarSolicitantesMasivamente(long ID, string usu, string pass, string solicitanteDe, string solicitanteNombreDe)
        {
            string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
            using (ClientContext clientContext = new ClientContext(URL_status))
            { 
                clientContext.Credentials = new NetworkCredential(usu, pass);
                Web web = clientContext.Web;

                clientContext.Load(web);
                clientContext.ExecuteQuery();

                Web myweb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation(); 
                ListItem oListItem = oList.GetItemById(ID.ToString());

                oListItem["comSolicitante"] = solicitanteDe;
                oListItem["comSolicitanteNombre"] = solicitanteNombreDe;

                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }
    }
}