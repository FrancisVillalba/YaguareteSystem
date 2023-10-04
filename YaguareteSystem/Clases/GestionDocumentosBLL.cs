using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Clases
{
    public class GestionDocumentosBLL
    {
        public int InsertaOrdenCompras(string nroOC, string tipoDocumento, int proveedorID, string tipoMoneda, string montoTotal, string codEmpresa,
                string usuSolicitante, string nroSP, string comentario, string usuario, string nombreSolicitante, string nombreUsuarioAdd, string esOrdenInterna, string nroOrdenInterna,
                string esDeg, string contratoMarco, string esDireccionado)
        {
            int FaseID = 0;
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertOrdenCompras", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nroOC", nroOC);
                    cmd.Parameters.AddWithValue("@tipoDocumento", tipoDocumento);
                    cmd.Parameters.AddWithValue("@proveedorID", proveedorID);
                    cmd.Parameters.AddWithValue("@tipoMonedaID", tipoMoneda);
                    cmd.Parameters.AddWithValue("@montoTotal", montoTotal);
                    cmd.Parameters.AddWithValue("@codEmpresa", codEmpresa);
                    cmd.Parameters.AddWithValue("@usuSolicitante", usuSolicitante);
                    cmd.Parameters.AddWithValue("@nroSP", nroSP);
                    cmd.Parameters.AddWithValue("@comentario", comentario);
                    cmd.Parameters.AddWithValue("@usuAdd", usuario);
                    cmd.Parameters.AddWithValue("@nomSolicitante", nombreSolicitante);
                    cmd.Parameters.AddWithValue("@nomUsuAdd", nombreUsuarioAdd);
                    cmd.Parameters.AddWithValue("@esProyecto", esOrdenInterna);
                    cmd.Parameters.AddWithValue("@nroOrdenInterna", nroOrdenInterna);
                    cmd.Parameters.AddWithValue("@esDEG", esDeg);
                    cmd.Parameters.AddWithValue("@contratoMarco", contratoMarco);
                    cmd.Parameters.AddWithValue("@esDireccionado", esDireccionado);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        FaseID = rdr.GetInt32(0);
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return FaseID;
        }

        public int InsertaSeguimientoSolicitud(int nroOcID, int movimientoID, string usuAdd, string comentario)
        {
            int FaseID = 0;
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertSeguimientoComentario", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ordenCompraID", nroOcID);
                    cmd.Parameters.AddWithValue("@movID", movimientoID);
                    cmd.Parameters.AddWithValue("@usuAdd", usuAdd);
                    cmd.Parameters.AddWithValue("@comentario", comentario);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        FaseID = rdr.GetInt32(0);
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return FaseID;
        }

        public DataTable InsertaDatosDocumentos(string nroOC, string nombre, string rutaFisica, string rutaVirtual, string departamento, string nroFactura, string tipoDocAdjuntar,
            string esActualizacion)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertaDatosDocumentos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nroOC", nroOC);
                    cmd.Parameters.AddWithValue("@nomDocumento", nombre);
                    cmd.Parameters.AddWithValue("@rutaFisica", rutaFisica);
                    cmd.Parameters.AddWithValue("@rutaVirtual", rutaVirtual);
                    cmd.Parameters.AddWithValue("@Departamento", departamento);
                    cmd.Parameters.AddWithValue("@nroFactura", nroFactura);
                    cmd.Parameters.AddWithValue("@tipoDocAdjuntar", tipoDocAdjuntar);
                    cmd.Parameters.AddWithValue("@esActualizacion", esActualizacion);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public int EliminarDatosDocumentos(int nroOC)
        {
            int FaseID = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteDocumentos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@docID", nroOC);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        FaseID = rdr.GetInt32(0);
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return FaseID;
        }
        public DataTable RetornaDatosOrdenCompras(int ordenID)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaDatosCompras", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ordenID", ordenID);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public DataTable RetornaDatosOrdenComprasCompras(int ordenID)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaDatosCompras", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ordenID", ordenID);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public DataTable RetornaDatosPorProveedor(int ordenID)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaDatosProveedores", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ordenID);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        public DataTable RetornaDatosOrdenComprasXoc(string nroOC)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaDatosComprasXoc", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nroOC", nroOC);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public int ModificarOrdenCompras(int ordenID, string enUso, string nroOC, string tipoDocumento, int proveedorID, string tipoMoneda, string montoTotal, string codEmmpresa,
                                            string solicitante, string solicitanteNombre, string nroSP, string estado, string comentario, string esProyecto, string nroOrdenInterna, string esDeg,
                                            string contratoMarco, string esDireccionado)
        {
            int FaseID = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarDatosCompras", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ordenID", ordenID);
                    cmd.Parameters.AddWithValue("@enUso", enUso);
                    cmd.Parameters.AddWithValue("@nroOC", nroOC);
                    cmd.Parameters.AddWithValue("@tipoDocumento", tipoDocumento);
                    cmd.Parameters.AddWithValue("@proveedorID", proveedorID);
                    cmd.Parameters.AddWithValue("@tipoMoneda", tipoMoneda);
                    cmd.Parameters.AddWithValue("@montoTotal", montoTotal);
                    cmd.Parameters.AddWithValue("@codEmpresa", codEmmpresa);
                    cmd.Parameters.AddWithValue("@usuSolicitante", solicitante);
                    cmd.Parameters.AddWithValue("@usuNombreSolicitante", solicitanteNombre);
                    cmd.Parameters.AddWithValue("@nroSP", nroSP);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@comentario", comentario);
                    cmd.Parameters.AddWithValue("@esProyecto", esProyecto);
                    cmd.Parameters.AddWithValue("@nroOrdenInterna", nroOrdenInterna);
                    cmd.Parameters.AddWithValue("@esDeg", esDeg);
                    cmd.Parameters.AddWithValue("@contratoMarco", contratoMarco);
                    cmd.Parameters.AddWithValue("@esDireccionado", esDireccionado);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        FaseID = rdr.GetInt32(0);
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return FaseID;
        }

        public int controlCargaAdjunto(string nroOC)
        {
            int FaseID = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_controlCantidadObligatoriaAdjunto", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nroOC", nroOC);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        FaseID = rdr.GetInt32(0);
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return FaseID;
        }

        public DataTable RetornaDatosDocumentos(string nroOC, string departamento, string nroFactura)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_RetornaDatosDocumentos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nroOC", nroOC);
                    cmd.Parameters.AddWithValue("@departamento", departamento);
                    cmd.Parameters.AddWithValue("@nroFactura", nroFactura);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        public DataTable RetornaDatosDocumentosParaActualizar(string nroOC, string departamento, string nroFactura)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_RetornaDatosDocumentosParaActualizar", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nroOC", nroOC);
                    cmd.Parameters.AddWithValue("@departamento", departamento);
                    cmd.Parameters.AddWithValue("@nroFactura", nroFactura);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        public DataTable InsertaProveedor(string sap, string ruc, string rasonSocial, string correo)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertaProveedor", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sap", sap);
                    cmd.Parameters.AddWithValue("@ruc", ruc);
                    cmd.Parameters.AddWithValue("@rasonSocial", rasonSocial);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public int EditarProveedores(string sap, string ruc, string rasonSocial, string correo, int id)
        {
            DataTable dt = new DataTable();

            int FaseID = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_modificarProveedores", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@sap", sap);
                    cmd.Parameters.AddWithValue("@ruc", ruc);
                    cmd.Parameters.AddWithValue("@razonSocial", rasonSocial);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        FaseID = rdr.GetInt32(0);
                    }
                    conexion.Close();
                }

                return FaseID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public DataTable InsertaProveedorCompras(string sap, string ruc, string rasonSocial)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertaProveedorCompras", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sap", sap);
                    cmd.Parameters.AddWithValue("@ruc", ruc);
                    cmd.Parameters.AddWithValue("@rasonSocial", rasonSocial);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataTable GetDatosProveedor(int idProveedor)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaDatosProveedor", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", idProveedor);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataTable InsertaProveedorRecepcion(string ruc, string rasonSocial)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertaProveedorRecepcion", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ruc", ruc);
                    cmd.Parameters.AddWithValue("@rasonSocial", rasonSocial);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public int ActualizaUsoRecepcion(string nroOC)
        {
            int FaseID = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_actualizaUsoRecepcion", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nroOC", nroOC);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        FaseID = rdr.GetInt32(0);
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return FaseID;
        }

        public int updateDocumentoBanderaEsActualizacion(string nroOC)
        {
            int FaseID = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_updateDocumentoBanderaEsActualizacion", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nroOc", nroOC);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        FaseID = rdr.GetInt32(0);
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return FaseID;
        }

    }
}