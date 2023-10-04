using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Clases
{
    public class AlmacenBLL
    {
        public DataTable retornaDatosMaterial(string accion, int? materialID, string codEmpresa)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_datosMateriales", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@accion", accion);
                    cmd.Parameters.AddWithValue("@idMaterial", materialID);
                    cmd.Parameters.AddWithValue("@codEmpresa", codEmpresa);
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

        public DataTable getDatosMateriales(int? materialID)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_getDatosMateriales", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@materialID", materialID); 
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
       
        public int insertaDatosMateriales(string nombreMaterial, int idCentroCostos, int materialCodigo, string usuAdd, int cantidad, int unidadMedida, string ubicacion )
        { 
            int FaseID = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_CargMateriales", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@matNOMBRE", nombreMaterial);
                    cmd.Parameters.AddWithValue("@idCentroCostos", idCentroCostos);
                    cmd.Parameters.AddWithValue("@matCodigo", materialCodigo);
                    cmd.Parameters.AddWithValue("@usuAdd", usuAdd);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@uniMedida", unidadMedida); 
                    cmd.Parameters.AddWithValue("@ubicacion", ubicacion);
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
        public int modificaDatosMateriales(int matID, string nombreMaterial, int idCentroCostos, int materialCodigo, string usuMod, int cantidad, int unidadMedida, string ubicacion)
        { 
            int FaseID = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_modificarMaterial", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@matID", matID);
                    cmd.Parameters.AddWithValue("@matNOMBRE", nombreMaterial);
                    cmd.Parameters.AddWithValue("@idCentroCostos", idCentroCostos);
                    cmd.Parameters.AddWithValue("@matCodigo", materialCodigo);
                    cmd.Parameters.AddWithValue("@usuModificacion", usuMod);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@uniMedida", unidadMedida);
                    cmd.Parameters.AddWithValue("@ubicacion", ubicacion);
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

        public int actualizaCantidad(DataTable dt)
        {
            int FaseID = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    //SqlCommand cmd = new SqlCommand("sp_modificarMaterial", conexion);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@dt", dt); 
                    //
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_importaCantidadMaterial", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter tvparam = cmd.Parameters.AddWithValue("@dt", dt);
                    tvparam.SqlDbType = SqlDbType.Structured;
                    //cmd.ExecuteNonQuery(); 
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