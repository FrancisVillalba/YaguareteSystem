using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using YaguareteSystem.Models;

namespace YaguareteSystem.Clases
{
    public class ProcesosBLL
    {
        #region BoxSoft
        public DataSet sp_fechasFacturasTesaka(string fechaDesde, string codEmpresa)
        {
            OracleConnection cone = new OracleConnection(ConfigurationManager.ConnectionStrings["conStringOracle"].ConnectionString);
            OracleCommand coma = new OracleCommand("CSG.PKG_PROCESOS_SYSTEMA.SP_FECHAS_FACTURAS_TESAKA", cone);
            coma.CommandType = CommandType.StoredProcedure;

            coma.Parameters.Add("PI_FECHA_DESDE", fechaDesde); //nombre del servicio a configurar 
            coma.Parameters.Add("PI_COD_EMPRESA", codEmpresa);
            coma.Parameters.Add("PO_FECHAS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            DataSet dtRespuesta = new DataSet();
            OracleDataAdapter adap = new OracleDataAdapter(coma);

            try
            {
                adap.Fill(dtRespuesta);
                adap.SelectCommand.Connection.Close();
                adap.SelectCommand.Connection.Dispose();
            }
            catch (Exception e)
            {

            }

            return dtRespuesta;

        }
        public DataTable sp_retornaDatosDataTable(string fechaRetencion, string fechaAutofactura, string codEmpresa)
        {
            //OracleConnection con = new OracleConnection();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["conStringOracle"].ConnectionString;
            //con.Open();

            //OracleCommand cmd = con.CreateCommand();

            string sqlText = "SELECT * FROM(SELECT DISTINCT'NO_CONTRIBUYENTE' info_situacion,BS_LFRT.BEZ_1 info_nombre,BS_LFRT.UST_IDENT_NR info_identificacion," +
                "'CEDULA'info_tipoIdentificacion," +
                "'' info_domicilio,'' info_direccion,'' info_pais,'' info_telefono,'retenciones@cysa.com.py' info_correoElectronico,5 tran_tipoComprobante," +
                " replace( M689_FORMATTED_RE_NR.RE_NR_GOVERNMENT, '_','-') tran_numeroComprobanteVenta,  " +
                " to_char(BS_EK_RE.EK_RE_DATUM, 'YYYY-MM-DD') tran_fecha,CSG.M689_FORMATTED_RE_NR.TIMBRADO_NUMBER tran_numeroTimbrado,'CONTADO' tran_condicionCompra,to_char(TO_DATE('" + fechaRetencion + "','DD/MM/YYYY'), 'YYYY-MM-DD') " +
                " rete_fecha,'PYG' rete_moneda," +
                "1 rete_retencionRenta,'RENTA_EMPRESARIAL.1' rete_conceptoRenta,0 rete_retencionIva,'' rete_conceptoIva,1.5 rete_rentaPorcentaje," +
                "0 rete_rentaCabezasBase,0 rete_rentaCabezasCantidad,0 rete_rentaToneladasBase,0 rete_rentaToneladasCantidad,0 rete_ivaPorcentaje5," +
                "0 rete_ivaPorcentaje10,1 deta_cantidad,SYS_RECH_BETRAG deta_precioUnitario,0 deta_tasaAplica,'Compra papel en desuso' deta_descripcion " +
                "FROM((((BS_EK_RE INNER JOIN CSG.BS_EK_RE_POS ON BS_EK_RE.EK_RE_KEY = BS_EK_RE_POS.EK_RE_KEY) INNER JOIN CSG.CS_ZAHLBED " +
                "ON(BS_EK_RE.ZAHLBED_KEY = CS_ZAHLBED.ZAHLBED_KEY) AND(BS_EK_RE.MANDANT = CS_ZAHLBED.MANDANT)) LEFT OUTER JOIN CSG.M689_FORMATTED_RE_NR " +
                "ON BS_EK_RE.EK_RE_KEY = M689_FORMATTED_RE_NR.RE_KEY) INNER JOIN CSG.BS_MATERIAL ON((BS_EK_RE.MANDANT = BS_MATERIAL.MANDANT) " +
                "AND(BS_EK_RE.FIRMA = BS_MATERIAL.FIRMA)) AND(BS_EK_RE_POS.MATERIAL_KEY = BS_MATERIAL.MATERIAL_KEY)) INNER JOIN CSG.BS_LFRT " +
                "ON((BS_EK_RE.MANDANT = BS_LFRT.MANDANT) AND(BS_EK_RE.FIRMA = BS_LFRT.FIRMA)) AND(BS_EK_RE.LFRT_KEY = BS_LFRT.LFRT_KEY) " +
                "WHERE SYS_RECH_BETRAG > 0 " +
                "AND(BS_EK_RE.EK_RE_DATUM BETWEEN TO_DATE('" + fechaAutofactura + "', 'dd.mm.yyyy') AND TO_DATE('" + fechaAutofactura + "', 'dd.mm.yyyy')) " +
                "AND BS_EK_RE.U_FIRMA = '" + codEmpresa + "' AND(BS_EK_RE.VORGANGS_ART = 'G21' OR BS_EK_RE.VORGANGS_ART = 'G22') " +
                "AND EK_RE_AN_FIBU = 1 " +
                "AND BS_MATERIAL.MATERIAL_KEY NOT IN(9318636, 9318588, 9318584, 9318637, 9318912, 9318589) ) " +
                "TESAKA WHERE NOT EXISTS( SELECT* FROM RV_TESAKA_RESULT WHERE RV_TESAKA_RESULT.FACTURA= " +
                "TESAKA.tran_numeroComprobanteVenta )";

            OracleConnection cone = new OracleConnection(ConfigurationManager.ConnectionStrings["conStringOracle"].ConnectionString);
            OracleCommand coma = new OracleCommand(sqlText, cone);
            DataTable dtRespuesta = new DataTable();
            OracleDataAdapter adap = new OracleDataAdapter(coma);

            try
            {
                adap.Fill(dtRespuesta);
                adap.SelectCommand.Connection.Close();
                adap.SelectCommand.Connection.Dispose();
            }
            catch (Exception e)
            {

            }

            return dtRespuesta;

        }
        public byte[] sp_retornaDatosJsonImportacion(string fechaRetencion, string procesos)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conStringOracle"].ConnectionString;
            con.Open();

            OracleCommand cmd = con.CreateCommand();

            string sqlText = "SELECT * FROM(SELECT DISTINCT BS_EK_RE.EK_RE_NR, 'NO_CONTRIBUYENTE' info_situacion,BS_LFRT.BEZ_1 info_nombre,BS_LFRT.UST_IDENT_NR info_identificacion,'CEDULA'info_tipoIdentificacion,'' info_domicilio," +
              "'' info_direccion,'' info_pais,'' info_telefono,'retenciones@cysa.com.py' info_correoElectronico,5 tran_tipoComprobante, replace( M689_FORMATTED_RE_NR.RE_NR_GOVERNMENT, '_','-') tran_numeroComprobanteVenta, to_char(BS_EK_RE.EK_RE_DATUM, 'YYYY-MM-DD') tran_fecha,'11884116' tran_numeroTimbrado," +
              "  'CONTADO' tran_condicionCompra,to_char(TO_DATE('" + fechaRetencion + "', 'DD/MM/YYYY'), 'YYYY-MM-DD')  rete_fecha,'PYG' rete_moneda,1 rete_retencionRenta,'RENTA_EMPRESARIAL.1' rete_conceptoRenta," +
              "  0 rete_retencionIva,'' rete_conceptoIva,1.5 rete_rentaPorcentaje,0 rete_rentaCabezasBase,0 rete_rentaCabezasCantidad,0 rete_rentaToneladasBase,0 rete_rentaToneladasCantidad," +
              "  0 rete_ivaPorcentaje5,0 rete_ivaPorcentaje10,1 deta_cantidad,SYS_RECH_BETRAG deta_precioUnitario,0 deta_tasaAplica,'Compra papel en desuso' deta_descripcion" +
              "  FROM((((BS_EK_RE INNER JOIN CSG.BS_EK_RE_POS ON BS_EK_RE.EK_RE_KEY = BS_EK_RE_POS.EK_RE_KEY) INNER JOIN CSG.CS_ZAHLBED ON(BS_EK_RE.ZAHLBED_KEY = CS_ZAHLBED.ZAHLBED_KEY)" +
              "  AND(BS_EK_RE.MANDANT = CS_ZAHLBED.MANDANT)) LEFT OUTER JOIN CSG.M689_FORMATTED_RE_NR ON BS_EK_RE.EK_RE_KEY = M689_FORMATTED_RE_NR.RE_KEY) INNER JOIN CSG.BS_MATERIAL" +
              " ON((BS_EK_RE.MANDANT = BS_MATERIAL.MANDANT) AND(BS_EK_RE.FIRMA = BS_MATERIAL.FIRMA)) AND(BS_EK_RE_POS.MATERIAL_KEY = BS_MATERIAL.MATERIAL_KEY)) INNER JOIN CSG.BS_LFRT" +
              "  ON((BS_EK_RE.MANDANT = BS_LFRT.MANDANT) AND(BS_EK_RE.FIRMA = BS_LFRT.FIRMA)) AND(BS_EK_RE.LFRT_KEY = BS_LFRT.LFRT_KEY)" +
              "  WHERE SYS_RECH_BETRAG > 0    AND EK_RE_AN_FIBU = 1" +
              " AND BS_MATERIAL.MATERIAL_KEY NOT IN(9318636, 9318588, 9318584, 9318637, 9318912, 9318589) ) TESAKA" +
              " WHERE  EK_RE_NR in " +
              "(" + procesos + ")";


            cmd.CommandText = sqlText;
            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                var list = reader.Cast<IDataRecord>()
                             .Select(dr => new TesakaModels
                             {
                                 detalle = new List<TesakaModels.DetalleType>
                                 {
                                         new TesakaModels.DetalleType
                                         {
                                             cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("deta_cantidad"))), //dr.GetInt32(dr.GetOrdinal("deta_cantidad")),
                                             tasaAplica = Convert.ToString(dr.GetValue(dr.GetOrdinal("deta_tasaAplica"))),
                                             precioUnitario = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("deta_precioUnitario"))),
                                             descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("deta_descripcion")))
                                         }
                                 },
                                 retencion = new TesakaModels.RetencionType
                                 {
                                     fecha = Convert.ToString(dr.GetValue(dr.GetOrdinal("rete_fecha"))),
                                     moneda = Convert.ToString(dr.GetValue(dr.GetOrdinal("rete_moneda"))),
                                     retencionRenta = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("rete_retencionRenta"))),
                                     conceptoRenta = Convert.ToString(dr.GetValue(dr.GetOrdinal("rete_conceptoRenta"))),
                                     retencionIva = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("rete_retencionIva"))),
                                     conceptoIva = Convert.ToString(dr.GetValue(dr.GetOrdinal("rete_conceptoIva"))),
                                     rentaPorcentaje = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("rete_rentaPorcentaje"))),
                                     rentaCabezasBase = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_rentaCabezasBase"))),
                                     rentaCabezasCantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_rentaCabezasCantidad"))),
                                     rentaToneladasBase = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_rentaToneladasBase"))),
                                     rentaToneladasCantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_rentaToneladasCantidad"))),
                                     ivaPorcentaje5 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_ivaPorcentaje5"))),
                                     ivaPorcentaje10 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_ivaPorcentaje10")))
                                 },
                                 informado = new TesakaModels.InformadoType
                                 {
                                     situacion = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_situacion"))),
                                     tipoIdentificacion = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_tipoIdentificacion"))),
                                     identificacion = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_identificacion"))),
                                     nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_nombre"))),
                                     domicilio = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_domicilio"))),
                                     direccion = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_direccion"))),
                                     correoElectronico = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_correoElectronico"))),
                                     pais = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_pais"))),
                                     telefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_telefono")))
                                 },
                                 transaccion = new TesakaModels.TransaccionType
                                 {
                                     condicionCompra = Convert.ToString(dr.GetValue(dr.GetOrdinal("tran_condicionCompra"))),
                                     tipoComprobante = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("tran_tipoComprobante"))),
                                     numeroComprobanteVenta = Convert.ToString(dr.GetValue(dr.GetOrdinal("tran_numeroComprobanteVenta"))),
                                     fecha = Convert.ToString(dr.GetValue(dr.GetOrdinal("tran_fecha"))),
                                     numeroTimbrado = Convert.ToString(dr.GetValue(dr.GetOrdinal("tran_numeroTimbrado")))
                                 },
                                 atributos = new TesakaModels.AtributosType
                                 {
                                     fechaCreacion = DateTime.Now.ToString("yyyy-MM-dd")
                                 }
                             })
                             .ToList();
                JsonSerializer serializer;

                serializer = new JsonSerializer();
                //byte[] buffer = Encoding.UTF8.GetBytes(list.ToString());

                serializer.Formatting = Formatting.Indented;
                serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

                string json = JsonConvert.SerializeObject(list);
                byte[] byteArray = Encoding.UTF8.GetBytes(json);

                return byteArray;
            }

            return null;
        }
        public byte[] sp_retornaDatosJson(string fechaRetencion, string fechaAutofactura, string codEmpresa, string nomEmpresa)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conStringOracle"].ConnectionString;
            con.Open();

            OracleCommand cmd = con.CreateCommand();
            //ak
            string sqlText = "SELECT * FROM(SELECT DISTINCT'NO_CONTRIBUYENTE' info_situacion,BS_LFRT.BEZ_1 info_nombre,BS_LFRT.UST_IDENT_NR info_identificacion," +
                "'CEDULA'info_tipoIdentificacion," +
                "'' info_domicilio,'' info_direccion,'' info_pais,'' info_telefono,'retenciones@cysa.com.py' info_correoElectronico,5 tran_tipoComprobante," +
                " replace( M689_FORMATTED_RE_NR.RE_NR_GOVERNMENT, '_','-') tran_numeroComprobanteVenta,  " +
                " to_char(BS_EK_RE.EK_RE_DATUM, 'YYYY-MM-DD') tran_fecha,'11884116' tran_numeroTimbrado,'CONTADO' tran_condicionCompra,to_char(TO_DATE('" + fechaRetencion + "','DD/MM/YYYY'), 'YYYY-MM-DD') " +
                " rete_fecha,'PYG' rete_moneda," +
                "1 rete_retencionRenta,'RENTA_EMPRESARIAL.1' rete_conceptoRenta,0 rete_retencionIva,'' rete_conceptoIva,1.5 rete_rentaPorcentaje," +
                "0 rete_rentaCabezasBase,0 rete_rentaCabezasCantidad,0 rete_rentaToneladasBase,0 rete_rentaToneladasCantidad,0 rete_ivaPorcentaje5," +
                "0 rete_ivaPorcentaje10,1 deta_cantidad,SYS_RECH_BETRAG deta_precioUnitario,0 deta_tasaAplica,'Compra papel en desuso' deta_descripcion " +
                "FROM((((BS_EK_RE INNER JOIN CSG.BS_EK_RE_POS ON BS_EK_RE.EK_RE_KEY = BS_EK_RE_POS.EK_RE_KEY) INNER JOIN CSG.CS_ZAHLBED " +
                "ON(BS_EK_RE.ZAHLBED_KEY = CS_ZAHLBED.ZAHLBED_KEY) AND(BS_EK_RE.MANDANT = CS_ZAHLBED.MANDANT)) LEFT OUTER JOIN CSG.M689_FORMATTED_RE_NR " +
                "ON BS_EK_RE.EK_RE_KEY = M689_FORMATTED_RE_NR.RE_KEY) INNER JOIN CSG.BS_MATERIAL ON((BS_EK_RE.MANDANT = BS_MATERIAL.MANDANT) " +
                "AND(BS_EK_RE.FIRMA = BS_MATERIAL.FIRMA)) AND(BS_EK_RE_POS.MATERIAL_KEY = BS_MATERIAL.MATERIAL_KEY)) INNER JOIN CSG.BS_LFRT " +
                "ON((BS_EK_RE.MANDANT = BS_LFRT.MANDANT) AND(BS_EK_RE.FIRMA = BS_LFRT.FIRMA)) AND(BS_EK_RE.LFRT_KEY = BS_LFRT.LFRT_KEY) " +
                "WHERE SYS_RECH_BETRAG > 0 " +
                "AND(BS_EK_RE.EK_RE_DATUM BETWEEN TO_DATE('" + fechaAutofactura + "', 'dd.mm.yyyy') AND TO_DATE('" + fechaAutofactura + "', 'dd.mm.yyyy')) " +
                "AND BS_EK_RE.U_FIRMA = '" + codEmpresa + "' AND(BS_EK_RE.VORGANGS_ART = 'G21' OR BS_EK_RE.VORGANGS_ART = 'G22') " +
                "AND EK_RE_AN_FIBU = 1 " +
                "AND BS_MATERIAL.MATERIAL_KEY NOT IN(9318636, 9318588, 9318584, 9318637, 9318912, 9318589) ) " +
                "TESAKA WHERE NOT EXISTS( SELECT* FROM RV_TESAKA_RESULT WHERE RV_TESAKA_RESULT.FACTURA= " +
                "TESAKA.tran_numeroComprobanteVenta )";

            //string sqlText = "SELECT * FROM(SELECT DISTINCT BS_EK_RE.EK_RE_NR, 'NO_CONTRIBUYENTE' info_situacion,BS_LFRT.BEZ_1 info_nombre,BS_LFRT.UST_IDENT_NR info_identificacion,'CEDULA'info_tipoIdentificacion,'' info_domicilio," +
            //    "'' info_direccion,'' info_pais,'' info_telefono,'retenciones@cysa.com.py' info_correoElectronico,5 tran_tipoComprobante,CASE WHEN((M689_FORMATTED_RE_NR.FORMATTED_RE_NR IS NULL)" +
            //    "  OR SUBSTR(M689_FORMATTED_RE_NR.FORMATTED_RE_NR, 1, 8) = '999_999_') then BS_EK_RE.VORGANGS_NR else replace(SUBSTR(M689_FORMATTED_RE_NR.FORMATTED_RE_NR, 1, 8) || '0' ||" +
            //    "    SUBSTR(M689_FORMATTED_RE_NR.FORMATTED_RE_NR, 12), '_', '-') END tran_numeroComprobanteVenta, to_char(BS_EK_RE.EK_RE_DATUM, 'YYYY-MM-DD') tran_fecha,'11884116' tran_numeroTimbrado," +
            //    "  'CONTADO' tran_condicionCompra,to_char(TO_DATE('31/12/2021', 'DD/MM/YYYY'), 'YYYY-MM-DD')  rete_fecha,'PYG' rete_moneda,1 rete_retencionRenta,'RENTA_EMPRESARIAL.1' rete_conceptoRenta," +
            //    "  0 rete_retencionIva,'' rete_conceptoIva,1.5 rete_rentaPorcentaje,0 rete_rentaCabezasBase,0 rete_rentaCabezasCantidad,0 rete_rentaToneladasBase,0 rete_rentaToneladasCantidad," +
            //    "  0 rete_ivaPorcentaje5,0 rete_ivaPorcentaje10,1 deta_cantidad,SYS_RECH_BETRAG deta_precioUnitario,0 deta_tasaAplica,'Compra papel en desuso' deta_descripcion" +
            //    "  FROM((((BS_EK_RE INNER JOIN CSG.BS_EK_RE_POS ON BS_EK_RE.EK_RE_KEY = BS_EK_RE_POS.EK_RE_KEY) INNER JOIN CSG.CS_ZAHLBED ON(BS_EK_RE.ZAHLBED_KEY = CS_ZAHLBED.ZAHLBED_KEY)" +
            //    "  AND(BS_EK_RE.MANDANT = CS_ZAHLBED.MANDANT)) LEFT OUTER JOIN CSG.M689_FORMATTED_RE_NR ON BS_EK_RE.EK_RE_KEY = M689_FORMATTED_RE_NR.RE_KEY) INNER JOIN CSG.BS_MATERIAL" +
            //    " ON((BS_EK_RE.MANDANT = BS_MATERIAL.MANDANT) AND(BS_EK_RE.FIRMA = BS_MATERIAL.FIRMA)) AND(BS_EK_RE_POS.MATERIAL_KEY = BS_MATERIAL.MATERIAL_KEY)) INNER JOIN CSG.BS_LFRT" +
            //    "  ON((BS_EK_RE.MANDANT = BS_LFRT.MANDANT) AND(BS_EK_RE.FIRMA = BS_LFRT.FIRMA)) AND(BS_EK_RE.LFRT_KEY = BS_LFRT.LFRT_KEY)" +
            //    "  WHERE SYS_RECH_BETRAG > 0    AND EK_RE_AN_FIBU = 1" +
            //    " AND BS_MATERIAL.MATERIAL_KEY NOT IN(9318636, 9318588, 9318584, 9318637, 9318912, 9318589) ) TESAKA" +
            //    " WHERE  EK_RE_NR in " +
            //    "( 958000430,958000427,958000431,958000428,958000432,958000429,855002649,855002646,855002648,855002645,855002644,855002647,667009745,795007346,795007347,795007351,667009747,795007345,667009746," +
            //    " 795007348,795007349,795007350,667009749,667009748,836002545,836002544,836002543,958000435,715001945,958000434,795007358,795007357,855002658,795007359,667009762,855002664,795007365,795007366," +
            //    "855002665,855002666,667009761,795007370,795007368,795007367,667009763,667009764,836002557,836002556,855002667,795007369,667009765,855002668,958000442,958000439,667009769,667009770,958000440," +
            //    "958000441,958000438,795007371,795007373,836002559,836002558,836002560,795007372,795007378,667009774,958000444,855002676,855002677,958000445,667009775,667009773,836002563,958000443,855002675,795007376)";


            cmd.CommandText = sqlText;
            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                var list = reader.Cast<IDataRecord>()
                             .Select(dr => new TesakaModels
                             {
                                 detalle = new List<TesakaModels.DetalleType>
                                 {
                                         new TesakaModels.DetalleType
                                         {
                                             cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("deta_cantidad"))), //dr.GetInt32(dr.GetOrdinal("deta_cantidad")),
                                             tasaAplica = Convert.ToString(dr.GetValue(dr.GetOrdinal("deta_tasaAplica"))),
                                             precioUnitario = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("deta_precioUnitario"))),
                                             descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("deta_descripcion")))
                                         }
                                 },
                                 retencion = new TesakaModels.RetencionType
                                 {
                                     fecha = Convert.ToString(dr.GetValue(dr.GetOrdinal("rete_fecha"))),
                                     moneda = Convert.ToString(dr.GetValue(dr.GetOrdinal("rete_moneda"))),
                                     retencionRenta = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("rete_retencionRenta"))),
                                     conceptoRenta = Convert.ToString(dr.GetValue(dr.GetOrdinal("rete_conceptoRenta"))),
                                     retencionIva = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("rete_retencionIva"))),
                                     conceptoIva = Convert.ToString(dr.GetValue(dr.GetOrdinal("rete_conceptoIva"))),
                                     rentaPorcentaje = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("rete_rentaPorcentaje"))),
                                     rentaCabezasBase = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_rentaCabezasBase"))),
                                     rentaCabezasCantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_rentaCabezasCantidad"))),
                                     rentaToneladasBase = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_rentaToneladasBase"))),
                                     rentaToneladasCantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_rentaToneladasCantidad"))),
                                     ivaPorcentaje5 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_ivaPorcentaje5"))),
                                     ivaPorcentaje10 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_ivaPorcentaje10")))
                                 },
                                 informado = new TesakaModels.InformadoType
                                 {
                                     situacion = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_situacion"))),
                                     tipoIdentificacion = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_tipoIdentificacion"))),
                                     identificacion = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_identificacion"))),
                                     nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_nombre"))),
                                     domicilio = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_domicilio"))),
                                     direccion = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_direccion"))),
                                     correoElectronico = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_correoElectronico"))),
                                     pais = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_pais"))),
                                     telefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_telefono")))
                                 },
                                 transaccion = new TesakaModels.TransaccionType
                                 {
                                     condicionCompra = Convert.ToString(dr.GetValue(dr.GetOrdinal("tran_condicionCompra"))),
                                     tipoComprobante = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("tran_tipoComprobante"))),
                                     numeroComprobanteVenta = Convert.ToString(dr.GetValue(dr.GetOrdinal("tran_numeroComprobanteVenta"))),
                                     fecha = Convert.ToString(dr.GetValue(dr.GetOrdinal("tran_fecha"))),
                                     numeroTimbrado = Convert.ToString(dr.GetValue(dr.GetOrdinal("tran_numeroTimbrado")))
                                 },
                                 atributos = new TesakaModels.AtributosType
                                 {
                                     fechaCreacion = DateTime.Now.ToString("yyyy-MM-dd")
                                 }
                             })
                             .ToList();
                JsonSerializer serializer;

                serializer = new JsonSerializer();
                //byte[] buffer = Encoding.UTF8.GetBytes(list.ToString());

                serializer.Formatting = Formatting.Indented;
                serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

                string json = JsonConvert.SerializeObject(list);
                byte[] byteArray = Encoding.UTF8.GetBytes(json);

                return byteArray;
            }

            return null;
        }
        public DataSet sp_marcarTransferidoCompras(string nroProceso)
        {
            OracleConnection cone = new OracleConnection(ConfigurationManager.ConnectionStrings["conStringOracle"].ConnectionString);
            OracleCommand coma = new OracleCommand("CSG.PKG_PROCESOS_SYSTEMA.SP_MARCAR_TRANF_COMPRAS", cone);
            coma.CommandType = CommandType.StoredProcedure;

            coma.Parameters.Add("PI_NRO_PROCESO", nroProceso); //nombre del servicio a configurar  
            coma.Parameters.Add("PO_DATOS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            DataSet dtRespuesta = new DataSet();
            OracleDataAdapter adap = new OracleDataAdapter(coma);

            try
            {
                adap.Fill(dtRespuesta);
                adap.SelectCommand.Connection.Close();
                adap.SelectCommand.Connection.Dispose();
            }
            catch (Exception e)
            {

            }

            return dtRespuesta;

        }
        public byte[] sp_retornaDatosJson(string fechaRetencion, string fechaAutofactura, string codEmpresa)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conStringOracle"].ConnectionString;
            con.Open();

            OracleCommand cmd = con.CreateCommand();

            string sqlText = "SELECT * FROM(SELECT DISTINCT'NO_CONTRIBUYENTE' info_situacion,BS_LFRT.BEZ_1 info_nombre,BS_LFRT.UST_IDENT_NR info_identificacion," +
                "'CEDULA'info_tipoIdentificacion," +
                "'' info_domicilio,'' info_direccion,'' info_pais,'' info_telefono,'retenciones@cysa.com.py' info_correoElectronico,5 tran_tipoComprobante," +
                " replace( M689_FORMATTED_RE_NR.RE_NR_GOVERNMENT, '_','-') tran_numeroComprobanteVenta,  " +
                " to_char(BS_EK_RE.EK_RE_DATUM, 'YYYY-MM-DD') tran_fecha,CSG.M689_FORMATTED_RE_NR.TIMBRADO_NUMBER tran_numeroTimbrado,'CONTADO' tran_condicionCompra,to_char(TO_DATE('" + fechaRetencion + "','DD/MM/YYYY'), 'YYYY-MM-DD') " +
                " rete_fecha,'PYG' rete_moneda," +
                "1 rete_retencionRenta,'RENTA_EMPRESARIAL.1' rete_conceptoRenta,0 rete_retencionIva,'' rete_conceptoIva,1.5 rete_rentaPorcentaje," +
                "0 rete_rentaCabezasBase,0 rete_rentaCabezasCantidad,0 rete_rentaToneladasBase,0 rete_rentaToneladasCantidad,0 rete_ivaPorcentaje5," +
                "0 rete_ivaPorcentaje10,1 deta_cantidad,SYS_RECH_BETRAG deta_precioUnitario,0 deta_tasaAplica,'Compra papel en desuso' deta_descripcion " +
                "FROM((((BS_EK_RE INNER JOIN CSG.BS_EK_RE_POS ON BS_EK_RE.EK_RE_KEY = BS_EK_RE_POS.EK_RE_KEY) INNER JOIN CSG.CS_ZAHLBED " +
                "ON(BS_EK_RE.ZAHLBED_KEY = CS_ZAHLBED.ZAHLBED_KEY) AND(BS_EK_RE.MANDANT = CS_ZAHLBED.MANDANT)) LEFT OUTER JOIN CSG.M689_FORMATTED_RE_NR " +
                "ON BS_EK_RE.EK_RE_KEY = M689_FORMATTED_RE_NR.RE_KEY) INNER JOIN CSG.BS_MATERIAL ON((BS_EK_RE.MANDANT = BS_MATERIAL.MANDANT) " +
                "AND(BS_EK_RE.FIRMA = BS_MATERIAL.FIRMA)) AND(BS_EK_RE_POS.MATERIAL_KEY = BS_MATERIAL.MATERIAL_KEY)) INNER JOIN CSG.BS_LFRT " +
                "ON((BS_EK_RE.MANDANT = BS_LFRT.MANDANT) AND(BS_EK_RE.FIRMA = BS_LFRT.FIRMA)) AND(BS_EK_RE.LFRT_KEY = BS_LFRT.LFRT_KEY) " +
                "WHERE SYS_RECH_BETRAG > 0 " +
                "AND(BS_EK_RE.EK_RE_DATUM BETWEEN TO_DATE('" + fechaAutofactura + "', 'dd.mm.yyyy') AND TO_DATE('" + fechaAutofactura + "', 'dd.mm.yyyy')) " +
                "AND BS_EK_RE.U_FIRMA = '" + codEmpresa + "' AND(BS_EK_RE.VORGANGS_ART = 'G21' OR BS_EK_RE.VORGANGS_ART = 'G22') " +
                "AND EK_RE_AN_FIBU = 1 " +
                "AND BS_MATERIAL.MATERIAL_KEY NOT IN(9318636, 9318588, 9318584, 9318637, 9318912, 9318589) ) " +
                "TESAKA WHERE NOT EXISTS( SELECT* FROM RV_TESAKA_RESULT WHERE RV_TESAKA_RESULT.FACTURA= " +
                "TESAKA.tran_numeroComprobanteVenta )";


            cmd.CommandText = sqlText;
            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                var list = reader.Cast<IDataRecord>()
                             .Select(dr => new TesakaModels
                             {
                                 detalle = new List<TesakaModels.DetalleType>
                                 {
                                         new TesakaModels.DetalleType
                                         {
                                             cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("deta_cantidad"))), //dr.GetInt32(dr.GetOrdinal("deta_cantidad")),
                                             tasaAplica = Convert.ToString(dr.GetValue(dr.GetOrdinal("deta_tasaAplica"))),
                                             precioUnitario = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("deta_precioUnitario"))),
                                             descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("deta_descripcion")))
                                         }
                                 },
                                 retencion = new TesakaModels.RetencionType
                                 {
                                     fecha = Convert.ToString(dr.GetValue(dr.GetOrdinal("rete_fecha"))),
                                     moneda = Convert.ToString(dr.GetValue(dr.GetOrdinal("rete_moneda"))),
                                     retencionRenta = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("rete_retencionRenta"))),
                                     conceptoRenta = Convert.ToString(dr.GetValue(dr.GetOrdinal("rete_conceptoRenta"))),
                                     retencionIva = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("rete_retencionIva"))),
                                     conceptoIva = Convert.ToString(dr.GetValue(dr.GetOrdinal("rete_conceptoIva"))),
                                     rentaPorcentaje = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("rete_rentaPorcentaje"))),
                                     rentaCabezasBase = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_rentaCabezasBase"))),
                                     rentaCabezasCantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_rentaCabezasCantidad"))),
                                     rentaToneladasBase = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_rentaToneladasBase"))),
                                     rentaToneladasCantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_rentaToneladasCantidad"))),
                                     ivaPorcentaje5 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_ivaPorcentaje5"))),
                                     ivaPorcentaje10 = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("rete_ivaPorcentaje10")))
                                 },
                                 informado = new TesakaModels.InformadoType
                                 {
                                     situacion = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_situacion"))),
                                     tipoIdentificacion = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_tipoIdentificacion"))),
                                     identificacion = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_identificacion"))),
                                     nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_nombre"))),
                                     domicilio = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_domicilio"))),
                                     direccion = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_direccion"))),
                                     correoElectronico = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_correoElectronico"))),
                                     pais = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_pais"))),
                                     telefono = Convert.ToString(dr.GetValue(dr.GetOrdinal("info_telefono")))
                                 },
                                 transaccion = new TesakaModels.TransaccionType
                                 {
                                     condicionCompra = Convert.ToString(dr.GetValue(dr.GetOrdinal("tran_condicionCompra"))),
                                     tipoComprobante = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("tran_tipoComprobante"))),
                                     numeroComprobanteVenta = Convert.ToString(dr.GetValue(dr.GetOrdinal("tran_numeroComprobanteVenta"))),
                                     fecha = Convert.ToString(dr.GetValue(dr.GetOrdinal("tran_fecha"))),
                                     numeroTimbrado = Convert.ToString(dr.GetValue(dr.GetOrdinal("tran_numeroTimbrado")))
                                 },
                                 atributos = new TesakaModels.AtributosType
                                 {
                                     fechaCreacion = DateTime.Now.ToString("yyyy-MM-dd")
                                 }
                             })
                             .ToList();
                JsonSerializer serializer;

                serializer = new JsonSerializer();
                //byte[] buffer = Encoding.UTF8.GetBytes(list.ToString());

                serializer.Formatting = Formatting.Indented;
                serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

                string json = JsonConvert.SerializeObject(list);
                byte[] byteArray = Encoding.UTF8.GetBytes(json);

                return byteArray;
            }

            return null;
        }
        public DataSet sp_marcarTransferidoVentas(string nroProceso)
        {
            OracleConnection cone = new OracleConnection(ConfigurationManager.ConnectionStrings["conStringOracle"].ConnectionString);
            OracleCommand coma = new OracleCommand("CSG.PKG_PROCESOS_SYSTEMA.SP_MARCAR_TRANF_VENTAS", cone);
            coma.CommandType = CommandType.StoredProcedure;

            coma.Parameters.Add("PI_NRO_PROCESO", nroProceso); //nombre del servicio a configurar  
            coma.Parameters.Add("PO_DATOS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            DataSet dtRespuesta = new DataSet();
            OracleDataAdapter adap = new OracleDataAdapter(coma);

            try
            {
                adap.Fill(dtRespuesta);
                adap.SelectCommand.Connection.Close();
                adap.SelectCommand.Connection.Dispose();
            }
            catch (Exception e)
            {

            }

            return dtRespuesta;

        }
        #endregion  
        public bool creaDirectorio(string ruta)
        {
            bool existeArchivo = Directory.Exists(ruta);
            try
            {
                if (!existeArchivo)
                {
                    System.IO.Directory.CreateDirectory(ruta);
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public DataTable importarTxtCamp(string FilePath)
        {
            DataTable dt = new DataTable("Datos");
            string[] columns = null;
            int ultColumna = 0;

            var lines = File.ReadAllLines(FilePath);

            if (lines.Count() > 0)
            {
                columns = lines[0].Split(new char[] { ';' });

                foreach (var column in columns)
                {
                    dt.Columns.Add(column);
                }
                ultColumna = columns.Count();
            }

            for (int i = 1; i < lines.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                string[] values = lines[i].Split(new char[] { ';' });

                for (int j = 0; j < values.Count() && j < columns.Count(); j++)
                {
                    dr[j] = values[j];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable sp_retornaDatosDatatableImportacion(string fechaRetencion, string procesos)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conStringOracle"].ConnectionString;
            con.Open();

            OracleCommand cmd = con.CreateCommand();

            string sqlText = "SELECT * FROM(SELECT DISTINCT BS_EK_RE.EK_RE_NR, 'NO_CONTRIBUYENTE' info_situacion,BS_LFRT.BEZ_1 info_nombre,BS_LFRT.UST_IDENT_NR info_identificacion,'CEDULA'info_tipoIdentificacion,'' info_domicilio," +
              "'' info_direccion,'' info_pais,'' info_telefono,'retenciones@cysa.com.py' info_correoElectronico,5 tran_tipoComprobante, replace( M689_FORMATTED_RE_NR.RE_NR_GOVERNMENT, '_','-') tran_numeroComprobanteVenta, to_char(BS_EK_RE.EK_RE_DATUM, 'YYYY-MM-DD') tran_fecha,'11884116' tran_numeroTimbrado," +
              "  'CONTADO' tran_condicionCompra,to_char(TO_DATE('" + fechaRetencion + "', 'DD/MM/YYYY'), 'YYYY-MM-DD')  rete_fecha,'PYG' rete_moneda,1 rete_retencionRenta,'RENTA_EMPRESARIAL.1' rete_conceptoRenta," +
              "  0 rete_retencionIva,'' rete_conceptoIva,1.5 rete_rentaPorcentaje,0 rete_rentaCabezasBase,0 rete_rentaCabezasCantidad,0 rete_rentaToneladasBase,0 rete_rentaToneladasCantidad," +
              "  0 rete_ivaPorcentaje5,0 rete_ivaPorcentaje10,1 deta_cantidad,SYS_RECH_BETRAG deta_precioUnitario,0 deta_tasaAplica,'Compra papel en desuso' deta_descripcion" +
              "  FROM((((BS_EK_RE INNER JOIN CSG.BS_EK_RE_POS ON BS_EK_RE.EK_RE_KEY = BS_EK_RE_POS.EK_RE_KEY) INNER JOIN CSG.CS_ZAHLBED ON(BS_EK_RE.ZAHLBED_KEY = CS_ZAHLBED.ZAHLBED_KEY)" +
              "  AND(BS_EK_RE.MANDANT = CS_ZAHLBED.MANDANT)) LEFT OUTER JOIN CSG.M689_FORMATTED_RE_NR ON BS_EK_RE.EK_RE_KEY = M689_FORMATTED_RE_NR.RE_KEY) INNER JOIN CSG.BS_MATERIAL" +
              " ON((BS_EK_RE.MANDANT = BS_MATERIAL.MANDANT) AND(BS_EK_RE.FIRMA = BS_MATERIAL.FIRMA)) AND(BS_EK_RE_POS.MATERIAL_KEY = BS_MATERIAL.MATERIAL_KEY)) INNER JOIN CSG.BS_LFRT" +
              "  ON((BS_EK_RE.MANDANT = BS_LFRT.MANDANT) AND(BS_EK_RE.FIRMA = BS_LFRT.FIRMA)) AND(BS_EK_RE.LFRT_KEY = BS_LFRT.LFRT_KEY)" +
              "  WHERE SYS_RECH_BETRAG > 0    AND EK_RE_AN_FIBU = 1" +
              " AND BS_MATERIAL.MATERIAL_KEY NOT IN(9318636, 9318588, 9318584, 9318637, 9318912, 9318589) ) TESAKA" +
              " WHERE  EK_RE_NR in " +
              "(" + procesos + ")";

            OracleConnection cone = new OracleConnection(ConfigurationManager.ConnectionStrings["conStringOracle"].ConnectionString);
            OracleCommand coma = new OracleCommand(sqlText, cone);
            DataTable dtRespuesta = new DataTable();
            OracleDataAdapter adap = new OracleDataAdapter(coma);

            try
            {
                adap.Fill(dtRespuesta);
                adap.SelectCommand.Connection.Close();
                adap.SelectCommand.Connection.Dispose();
            }
            catch (Exception e)
            {

            }

            return dtRespuesta;
        }
        public DataTable retornaDatosProveedor(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaDatosProveedor", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
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
        public DataTable retornaDatosDocumentos(string nroOC, string departamento, string nroFactura)
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
        public DatosProveedoresModels retornaDatosProveedorPorRuc(string ruc)
        {
            DataTable dt = new DataTable();
            var model = new DatosProveedoresModels();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringYaguareteSistem"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_retornaDatosProveedorPorRuc", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ruc", ruc);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();

                    foreach (DataRow rowp in dt.Rows)
                    {
                        model.provId = rowp["provID"].ToString();
                        model.provCodigoSap = rowp["provCodigoSAP"].ToString();
                        model.provRuc = rowp["provRUC"].ToString();
                        model.provRazonSocial = rowp["provRazonSocial"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return model;
        }

    }
}