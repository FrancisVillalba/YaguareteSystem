using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Models
{
    public class OcsValues
    { 
        public int Id { get; set; }
        public string Title { get; set; }
        public int ID { get; set; }
    } 
    public class OcsFiltradosPorSolicitanteModels
    { 
        public IList<OcsValues> value { get; set; }

    }
}