﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Models
{
    public class NombreTablaReportesModels
    {
        [JsonProperty("nombres")]
        public string[] Nombres { get; set; }
    }
}