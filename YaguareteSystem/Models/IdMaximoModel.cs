using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaguareteSystem.Models
{ 
    public class IdMaximoModel
    { 
        public List<ValueId> value { get; set; }
    }

    public class ValueId
    {  
        public int ID { get; set; }
    }
}