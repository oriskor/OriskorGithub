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
    
    public partial class edi_format_master
    {
        public edi_format_master()
        {
            this.company_master = new HashSet<company_master>();
        }
    
        public int edi_code { get; set; }
        public string edi_type { get; set; }
        public string c_name { get; set; }
        public string edi_foramt { get; set; }
    
        public virtual ICollection<company_master> company_master { get; set; }
    }
}