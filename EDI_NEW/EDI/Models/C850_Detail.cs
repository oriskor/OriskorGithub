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
    
    public partial class C850_Detail
    {
        public int DetailKey { get; set; }
        public Nullable<int> HeaderKey { get; set; }
        public string PO102_Quantity { get; set; }
        public string PO103_UnitMeasurementCode { get; set; }
        public string PO104_UnitPrice { get; set; }
        public string PO107_ArticleNo { get; set; }
        public string PO108_GTIN_Qlfr { get; set; }
        public string PO109_GTIN_DigitDatatructure { get; set; }
        public string PO111_VendorItemNo { get; set; }
        public string PO113_Case { get; set; }
        public string SDQ01_UnitMeasurementCode { get; set; }
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
    
        public virtual C850_Header C850_Header { get; set; }
    }
}
