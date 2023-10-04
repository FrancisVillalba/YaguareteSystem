using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Clases
{
    public class EtiquetaBLL
    {
        public DataTable RetornaDatosImprimirEtiqueta(int materialID)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaDatosImpEtiqueta", conexion);
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
    }
}