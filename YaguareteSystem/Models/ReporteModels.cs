using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Models
{
    public class ReporteModels
    {
        [JsonProperty("odata.metadata")]
        public Uri OdataMetadata { get; set; }

        [JsonProperty("value")]
        public object[] Value { get; set; }
    }
}