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
    
    public partial class company_master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public company_master()
        {
            this.erp_master = new HashSet<erp_master>();
            this.user_master = new HashSet<user_master>();
            this.transaction_rejected = new HashSet<transaction_rejected>();
            this.transaction_sent = new HashSet<transaction_sent>();
            this.tradingPartnerSetups = new HashSet<tradingPartnerSetup>();
            this.transactions = new HashSet<transaction>();
        }
    
        public string c_id { get; set; }
        public string c_name { get; set; }
        public string c_type { get; set; }
        public byte[] logo { get; set; }
        public string c_address { get; set; }
        public string contact_person { get; set; }
        public string c_person_position { get; set; }
        public int phone_no { get; set; }
        public string email { get; set; }
        public int package_id { get; set; }
        public string edi_code { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<erp_master> erp_master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_master> user_master { get; set; }
        public virtual package_master package_master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transaction_rejected> transaction_rejected { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transaction_sent> transaction_sent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tradingPartnerSetup> tradingPartnerSetups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transaction> transactions { get; set; }
    }
}