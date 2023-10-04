using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Clases
{
    public class LogsBLL
    {
        public int registrarLogs(string usuario, string modulo, string comentario)
        {
            int FaseID = 0;
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_insertaLogs", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@modulo", modulo);
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
            catch
            {

            }

            return FaseID;
        }
        public int insertarComentario(string usuario, int idSharepoint, string comentario, string nrooc, string tipoMotivo, string motivo, string departamento, string  departamentoSiguiente)
        {
            int FaseID = 0;
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_insertaComentarios", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@idSharepoint", idSharepoint);
                    cmd.Parameters.AddWithValue("@comentario", comentario);
                    cmd.Parameters.AddWithValue("@comNroOc", nrooc);
                    cmd.Parameters.AddWithValue("@tipoMotivo", tipoMotivo);
                    cmd.Parameters.AddWithValue("@motivo", motivo);
                    cmd.Parameters.AddWithValue("@departamento", departamento);
                    cmd.Parameters.AddWithValue("@departamentoSiguiente", departamentoSiguiente);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        FaseID = rdr.GetInt32(0);
                    }
                    conexion.Close();
                }
            }
            catch
            {

            }

            return FaseID;
        }

        public DataTable getListaComentario(string nroOC, int idSharepoint)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_listaComentario", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nrooc", nroOC);
                    cmd.Parameters.AddWithValue("@idSharepoint", idSharepoint); 
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

        public int insertarDatosReporteStatusFacturaPowerBI(string departamentoLlegada, string departamentoEnviada, int idSharePoint, string usuarioProcesado, string tipoMotivo, 
                                        string motivo, string nroOC, string nroFactura, string comprador, string solicitante, string fechaFactura, DateTime fechaCargaFactura)
        {
            int FaseID = 0;
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_insertarDatosTableroStatusFactura", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@departamentoLlegada", departamentoLlegada);
                    cmd.Parameters.AddWithValue("@departamentoEnviada", departamentoEnviada);
                    cmd.Parameters.AddWithValue("@idSharePoint", idSharePoint);
                    cmd.Parameters.AddWithValue("@usuarioProcesada", usuarioProcesado);
                    cmd.Parameters.AddWithValue("@tipoMotivo", tipoMotivo);
                    cmd.Parameters.AddWithValue("@motivo", motivo);
                    cmd.Parameters.AddWithValue("@nroOC", nroOC);
                    cmd.Parameters.AddWithValue("@nroFactura", nroFactura);
                    cmd.Parameters.AddWithValue("@comprador", comprador);
                    cmd.Parameters.AddWithValue("@solicitante", solicitante);
                    cmd.Parameters.AddWithValue("@fechaFactura", fechaFactura);
                    cmd.Parameters.AddWithValue("@fechaCargaFactura", fechaCargaFactura); 
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        FaseID = rdr.GetInt32(0);
                    }
                    conexion.Close();
                }
            }
            catch(Exception ex)
            {

            }

            return FaseID;
        }

    }
}