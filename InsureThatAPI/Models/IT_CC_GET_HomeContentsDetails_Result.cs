//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InsureThatAPI.Models
{
    using System;
    
    public partial class IT_CC_GET_HomeContentsDetails_Result
    {
        public int PcId { get; set; }
        public int TrId { get; set; }
        public int HomeID { get; set; }
        public decimal HomeContentsUnspecifiedSumInsured { get; set; }
        public int HomeContentsNoClaimPeriod { get; set; }
        public decimal HomeContentsExcess { get; set; }
        public int HomeContentsNoClaimDiscount { get; set; }
        public int HomeContentsAgeDiscount { get; set; }
        public int PremiumID { get; set; }
        public int HomeContentsDetailID { get; set; }
    }
}