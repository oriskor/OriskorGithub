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
    
    public partial class erp_master
    {
        public string c_name { get; set; }
        public string c_id { get; set; }
        public string erp_name { get; set; }
        public string technology { get; set; }
        public string api_details { get; set; }
        public string description { get; set; }
    
        public virtual company_master company_master { get; set; }
    }
}
