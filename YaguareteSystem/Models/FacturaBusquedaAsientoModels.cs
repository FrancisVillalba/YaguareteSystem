using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Models
{
    public class FacturaBusquedaAsientoModels
    {
        [JsonProperty("odata.metadata")]
        public string OdataMetadata { get; set; }
        public List<ValueFactura> value { get; set; }
    }

    public class ValueFactura
    {
        [JsonProperty("odata.type")]
        public string OdataType { get; set; }

        [JsonProperty("odata.id")]
        public string OdataId { get; set; }

        [JsonProperty("odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("odata.editLink")]
        public string OdataEditLink { get; set; }
        public string Title { get; set; }

        [JsonProperty("ID")]
        public object Id { get; set; }
    }
}