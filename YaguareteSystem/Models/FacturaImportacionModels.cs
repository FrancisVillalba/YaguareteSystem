using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Models
{
    public class FacturaImportacionModels
    {
        public string Ruc { get; set; }
        public string Cv { get; set; }
        public string NombreProveedor { get; set; }
        public string Timbrado { get; set; }
        public string NroFactura { get; set; }
        public string FechaFactura { get; set; } 
    }
}