using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDI.Structure
{
    
    public class Entity
    {
        public int Id { get; set; }
    }

    public class Header_Details_Information : Entity
    {
        public int HeaderKey { get; set; }
        public string Company { get; set; }
        public string TradingPartner { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string AlternateDocument { get; set; }
        public string StoreNumber { get; set; }
        public decimal Amount { get; set; }
        public string DateRecieved { get; set; }
        public string DateAcknowledgement { get; set; }
    }
}