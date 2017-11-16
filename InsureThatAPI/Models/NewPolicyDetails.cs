using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class PolicyDetails
    {
        public string PcId { get; set; }
        public string TrId { get; set; }
        public string PolicyNumber { get; set; }
        public string AccountManagerID { get; set; }
        public string PolicyStatus { get; set; }
        public string CoverPeriod { get; set; }
        public string CoverPeriodUnit { get; set; }
        public DateTime InceptionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string ProductID { get; set; }
        public string FloodCover { get; set; }
        public string InsuredFullName { get; set; }
        public int PolicyType { get; set; }
        public string RemoveStampDuty { get; set; }
        public string CreatedByUserID { get; set; }
        public DateTime Timestamp { get; set; }
        public string InsuredName { get; set; }

        public string Reason { get; set; }
        public string Broker { get; set; }
        public string PolicyDetailsID { get; set; }
        public string IsClaimed { get; set; }
        public int PrId { get; set; }
    }

    public class NewPolicyDetailsRef
    {
        public int PolicyDetailsID { get; set; }

        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
    public class GetNewPolicyDetailsRef
    {

        public List<PolicyDetails> PolicyData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
    public class PolicyList
    {
        public List<PolicyDetails> PolicyListDetails { get; set; }
    }
    public class ViewEditPolicyDetails
    {
        public PolicyDetails PolicyData { get; set; }
        public List<InsuredDetails> InsuredDetails { get; set; }
        public List<RiskDetails> RiskData { get; set; }
        public List<PremiumDetails> PremiumData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
     


    }
}