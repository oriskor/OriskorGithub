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

    public class TransactionInboxDetails : Entity
    {
        public int HeaderKey { get; set; }
        public string CompanyName { get; set; }
        public string CodeShipFromID { get; set; }
        public string CodeShipToID { get; set; }
        public string TradingPartner { get; set; }
        public string Purpose { get; set; }
        public string Type { get; set; }
        public string PO { get; set; }
        public string AltDocument { get; set; }
        public decimal TotalOfLineItems { get; set; }
        public decimal TotalAmount { get; set; }
        public string Othercharges { get; set; }
        public string DateRecieved { get; set; }
        public string PODate { get; set; }
        public string StoreNumber { get; set; }
        public string VendorItemNo { get; set; }
        public string UOM { get; set; }
        public decimal Price { get; set; }
        public string Amount { get; set; }

        public string SentDate { get; set; }
        public string TransactionId { get; set; }
        public string FunctionalControlNo { get; set; }
    }
    public class TransactionInboxDetailsItem : Entity
    {
        public int HeaderKey { get; set; }
        public string UOM { get; set; }
        public string Qty { get; set; }
        public string Price { get; set; }
        public string PriceBasis { get; set; }
        public string VendorItem { get; set; }
        public string ItemPo { get; set; }
        
    }
}