namespace SQEstimationAndBillingSystem.Models
{
    public class DSRModelUpload
    {
       
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