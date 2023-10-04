using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Models
{
    public class TesakaModels
    { 
        public List<DetalleType> detalle { get; set; }
        //public DetalleType Detalle { get; set; }
        public RetencionType retencion { get; set; }
        public InformadoType informado { get; set; }
        public TransaccionType transaccion { get; set; }
        public AtributosType atributos { get; set; }

        public class DetalleType
        {
            public int cantidad { get; set; }
            public string tasaAplica { get; set; }
            public long precioUnitario { get; set; }
            public string descripcion { get; set; }
        }

        public class RetencionType
        {
            public string fecha { get; set; }
            public string moneda { get; set; }
            public bool retencionRenta { get; set; }
            public string conceptoRenta { get; set; }
            public bool retencionIva { get; set; }
            public string conceptoIva { get; set; }
            public double rentaPorcentaje { get; set; }
            public int rentaCabezasBase { get; set; }
            public int rentaCabezasCantidad { get; set; }
            public int rentaToneladasBase { get; set; }
            public int rentaToneladasCantidad { get; set; }
            public int ivaPorcentaje5 { get; set; }
            public int ivaPorcentaje10 { get; set; }
        }

        public class InformadoType
        {
            public string situacion { get; set; }
            public string tipoIdentificacion { get; set; }
            public string identificacion { get; set; }
            public string nombre { get; set; }
            public string domicilio { get; set; }
            public string direccion { get; set; }
            public string correoElectronico { get; set; }
            public string pais { get; set; }
            public string telefono { get; set; }
        }

        public class TransaccionType
        {
            public string condicionCompra { get; set; }
            public int tipoComprobante { get; set; }
            public string numeroComprobanteVenta { get; set; }
            public string fecha { get; set; }
            public string numeroTimbrado { get; set; }
        }

        public class AtributosType
        {
            public string fechaCreacion { get; set; }
        }
    }
}