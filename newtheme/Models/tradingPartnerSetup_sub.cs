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
    
    public partial class tradingPartnerSetup_sub
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TP_id { get; set; }
        public string Value { get; set; }
    
        public virtual tradingPartnerSetup tradingPartnerSetup { get; set; }
    }
}
