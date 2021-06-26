using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQEstimationAndBillingSystem.Models
{
    public class RateAnalysisModel
    {
        public long id { get; set; }
        public long DSRId { get; set; }
        public long ProjectId { get; set; }
        public long? DSRDetailsId { get; set; }
        public string ItemDescription { get; set; }
        public string SSRItemNo { get; set; }
        public string Unit { get; set; }
        public decimal CompletedRateForSSR { get; set; }
        public decimal DeductForContractorsProfit { get; set; }
        public decimal ContractorsProfitPer { get; set; }
        public decimal Total { get; set; }
       
        public string Material { get; set; }
        public decimal BasicLead { get; set; }

        public decimal Consumption { get; set; }
        public decimal LeadCharge { get; set; }
        public decimal TotalLeadCharges { get; set; }
        public decimal AddForTribalAreaPer { get; set; }
        public decimal AddForTribalArea { get; set; }
        public decimal FinalCompletedRate { get; set; }

        public long CreatedBy { get; set; }
        public Nullable<DateTime> CreadtedOn { get; set; }
        public long ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
    }
}