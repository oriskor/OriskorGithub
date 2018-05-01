using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDI.Structure
{
    public class TradingPartners
    {
    }
    
    public class Templates : Entity
    {
        public string Dcument_Type { get; set; }
        public string Version { get; set; }
        public DateTime Date_Changed { get; set; }
        public string t_id { get; set; }
    }

}