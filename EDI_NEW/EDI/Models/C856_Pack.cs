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
    
    public partial class C856_Pack
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C856_Pack()
        {
            this.C856_Item = new HashSet<C856_Item>();
        }
    
        public int PackKey { get; set; }
        public Nullable<int> OrderKey { get; set; }
        public string LIN03_LotNo { get; set; }
        public string MAN02_SerialShipContainerCode { get; set; }
        public string DTM02_ExpirationDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C856_Item> C856_Item { get; set; }
        public virtual C856_Order C856_Order { get; set; }
    }
}
