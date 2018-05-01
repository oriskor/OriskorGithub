using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDI.Class_Structure
{
    public class TradingPartnerIdentifire
    {
        public string ID { get; set; }
        public string TPlugingName { get; set; }
        public string TPlugingVersion { get; set; }
        public string TPTECIdentifierName { get; set; }
        public string TPTECIdentifierType { get; set; }
        public string TPPECIdentifierName { get; set; }
        public string TPPECIdentifier_type { get; set; }
        public int TPNumbering { get; set; }
    }
}