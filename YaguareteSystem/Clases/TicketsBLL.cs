using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Clases
{
    public class TicketsBLL
    {
        public int InsertarSolicitudTicket(int numTicket, string comentario, int worflowCab, int worflowFaseID, int worflowMovID, string usuADD)
        {
            int FaseID = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertSolicitudTicket", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@numTicket", numTicket);
                    cmd.Parameters.AddWithValue("@comentario", comentario);
                    cmd.Parameters.AddWithValue("@worflowCab", worflowCab);
                    cmd.Parameters.AddWithValue("@worflowFaseID", worflowFaseID);
                    cmd.Parameters.AddWithValue("@worflowMovID", worflowMovID);
                    cmd.Parameters.AddWithValue("@usuAdd", usuADD);
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
        public int RetorWorflowCabID(string usuInicioSesion)
        {
            int FaseID = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringSecurity"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaWorflowCabID", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuInicioSesion", usuInicioSesion);
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
        public int RetorFaseInicioID(string usuInicioSesion, int wfCabID)
        {
            int FaseID = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringSecurity"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaFaseInicioID", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuInicioSesion", usuInicioSesion);
                    cmd.Parameters.AddWithValue("@wfCabID", wfCabID);
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
        public int RetorMovimientoID(int wfFaseID)
        {
            int FaseID = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringSecurity"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaMovimentoID", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@wfFaseID", wfFaseID);
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
        public DataTable RetornaDatosSolicitudTicketes(int nroTickets)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaDatosSolicitudTicket", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@solID", nroTickets);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
            }
            catch
            {

            }
            return dt;
        }
        public string RetornaTipoMovimiento(int movID)
        {
            string FaseID = "";
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringSecurity"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaAccionMovimentoID", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@movID", movID);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        FaseID = rdr.GetString(0);
                    }

                    conexion.Close();
                }
            }
            catch
            {

            }

            return FaseID;
        }
    }
}