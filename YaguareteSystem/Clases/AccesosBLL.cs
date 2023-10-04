using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Clases
{
    public class AccesosBLL
    {
        //Si retorna valor 1 es porque tiene permiso y si retorna valor 0 es porque no tiene permiso
        public int ControlaAcceso(string nomInterfaz, string roll)
        {
            int FaseID = 0;
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringSecurity"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_Accesos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nomInterfaz", nomInterfaz);
                    cmd.Parameters.AddWithValue("@roll", roll);
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

        public DataTable RetornaRollxInterfaz(string nomInterfaz)
        {
            DataTable dt = new DataTable();
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringSecurity"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaRollPorInterfaz", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nomInterfaz", nomInterfaz);
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

        public string RetornaDepartamentoPorRol(string rol)
        {
            string FaseID = "";
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringSecurity"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaDepartamentoPorRol", conexion);
                    cmd.CommandType = CommandType.StoredProcedure; 
                    cmd.Parameters.AddWithValue("@rol", rol);
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