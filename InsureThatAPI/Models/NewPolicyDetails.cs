using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class NewPolicyDetails
    {
        public int PcId { get; set; }
        public int TrId { get; set; }
        public string PolicyNumber { get; set; }
        public int AccountManagerID { get; set; }
        public int PolicyStatus { get; set; }
        public int CoverPeriod { get; set; }
        public string CoverPeriodUnit { get; set; }
        public DateTime InceptionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int ProductID { get; set; }
        public int FloodCover { get; set; }
   
        public string RemoveStampDuty { get; set; }
        public string CreatedByUserID { get; set; }
        public DateTime Timestamp { get; set; }
       
        public string Reason { get; set; }
        public string Broker { get; set; }
        public int PolicyDetailsID { get; set; }
        public string IsClaimed { get; set; }
    }

    public class NewPolicyDetailsRef
    {
        public int PolicyDetailsID { get; set; }
      
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
    public class GetNewPolicyDetailsRef
    {
     
        public NewPolicyDetails PolicyDetailsData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
    }

}