using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsureThatAPI.Models
{
    public class PolicyInsuredID
    {
        public int PcId { get; set; }
        public int TrId { get; set; }
        public int PolicyInsurID { get; set; }
    }

    public class PolicyInsuredIDRef
    {
        public PolicyInsuredID PolicyInsurIDData { get; set; }
        public string Status { get; set; }
        public List<string> ErrorMessage { get; set; }
    }

}