//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EDI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class C860_Header
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C860_Header()
        {
            this.C860_Detail = new HashSet<C860_Detail>();
        }
    
        public int HeaderKey { get; set; }
        public Nullable<int> FunctionalGroupKey { get; set; }
        public string ST01_TranSetIdfrCode { get; set; }
        public string ST02_TranSetControlNo { get; set; }
        public string BCH01_TransactionSetPurposeCode { get; set; }
        public string BCH02_PurchaseOrderTypeCode { get; set; }
        public string BCH03_OriginalPO_No { get; set; }
        public string BCH06_OriginalPO_Date { get; set; }
        public string BCH11_OriginalPO_DateChange { get; set; }
        public string DTM02_DeliveryRequestedDate { get; set; }
        public string DTM02_ShipNotBeforeDate { get; set; }
        public string DTM02_ShipNoLaterDate { get; set; }
        public string DTM02_RequestedPickupDate { get; set; }
        public string N104_ShipFromID { get; set; }
        public string N401_ShipFromCity { get; set; }
        public string N402_ShipFromState { get; set; }
        public string N403_ShipFromZip { get; set; }
        public string N404_ShipFromCountryCode { get; set; }
        public string CTT01_NumberOfPOCSegments { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C860_Detail> C860_Detail { get; set; }
    }
}
