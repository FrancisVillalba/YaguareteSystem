using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Clases
{
    public class ChequeBLL
    {
        public DataTable RetornaDatosCheque(int codCheque)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringCysaCheque"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaDatosCheque", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codCuentaCorriente", codCheque);
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

        public DataTable RetornaDatosImpresionCheque(int codCheque)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringCysaCheque"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaDatosImpresionCheque", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codCheque", codCheque);
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

        public DataTable RetornaDatosChequeYFactura(int codCheque)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringCysaCheque"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaDatosChequeFactura", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codCheque", codCheque);
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

        public int InsertarDatosCheque(string numCheque, int codCuentaCorriente, string rucProveedor, DateTime fechaEmision, DateTime fechaPago, double montoCheque,
            int diferido, int porcentajeIvaApli, int porcentajeRentaApli, double montoTotalFactura10, double montoTotalFactura5, int anulado, int impresoCheque, int impresoRetencion,
            int emailNotificacion, DateTime fechaPromFactura, double montoTotalFacturaExcenta, string accion, int codCheque)
        {
            int FaseID = 0;
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringCysaCheque"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_cargaDatosCheque", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@numCheque", numCheque);
                    cmd.Parameters.AddWithValue("@codigoCuentaCorriente", codCuentaCorriente);
                    cmd.Parameters.AddWithValue("@rucProveedor", rucProveedor);
                    cmd.Parameters.AddWithValue("@fechaEmision", fechaEmision);
                    cmd.Parameters.AddWithValue("@fechaPago", fechaPago);
                    cmd.Parameters.AddWithValue("@montoCheque", montoCheque.ToString());
                    cmd.Parameters.AddWithValue("@diferido", diferido);
                    cmd.Parameters.AddWithValue("@porcentajeIvaAplicado", porcentajeIvaApli);
                    cmd.Parameters.AddWithValue("@porcentajeRentaAplicado", porcentajeRentaApli);
                    cmd.Parameters.AddWithValue("@montoTotalFactura10", montoTotalFactura10);
                    cmd.Parameters.AddWithValue("@montoTotalFactura5", montoTotalFactura5);
                    cmd.Parameters.AddWithValue("@anulado", anulado);
                    cmd.Parameters.AddWithValue("@impresoCheque", impresoCheque);
                    cmd.Parameters.AddWithValue("@impresoRetencion", impresoRetencion);
                    cmd.Parameters.AddWithValue("@emailNotificado", emailNotificacion);
                    cmd.Parameters.AddWithValue("@fechaPromedioFactura", fechaPromFactura);
                    cmd.Parameters.AddWithValue("@montoTotalFacturaExenta", montoTotalFacturaExcenta.ToString());
                    cmd.Parameters.AddWithValue("@accion", accion);
                    cmd.Parameters.AddWithValue("@codCheque", codCheque);
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

        public int InsertarDatosFactura(string codCheque, string numFactura, DateTime fechaFactura, double montoFactura10, double montoFactura5, double montoFacturaExenta, string accion)
        {
            int FaseID = 0;
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringCysaCheque"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_cargaDatosFactura", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigoCheque", codCheque);
                    cmd.Parameters.AddWithValue("@numFactura", numFactura);
                    cmd.Parameters.AddWithValue("@fechaFactura", fechaFactura);
                    cmd.Parameters.AddWithValue("@montoFactura10", montoFactura10);
                    cmd.Parameters.AddWithValue("@montoFactura5", montoFactura5);
                    cmd.Parameters.AddWithValue("@montoFacturaExenta", montoFacturaExenta.ToString());
                    cmd.Parameters.AddWithValue("@accion", accion);
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

        public DataTable AbmProveedor(string ruc, string proveedor, string accion)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringCysaCheque"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_abmProveedor", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ruc", ruc);
                    cmd.Parameters.AddWithValue("@nomProveedor", proveedor);
                    cmd.Parameters.AddWithValue("@accion", accion);
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