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
    
    public partial class C856_Shipment
    {
        public C856_Shipment()
        {
            this.C856_Order = new HashSet<C856_Order>();
        }
    
        public int ShipmentKey { get; set; }
        public Nullable<int> HeaderKey { get; set; }
        public string TD101_PackagingCode { get; set; }
        public string TD102_LadingQuantity { get; set; }
        public string REF02_BillOfLadingNo { get; set; }
        public string N104_ShipToId { get; set; }
    
        public virtual C856_Header C856_Header { get; set; }
        public virtual ICollection<C856_Order> C856_Order { get; set; }
    }
}
