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
    
    public partial class C852_ProdActivity
    {
        public int ProdActivityKey { get; set; }
        public Nullable<int> DetailKey { get; set; }
        public string ZA01_ActivityCode { get; set; }
        public string SDQ01_Unit { get; set; }
        public string SDQ02_IdCodeQlfr { get; set; }
        public string SDQ03_IdCode { get; set; }
        public string SDQ04_Quantity { get; set; }
        public string SDQ05_IdCode { get; set; }
        public string SDQ06_Quantity { get; set; }
        public string SDQ07_IdCode { get; set; }
        public string SDQ08_Quantity { get; set; }
        public string SDQ09_IdCode { get; set; }
        public string SDQ10_Quantity { get; set; }
        public string SDQ11_IdCode { get; set; }
        public string SDQ12_Quantity { get; set; }
        public string SDQ13_IdCode { get; set; }
        public string SDQ14_Quantity { get; set; }
        public string SDQ15_IdCode { get; set; }
        public string SDQ16_Quantity { get; set; }
        public string SDQ17_IdCode { get; set; }
        public string SDQ18_Quantity { get; set; }
        public string SDQ19_IdCode { get; set; }
        public string SDQ20_Quantity { get; set; }
        public string SDQ21_IdCode { get; set; }
        public string SDQ22_Quantity { get; set; }
    
        public virtual C852_Detail C852_Detail { get; set; }
    }
}
