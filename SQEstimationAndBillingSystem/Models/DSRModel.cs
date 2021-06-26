namespace SQEstimationAndBillingSystem.Models
{
    public class DSRModel
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
        public bool? IsNonDSRItem { get; set; }
        public int DSRId { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }

    }
}