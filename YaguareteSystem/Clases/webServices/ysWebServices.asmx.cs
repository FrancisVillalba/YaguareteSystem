using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using YaguareteSystem.Models;

namespace YaguareteSystem.Clases.webServices
{
    /// <summary>
    /// Descripción breve de ysWebServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class ysWebServices : System.Web.Services.WebService
    {
        SharePointBLL sharePoint = new SharePointBLL();
        LogsBLL lg = new LogsBLL();

        [WebMethod]
        public string[] retornaNombreColumnaReportes(int id)
        {
            string retorna = "";
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaNombreColumnas", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        retorna = rdr.GetString(0);
                    }
                    conexion.Close();
                }

                //return array; 
                string[] nombres = JsonConvert.DeserializeObject<string[]>(retorna);

                return nombres;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [WebMethod]
        public string retornaUrlFiltroReportes(int id)
        {
            string retorna = "";
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaContenidoColumnas", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        retorna = rdr.GetString(0);
                    }
                    conexion.Close();
                }
                return retorna;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public List<object[]> retornaDatosDepartamento(string departamentoFiltro, string usuario, string pass)
        {
            string url = "";
            DatosPrincipalesSharePointModels datosPrincipal = new DatosPrincipalesSharePointModels();
            List<DatosPrincipalesSharePointModels> lista = new List<DatosPrincipalesSharePointModels>();

            switch (departamentoFiltro)
            {
                case "Compras":
                    // code block
                    break;

                case "Recepcion":
                    url = @"https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$expand=AttachmentFiles&$top=10000000&$filter=((docDepartamento eq '" + departamentoFiltro + "') and (recepAccion ne null))";
                    break;

                case "Impuestos":
                    url = @"https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$expand=AttachmentFiles&$top=10000&$filter=docDepartamento eq '" + departamentoFiltro + "'";
                    break;

                case "CuentaPagar":
                    url = @"https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$expand=AttachmentFiles&$top=10000000&$filter=((docDepartamento eq 'CuentaPagar') or ((cuentaPagarYaLoProceso eq 'SI' and docDepartamento ne 'CuentaPagar')) and  (docDepartamento ne 'Tesoreria') and  (docDepartamento ne 'RevisionTesoreria') and  (docDepartamento ne 'Procesadas') and  (docDepartamento ne 'Rechazados') and  (docDepartamento ne 'RechazadosCompras') and  (docDepartamento ne 'RechazadosRecepcion'))";
                    break;

                case "RevisionCompras":
                    url = @"https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$expand=AttachmentFiles&$top=10000&$filter=docDepartamento eq '" + departamentoFiltro + "'";
                    break;

                case "Tesoreria":
                    url = @"https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$expand=AttachmentFiles&$top=10000&$filter=docDepartamento eq '" + departamentoFiltro + "'";
                    break;

                case "RevisionTesoreria":
                    url = @"https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$expand=AttachmentFiles&$top=10000&$filter=docDepartamento eq '" + departamentoFiltro + "'";
                    break;

                case "RechazadosCompras":
                    url = @"https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$expand=AttachmentFiles&$top=10000&$filter=docDepartamento eq '" + departamentoFiltro + "'";
                    break;

                case "RechazadosRecepcion":
                    url = @"https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$expand=AttachmentFiles&$top=10000&$filter=docDepartamento eq '" + departamentoFiltro + "'";
                    break;

                case "TesoreriaVistaDatosImportados":
                    url = @"https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$expand=AttachmentFiles&$top=10000&$filter=docDepartamento eq 'Tesoreria' and tesoFaltaProcesarMasivo eq 'Pendiente procesar'";
                    break;

                default:
                    // code block
                    break;
            }

            var json = sharePoint.RetornaDatosBuscadorGeneral(url, usuario, pass);
            datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesSharePointModels>(json);
            List<object[]> personNames = new List<object[]>();

            try
            {
                lista.Add(datosPrincipal);

                foreach (DatosPrincipalesSharePointModels p in lista)
                {
                    foreach (Value v in p.Values)
                    {
                        if (v.RecepAccion != null)
                        {
                            string tipoLegajo = v.ComTipoLegajo;
                            if (v.ComTipoLegajo == "Importacion")
                            {
                                tipoLegajo = "Importación";
                            }

                            string tipoFactura = v.recepTipoFactura.ToString();
                            if (tipoFactura == "Importacion")
                            {
                                tipoFactura = " Fact. Importación";
                            }

                            if (tipoFactura == "Local")
                            {
                                tipoFactura = "Fact. Local";
                            }

                            if (tipoFactura == "ComEgreso")
                            {
                                tipoFactura = "Comp. Egreso";
                            }

                            switch (departamentoFiltro)
                            {
                                case "Recepcion":
                                    object[] obj6 = { v.Id, v.RecepNroFactura, v.Title, tipoLegajo, tipoFactura, v.RecepProveedorNombre, Convert.ToDateTime(v.DocFechaUltimaAccion).ToString("dd/MM/yyyy"), v.DocUltimaAccion, v.DocUltimoComentario };
                                    personNames.Add(obj6);
                                    break;

                                case "Impuestos":
                                    object[] obj = { v.Id, v.RecepNroFactura, v.Title, tipoLegajo, tipoFactura, v.RecepProveedorNombre, Convert.ToDateTime(v.DocFechaUltimaAccion).ToString("dd/MM/yyyy"), v.DocUltimaAccion, v.ComEmpresa };
                                    personNames.Add(obj);
                                    break;

                                case "CuentaPagar":
                                    object[] obj5 = { v.Id, v.RecepNroFactura, v.Title, v.DocNombreDepartamento, tipoLegajo, tipoFactura, v.ComSolicitanteNombre, v.ComCreadoNombre, v.RecepProveedorNombre,/* Convert.ToDateTime(v.RecepFechaFactura).ToString("dd/MM/yyyy"),*/ Convert.ToDateTime(v.CuentaPagarRecibido).ToString("dd/MM/yyyy"), v.ComEmpresa, /*v.DocUltimoTipoMovimiento*/Convert.ToDateTime(v.RecepFechaFactura).ToString("dd/MM/yyyy"), v.DocUltimaAccion };
                                    personNames.Add(obj5);
                                    break;

                                case "RevisionCompras":
                                    object[] obj2 = { v.Id, v.RecepNroFactura, v.Title, v.ComCreadoNombre, tipoLegajo, tipoFactura, v.RecepProveedorNombre, Convert.ToDateTime(v.DocFechaUltimaAccion).ToString("dd/MM/yyyy"), v.DocUltimaAccion, v.DocUltimoComentario };
                                    personNames.Add(obj2);
                                    break;

                                case "Tesoreria":
                                    var urlstring = retornaUrlArchivo(v.RecepNroFactura.ToString(), v.Title);
                                    var boton = "";
                                    if (urlstring != "")
                                    {
                                        boton = "<a href='" + urlstring + "' target='_blank'><i class='fa fa-file-pdf' aria-hidden='true'></i></a>";
                                    }
                                    else
                                    {
                                        boton = "";
                                    }
                                    object[] obj3 = { v.Id, v.RecepNroFactura, v.Title, tipoLegajo, tipoFactura, v.RecepProveedorNombre, v.CuentaPagarNroAsiento, v.DocUltimaAccion, v.ComEmpresa, v.TesoFaltaProcesarMasivo, boton };
                                    personNames.Add(obj3);
                                    break;

                                case "TesoreriaVistaDatosImportados":
                                    var urlstring001 = retornaUrlArchivo(v.RecepNroFactura.ToString(), v.Title);
                                    var boton01 = "";
                                    if (urlstring001 != "")
                                    {
                                        boton01 = "<a href='" + urlstring001 + "' target='_blank'><i class='fa fa-file-pdf' aria-hidden='true'></i></a>";
                                    }
                                    else
                                    {
                                        boton01 = "";
                                    }
                                    object[] obj15 = { v.Id, v.RecepNroFactura, v.Title, tipoLegajo, tipoFactura, v.RecepProveedorNombre, v.CuentaPagarNroAsiento, v.DocUltimaAccion, v.ComEmpresa, v.TesoFaltaProcesarMasivo, boton01 };
                                    personNames.Add(obj15);
                                    break;

                                case "RevisionTesoreria":
                                    object[] obj4 = { v.Id, v.RecepNroFactura, v.Title, tipoLegajo, tipoFactura, v.RecepProveedorNombre, v.CuentaPagarNroAsiento, v.DocUltimaAccion, v.ComEmpresa };
                                    personNames.Add(obj4);
                                    break;

                                case "RechazadosCompras":
                                    object[] obj7 = { v.Id, v.RecepNroFactura, v.Title, v.ComCreadoNombre, tipoLegajo, tipoFactura, v.RecepProveedorNombre, Convert.ToDateTime(v.DocFechaUltimaAccion).ToString("dd/MM/yyyy"), v.DocUltimaAccion, v.DocUltimoComentario };
                                    personNames.Add(obj7);
                                    break;

                                case "RechazadosRecepcion":
                                    object[] obj8 = { v.Id, v.RecepNroFactura, v.Title, v.ComCreadoNombre, tipoLegajo, tipoFactura, v.RecepProveedorNombre, Convert.ToDateTime(v.DocFechaUltimaAccion).ToString("dd/MM/yyyy"), v.DocUltimaAccion, v.DocUltimoComentario };
                                    personNames.Add(obj8);
                                    break;
                                default:
                                    // code block
                                    break;
                            }
                            //if (departamentoFiltro == "RevisionCompras")
                            //{
                            //    object[] obj = { v.Id, v.RecepNroFactura, v.Title, v.ComCreadoNombre, tipoLegajo, tipoFactura, v.RecepProveedorNombre, Convert.ToDateTime(v.DocFechaUltimaAccion).ToString("dd/MM/yyyy"), v.DocUltimaAccion, v.DocUltimoComentario };
                            //    personNames.Add(obj);
                            //}
                            //else
                            // if (departamentoFiltro == "Impuestos")
                            //{
                            //    object[] obj = { v.Id, v.RecepNroFactura, v.Title, tipoLegajo, tipoFactura, v.RecepProveedorNombre, Convert.ToDateTime(v.DocFechaUltimaAccion).ToString("dd/MM/yyyy"), v.DocUltimaAccion, v.ComEmpresa };
                            //    personNames.Add(obj);
                            //}
                            //if (departamentoFiltro == "Tesoreria")
                            //{
                            //    var urlstring = retornaUrlArchivo(v.RecepNroFactura.ToString(), v.Title);
                            //    var boton = "";
                            //    if (urlstring != "")
                            //    {
                            //        boton = "<a href='" + urlstring + "' target='_blank'><i class='fa fa-file-pdf' aria-hidden='true'></i></a>";
                            //    }
                            //    else
                            //    {
                            //        boton = "";
                            //    }
                            //    object[] obj = { v.Id, v.RecepNroFactura, v.Title, tipoLegajo, tipoFactura, v.RecepProveedorNombre, v.CuentaPagarNroAsiento, v.DocUltimaAccion, v.ComEmpresa, v.TesoFaltaProcesarMasivo, boton };
                            //    personNames.Add(obj);
                            //}
                            //else
                            // if (departamentoFiltro == "RevisionTesoreria")
                            //{
                            //    object[] obj = { v.Id, v.RecepNroFactura, v.Title, tipoLegajo, tipoFactura, v.RecepProveedorNombre, v.CuentaPagarNroAsiento, v.DocUltimaAccion, v.ComEmpresa };
                            //    personNames.Add(obj);
                            //}
                            //else
                            //if (departamentoFiltro == "CuentaPagar")
                            //{
                            //    object[] obj = { v.Id, v.RecepNroFactura, v.Title, v.DocNombreDepartamento, tipoLegajo, tipoFactura, v.ComSolicitanteNombre, v.RecepProveedorNombre, Convert.ToDateTime(v.RecepFechaFactura).ToString("dd/MM/yyyy"), v.ComEmpresa, v.DocUltimoTipoMovimiento, v.DocUltimaAccion };
                            //    personNames.Add(obj);
                            //}
                            //else
                            //if (departamentoFiltro == "Recepcion")
                            //{
                            //    object[] obj = { v.Id, v.RecepNroFactura, v.Title, tipoLegajo, tipoFactura, v.RecepProveedorNombre, Convert.ToDateTime(v.DocFechaUltimaAccion).ToString("dd/MM/yyyy"), v.DocUltimaAccion, v.DocUltimoComentario };
                            //    personNames.Add(obj);
                            //}
                        }
                    }
                }
                return personNames;
            }
            catch (Exception ex)
            {
                return personNames;
            }
        }

        [WebMethod]
        public List<object[]> retornaDatosParaCompras(string usuario)
        {
            DataTable dt = new DataTable();
            List<object[]> datos = new List<object[]>();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaOrdenCompras", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }

                foreach (DataRow row in dt.Rows)
                {
                    object[] obj = {row["oComID"].ToString(),
                                    row["tipoDocumento"].ToString(),
                                    row["oComNroOC"].ToString(),
                                    row["oComUsuarioCreador"].ToString(),
                                    row["proveedor"].ToString(),
                                    row["moneda"].ToString(),
                                    row["oComMontoTotal"].ToString(),
                                    row["empresa"].ToString(),
                                    row["oComNombreSolicitante"].ToString(),
                                    row["oComEnUso"].ToString() };

                    datos.Add(obj);
                }

                return datos;

            }

            catch (Exception ex)
            {
                return null;
            }

        }

        [WebMethod]
        public List<object[]> retornaListaProveedores()
        {
            DataTable dt = new DataTable();
            List<object[]> datos = new List<object[]>();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_listarDatosProveedores", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }

                foreach (DataRow row in dt.Rows)
                {
                    object[] obj = {row["provID"].ToString(),
                                    row["provCodigoSAP"].ToString(),
                                    row["provRUC"].ToString(),
                                    row["provRazonSocial"].ToString(),
                                    row["provCorreo"].ToString()};

                    datos.Add(obj);
                }

                return datos;

            }

            catch (Exception ex)
            {
                return null;
            }

        }

        [WebMethod]
        public JsonResult retornaJsonReportes(string siteUrl, string usu, string pass)
        {
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

                string json = response.Content;

                var aaa = new JsonResult() { Data = json, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                return aaa;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [WebMethod]
        public string[] retornaOrdenBodyColumnaReportes(int id)
        {
            string retorna = "";
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaOrdenBodyColumnas", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        retorna = rdr.GetString(0);
                    }
                    conexion.Close();
                }

                //return array; 
                string[] nombres = JsonConvert.DeserializeObject<string[]>(retorna);

                return nombres;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [WebMethod]
        public bool procesarMasivamente(string ID, string motivo, string departamentoSiguiente, string nombreProcesado, string idMotivo, string departamentoActual, string usu, string pass,
            string comentario, string nroOC, string nombreDepartamento, string tipoMotivo, string nroFactura, string nombreComprador, string solicitanteNombre, string fechaFactura,
            string fechaAddFactura)
        {

            procesarSharepoint(ID, motivo, departamentoSiguiente, nombreProcesado, idMotivo, departamentoActual, usu, pass, comentario, nombreDepartamento, tipoMotivo, "SI");
            guardarComentarios(ID, motivo, departamentoSiguiente, nombreProcesado, idMotivo, departamentoActual, usu, pass, comentario, nroOC, nombreDepartamento, tipoMotivo);
            guardarDatosReporteStatusFacturaPowerBI(ID, motivo, departamentoSiguiente, nombreProcesado, idMotivo, departamentoActual, usu, pass, comentario, nroOC, nombreDepartamento, tipoMotivo,
                                                    nroFactura, nombreComprador, solicitanteNombre, fechaFactura, fechaAddFactura);

            return true;
        }

        private void procesarSharepoint(string ID, string motivo, string departamentoSiguiente, string nombreProcesado, string idMotivo, string departamentoActual, string usu, string pass,
            string comentario, string nombreDepartamento, string tipoMotivo, string enviarCorreo)
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
                    ListItem oListItem = oList.GetItemById(ID);
                    oListItem["docUltimaAccion"] = motivo;
                    oListItem["docDepartamento"] = departamentoSiguiente;
                    oListItem["docFechaUltimaAccion"] = DateTime.Now;
                    oListItem["docUltimoUsuarioAccion"] = nombreProcesado;
                    oListItem["docIdUltimaAccion"] = idMotivo;
                    oListItem["docDepartamentoAnterior"] = departamentoActual;
                    oListItem["docUltimoComentario"] = comentario;
                    oListItem["docNombreDepartamento"] = nombreDepartamento;
                    oListItem["docUltimoTipoMovimiento"] = tipoMotivo;
                    oListItem["envioCorreo"] = enviarCorreo;

                    if (departamentoActual == "Impuestos")
                    {
                        oListItem["impProcesadoPor"] = usu;
                        oListItem["impProcesadoNombre"] = nombreProcesado;
                        oListItem["impAccion"] = motivo;
                        oListItem["impProcesado"] = DateTime.Now;
                        oListItem["imprComentario"] = comentario;
                    }

                    //if (departamentoActual == "Tesoreria")
                    //{
                    //    oListItem["tesoProcesadoPor"] = usu;
                    //    oListItem["tesoProcesadoNombre"] = nombreProcesado;
                    //    oListItem["tesoAccion"] = motivo;
                    //    oListItem["tesoComentario"] = comentario;
                    //    oListItem["tesoProcesado"] = DateTime.Now;
                    //    oListItem["tesoNroAsiento"] = nroAsiento;
                    //}


                    if (departamentoSiguiente == "Recepcion")
                    {
                        oListItem["recepRecibido"] = DateTime.Now;
                    }

                    if (departamentoSiguiente == "Impuestos")
                    {
                        oListItem["impRecibido"] = DateTime.Now;
                    }

                    if (departamentoSiguiente == "CuentaPagar")
                    {
                        oListItem["cuentaPagarRecibido"] = DateTime.Now;
                    }

                    if (departamentoSiguiente == "RevisionCompras")
                    {
                        oListItem["revComprasRecibido"] = DateTime.Now;
                    }

                    if (departamentoSiguiente == "Solicitante")
                    {
                        oListItem["solRecibido"] = DateTime.Now;
                    }

                    if (departamentoSiguiente == "Tesoreria")
                    {
                        oListItem["tesoRecibido"] = DateTime.Now;
                    }

                    oListItem.Update();
                    clientContext.Load(oListItem);
                    clientContext.ExecuteQuery();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void guardarComentarios(string ID, string motivo, string departamentoSiguiente, string nombreProcesado, string idMotivo, string departamentoActual, string usu, string pass,
           string comentario, string nrooc, string nombreDepartamento, string tipoMotivo)
        {
            LogsBLL logs = new LogsBLL();
            try
            {
                logs.insertarComentario(nombreProcesado, Convert.ToInt32(ID), comentario, nrooc, tipoMotivo, motivo, departamentoActual, nombreDepartamento);

            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public bool procesarMasivamenteAdjunto(string ID, string motivo, string departamentoSiguiente, string nombreProcesado, string idMotivo, string departamentoActual, string usu, string pass,
           string comentario, string ruta, string nombreDepartamentoSiguiente, string tipoMotivo, string rutaVirtual, string nombre, string extension, string nroOC, string enviarCorreo,
           string nroFactura, string nombreComprador, string nombreSolicitante, string fechaFactura, string fechaAddFactura)
        {

            procesarSharepointTesoreria(ID, motivo, departamentoSiguiente, nombreProcesado, idMotivo, departamentoActual, usu, pass, comentario, ruta,
                nombreDepartamentoSiguiente, tipoMotivo, rutaVirtual, nombre, extension, enviarCorreo);

            insertarcomentarioTesoreria(ID, motivo, departamentoSiguiente, nombreProcesado, idMotivo, departamentoActual, usu, pass, comentario, ruta, nombreDepartamentoSiguiente, tipoMotivo, nroOC);

            guardarDatosReporteStatusFacturaPowerBI(ID, motivo, nombreDepartamentoSiguiente, nombreProcesado, idMotivo, departamentoActual, usu, pass, comentario, nroOC, departamentoActual, tipoMotivo,
                                                    nroFactura, nombreComprador, nombreSolicitante, fechaFactura, fechaAddFactura);
            return true;
        }

        public void procesarSharepointTesoreria(string ID, string motivo, string departamentoSiguiente, string nombreProcesado, string idMotivo, string departamentoActual, string usu, string pass,
           string comentario, string ruta, string nombreDepartamentoSiguiente, string tipoMotivo, string rutaVirtual, string nombre, string extension, string enviarCorreo)
        {
            try
            {
                string URL_status = ConfigurationManager.AppSettings.Get("UrlSharePoint");
                using (ClientContext clientContext = new ClientContext(URL_status))
                {
                    GestionDocumentosBLL ges = new GestionDocumentosBLL();
                    SharePointBLL sharePoint = new SharePointBLL();
                    DatosPrincipalesPorIdModels datosPrincipal = new DatosPrincipalesPorIdModels();
                    // SharePoint Online Credentials    
                    //clientContext.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                    clientContext.Credentials = new NetworkCredential(usu, pass);
                    Web web = clientContext.Web;

                    clientContext.Load(web);
                    clientContext.ExecuteQuery();

                    Web myweb = clientContext.Web;
                    List oList = clientContext.Web.Lists.GetByTitle(ConfigurationManager.AppSettings.Get("NombreListaTrabajo"));
                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem oListItem = oList.GetItemById(ID);
                    oListItem["docUltimaAccion"] = motivo;
                    oListItem["docDepartamento"] = departamentoSiguiente;
                    oListItem["docFechaUltimaAccion"] = DateTime.Now;
                    oListItem["docUltimoUsuarioAccion"] = nombreProcesado;
                    oListItem["docIdUltimaAccion"] = idMotivo;
                    oListItem["docDepartamentoAnterior"] = departamentoActual;
                    oListItem["docUltimoComentario"] = comentario;
                    oListItem["docNombreDepartamento"] = nombreDepartamentoSiguiente;
                    oListItem["docUltimoTipoMovimiento"] = tipoMotivo;
                    oListItem["envioCorreo"] = enviarCorreo;

                    if (departamentoActual == "Impuestos")
                    {
                        oListItem["impProcesadoPor"] = usu;
                        oListItem["impProcesadoNombre"] = nombreProcesado;
                        oListItem["impAccion"] = motivo;
                        oListItem["impProcesado"] = DateTime.Now;
                        oListItem["imprComentario"] = comentario;
                    }

                    if (departamentoActual == "Tesoreria")
                    {
                        oListItem["tesoProcesadoPor"] = usu;
                        oListItem["tesoProcesadoNombre"] = nombreProcesado;
                        oListItem["tesoAccion"] = motivo;
                        oListItem["tesoComentario"] = comentario;
                        oListItem["tesoProcesado"] = DateTime.Now;
                    }


                    if (departamentoSiguiente == "Recepcion")
                    {
                        oListItem["recepRecibido"] = DateTime.Now;
                    }

                    if (departamentoSiguiente == "Impuestos")
                    {
                        oListItem["impRecibido"] = DateTime.Now;
                    }

                    if (departamentoSiguiente == "CuentaPagar")
                    {
                        oListItem["cuentaPagarRecibido"] = DateTime.Now;
                    }

                    if (departamentoSiguiente == "RevisionCompras")
                    {
                        oListItem["revComprasRecibido"] = DateTime.Now;
                    }

                    if (departamentoSiguiente == "Solicitante")
                    {
                        oListItem["solRecibido"] = DateTime.Now;
                    }

                    if (departamentoSiguiente == "Tesoreria")
                    {
                        oListItem["tesoRecibido"] = DateTime.Now;
                    }

                    oListItem.Update();
                    clientContext.Load(oListItem);
                    clientContext.ExecuteQuery();

                    if (ruta != "")
                    {
                        var json = sharePoint.RetornaDatosPorID(ID, usu, pass);
                        datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesPorIdModels>(json);


                        ges.InsertaDatosDocumentos(datosPrincipal.Title, nombre + " " + DateTime.Now.ToString("dd-MM-yyyy") + extension, ruta,
                                rutaVirtual, "Tesoreria", datosPrincipal.RecepNroFactura, "", "NO");
                        //FileStream stream = new FileStream(ruta, FileMode.Open);
                        //AttachmentCreationInformation attInfo = new AttachmentCreationInformation();
                        //attInfo.FileName = stream.Name;
                        //attInfo.ContentStream = stream;

                        //oListItem.AttachmentFiles.Add(attInfo);

                        //clientContext.Load(oListItem);
                        //clientContext.ExecuteQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void insertarcomentarioTesoreria(string ID, string motivo, string departamentoSiguiente, string nombreProcesado, string idMotivo, string departamentoActual, string usu, string pass,
           string comentario, string ruta, string nombreDepartamentoSiguiente, string tipoMotivo, string nroOC)
        {
            LogsBLL logs = new LogsBLL();
            logs.insertarComentario(nombreProcesado, Convert.ToInt32(ID), comentario, nroOC, tipoMotivo, motivo, departamentoActual, nombreDepartamentoSiguiente);
        }

        [WebMethod]
        public bool enviarRevisionTesoreria(string ID, string usu, string pass, string departamento, string departamentoNombre, string adjuntarDocumento, string ruta, string rutaVirtual,
                                            string nombre, string extension, string nombreProcesado, string nroOC, string enviarCorreo, string nroFactura, string nombreComprador,
                                            string solicitanteNombre, string fechaFactura, string fechaAddFactura)
        {
            tesoreriaEnviarRevisionTesoreria(ID, usu, pass, departamento, departamentoNombre, adjuntarDocumento, ruta, rutaVirtual, nombre, extension, enviarCorreo, "Tesorería");

            LogsBLL logs = new LogsBLL();
            logs.insertarComentario(nombreProcesado, Convert.ToInt32(ID), "Envio a revisión por tesoreria", nroOC, "Enviado", "Procesado", "Tesorería", "Revisión tesorería");

            logs.insertarDatosReporteStatusFacturaPowerBI("Tesorería", "Revisión tesorería", Convert.ToInt32(ID), nombreProcesado, "Enviado", "Procesado", nroOC,
            nroFactura, nombreComprador, solicitanteNombre, Convert.ToDateTime(fechaFactura).ToString().Substring(0,10), Convert.ToDateTime(fechaAddFactura));
            
            return true;
        }

        public void tesoreriaEnviarRevisionTesoreria(string ID, string usu, string pass, string departamento, string departamentoNombre, string adjuntarDocumento, string ruta, string rutaVirtual,
                                            string nombre, string extension, string enviarCorreo, string departamentoAnterior)
        {
            GestionDocumentosBLL ges = new GestionDocumentosBLL();
            SharePointBLL sharePoint = new SharePointBLL();
            DatosPrincipalesPorIdModels datosPrincipal = new DatosPrincipalesPorIdModels();
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
                    ListItem oListItem = oList.GetItemById(ID);
                    oListItem["docDepartamento"] = departamento;
                    oListItem["docNombreDepartamento"] = departamentoNombre;
                    oListItem["envioCorreo"] = enviarCorreo;
                    oListItem["docDepartamentoAnterior"] = departamentoAnterior;
                    if (adjuntarDocumento == "SI")
                    {
                        oListItem["tesoArchivoAdjuntado"] = adjuntarDocumento;
                    }

                    oListItem.Update();
                    clientContext.Load(oListItem);
                    clientContext.ExecuteQuery();

                    if (adjuntarDocumento == "SI")
                    {
                        var json = sharePoint.RetornaDatosPorID(ID, usu, pass);
                        datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesPorIdModels>(json);


                        ges.InsertaDatosDocumentos(datosPrincipal.Title, nombre + " " + DateTime.Now.ToString("dd-MM-yyyy") + extension, ruta,
                                rutaVirtual, "Tesoreria", datosPrincipal.RecepNroFactura, "", "NO");
                        //FileStream stream = new FileStream(ruta, FileMode.Open);
                        //AttachmentCreationInformation attInfo = new AttachmentCreationInformation();
                        //attInfo.FileName = stream.Name;
                        //attInfo.ContentStream = stream;

                        //oListItem.AttachmentFiles.Add(attInfo);

                        //clientContext.Load(oListItem);
                        //clientContext.ExecuteQuery();
                    }


                }
            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public JsonResult retornaDatosTesoreria(string usu, string pass, string ID)
        {
            try
            {
                var siteUrl = "https://aplicaciones.cysa.com.py/_api/web/lists/getbytitle('" + ConfigurationManager.AppSettings.Get("NombreListaTrabajo") + "')/items?$select= Title,recepTipoDocumento,tesoNroRetencion,tesoArchivoAdjuntado,recepNroFactura,comCreadoNombre,comSolicitanteNombre,recepFechaFactura,recepFechaAddFactura &$filter=((ID eq '" + ID + "'))";

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

                string json = response.Content;

                var aaa = new JsonResult() { Data = json, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                return aaa;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [WebMethod]
        public JsonResult verificaSiFacturaExisteOC(string nroOc, string nroFactura, string usu, string pass)
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

                string json = response.Content;

                var aaa = new JsonResult() { Data = json, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                return aaa;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [WebMethod]
        public DropDownListModel[] retornaDatosProveedorBuscador(string datos)
        {
            DataTable dt = new DataTable();
            List<DropDownListModel> details = new List<DropDownListModel>();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaBuscadorProveedor", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@datos", datos);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }

                foreach (DataRow dtRow in dt.Rows)
                {
                    DropDownListModel Listado = new DropDownListModel();
                    Listado.id = dtRow["ID"].ToString();
                    Listado.value = dtRow["DATOS"].ToString();
                    details.Add(Listado);
                }
            }
            catch (Exception ex)
            {

            }
            return details.ToArray();
        }
        public string retornaUrlArchivo(string nroFactura, string nroOC)
        {
            var url = "";

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_getDocumentoPDF", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nroOC", nroOC);
                    cmd.Parameters.AddWithValue("@nroFactura", nroFactura);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        url = rdr.GetString(0);
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return url;
        }

        [WebMethod]
        public MensajesModels notificarSolicitanteCostosIndirectosCargados(string nroOC, string usuario, string pass, string nombreUsuario)
        {
            SharePointBLL sp = new SharePointBLL();
            MensajesModels mensajes = new MensajesModels();
            LogsBLL logs = new LogsBLL();
            List<DatosPrincipalesSharePointModels> lista = new List<DatosPrincipalesSharePointModels>();
            DatosPrincipalesSharePointModels datosPrincipal = new DatosPrincipalesSharePointModels();
            var contador = 0;
            try
            {
                string json = sp.getVerificaSiExisteNroOC(nroOC, usuario, pass);

                datosPrincipal = JsonConvert.DeserializeObject<DatosPrincipalesSharePointModels>(json);

                lista.Add(datosPrincipal);

                foreach (DatosPrincipalesSharePointModels p in lista)
                {
                    foreach (Value v in p.Values)
                    {
                        if (contador == 0)
                        {
                            notificarSolicitanteCostoIndirectoCargado(v.Id.ToString(), usuario, pass, "SI_NOTIFICAR", nombreUsuario);
                            logs.insertarComentario(nombreUsuario, Convert.ToInt32(v.Id.ToString()), "Notificación de costos indirectos cargados", nroOC, "Enviado", "Notificado", "Compras", "Compras");
                            contador = 1;
                        }
                        else
                        {
                            notificarSolicitanteCostoIndirectoCargado(v.Id.ToString(), usuario, pass, "NO_NOTIFICAR", nombreUsuario);
                            logs.insertarComentario(nombreUsuario, Convert.ToInt32(v.Id.ToString()), "Notificación de costos indirectos cargados", nroOC, "Enviado", "Notificado", "Compras", "Compras");
                        }
                    }
                }
                mensajes.estado = true;
                return mensajes;
            }
            catch (Exception ex)
            {
                mensajes.estado = false;
                mensajes.mensaje = ex.Message;
                return mensajes;
            }
        }

        public void notificarSolicitanteCostoIndirectoCargado(string ID, string usu, string pass, string enviarCorreo, string nombreUsuario)
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



                oListItem["docDepartamentoAnterior"] = "Compras";
                oListItem["docUltimaAccion"] = "Notificación de costos indirectos cargados";
                oListItem["docUltimoUsuarioAccion"] = nombreUsuario;
                oListItem["docUltimoComentario"] = "Notificación de costos indirectos cargados en SAP";
                oListItem["comNotificarCargaCostoIndirecto"] = "Notificar";
                oListItem["envioCorreo"] = enviarCorreo;
                oListItem["docFechaUltimaAccion"] = DateTime.Now;


                oListItem.Update();
                clientContext.Load(oListItem);
                clientContext.ExecuteQuery();
            }
        }

        private void guardarDatosReporteStatusFacturaPowerBI(string ID, string motivo, string departamentoSiguiente, string nombreProcesado, string idMotivo,
            string departamentoActual, string usu, string pass, string comentario, string nrooc, string nombreDepartamento, string tipoMotivo, string nroFactura, string nombreComprador,
            string solicitanteNombre, string fechaFactura, string fechaAddFactura)
        {
            LogsBLL logs = new LogsBLL();
            try
            {
                logs.insertarDatosReporteStatusFacturaPowerBI(departamentoActual, departamentoSiguiente, Convert.ToInt32(ID), nombreProcesado, tipoMotivo, motivo, nrooc,
                    nroFactura, nombreComprador, solicitanteNombre, Convert.ToDateTime(fechaFactura).ToString("dd/MM/yyyy"), Convert.ToDateTime(fechaAddFactura));

            }
            catch (Exception ex)
            {

            }
        } 

        [WebMethod]
        public DropDownListModel[] retornaDatosDepartamentoGestion(string departamentoActual, string tipoGestion)
        {
            DataTable dt = new DataTable();
            List<DropDownListModel> details = new List<DropDownListModel>();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_filtroDepartamentos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@departamento", departamentoActual);
                    cmd.Parameters.AddWithValue("@TipoGestion", tipoGestion);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }

                foreach (DataRow dtRow in dt.Rows)
                {
                    DropDownListModel Listado = new DropDownListModel();
                    Listado.id = dtRow["fNombreCorto"].ToString();
                    Listado.value = dtRow["fNombre"].ToString();
                    details.Add(Listado);
                }
            }
            catch (Exception ex)
            {

            }
            return details.ToArray();
        }
        [WebMethod]
        public DropDownListModel[] retornaDatosMovimientos(string departamentoActual, string tipoMovimiento, string departamentoSiguiente)
        {
            DataTable dt = new DataTable();
            List<DropDownListModel> details = new List<DropDownListModel>();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_filtraMovimientos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@departamentoActual", departamentoActual);
                    cmd.Parameters.AddWithValue("@tipoMovimiento", tipoMovimiento);
                    cmd.Parameters.AddWithValue("@departamentoSiguiente", departamentoSiguiente);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }

                foreach (DataRow dtRow in dt.Rows)
                {
                    DropDownListModel Listado = new DropDownListModel();
                    Listado.id = dtRow["movID"].ToString();
                    Listado.value = dtRow["movDescripcion"].ToString();
                    details.Add(Listado);
                }
            }
            catch (Exception ex)
            {

            }
            return details.ToArray();
        }

        [WebMethod]
        public bool procesarMasivamenteRevisionCompras(string ID, string comentario, string nombreMotivo, string departamento, string idMotivo,
                                                       string nombreDepartamentoSiguiente, string tipoMotivo, string nroOC, string nroFactura, string nombreComprador,
                                                       string nombreSolicitante, string fechaFactura, string fechaAddFactura, string usu, string pass, string nombreUsuario,
                                                       string nombreDepartamentoAnterior)
        {
            bool bandera = true;
            try
            {
                sharePoint.ActualizaMotivosRevisionCompras(Convert.ToInt64(ID), usu, pass, comentario,
                           nombreUsuario, nombreMotivo, departamento, idMotivo, nombreDepartamentoSiguiente, "SI", nombreDepartamentoAnterior, tipoMotivo);

                lg.insertarComentario(nombreUsuario, Convert.ToInt32(ID), comentario, nroOC, tipoMotivo, nombreMotivo, nombreDepartamentoAnterior, nombreDepartamentoSiguiente);

                lg.insertarDatosReporteStatusFacturaPowerBI(nombreDepartamentoAnterior, nombreDepartamentoSiguiente, Convert.ToInt32(ID), nombreUsuario,
                tipoMotivo, nombreMotivo, nroOC, nroFactura, nombreComprador, nombreSolicitante, Convert.ToDateTime(fechaFactura).ToString("dd/mm/yyyy"), Convert.ToDateTime(fechaAddFactura == null ? DateTime.Now.ToString() : fechaAddFactura));

            }
            catch (Exception ex)
            {
                bandera = false;
            }

            return bandera;
        }
    }

}

