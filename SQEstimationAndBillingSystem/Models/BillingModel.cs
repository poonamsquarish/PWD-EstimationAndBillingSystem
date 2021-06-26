using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQEstimationAndBillingSystem.Models
{
    public class BillingModel
    {
        public long id { get; set; }
        public string SrNo { get; set; }
        public string Chapter { get; set; }
        public string SSRItemNo { get; set; }
        public string ReferenceNo { get; set; }
        public string DescriptionOfTheItem { get; set; }
        public string AdditionalSpecification { get; set; }
        public string Unit { get; set; }
        public string CompletedRateExcludingGSTInRs { get; set; }
        public string LabourRateExcludingGSTInRs { get; set; }
    }
}