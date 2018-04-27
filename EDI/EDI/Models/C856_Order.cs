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
    
    public partial class C856_Order
    {
        public C856_Order()
        {
            this.C856_Pack = new HashSet<C856_Pack>();
            this.C856_Tare = new HashSet<C856_Tare>();
        }
    
        public int OrderKey { get; set; }
        public Nullable<int> ShipmentKey { get; set; }
        public string PRF01_RetailPurchaseOrderNo { get; set; }
        public string PRF02_ReleaseNumber { get; set; }
    
        public virtual C856_Shipment C856_Shipment { get; set; }
        public virtual ICollection<C856_Pack> C856_Pack { get; set; }
        public virtual ICollection<C856_Tare> C856_Tare { get; set; }
    }
}
