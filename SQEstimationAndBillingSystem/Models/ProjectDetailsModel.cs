using System.ComponentModel.DataAnnotations;

namespace SQEstimationAndBillingSystem.Models
{
    public class ProjectDetailsModel
    {
        public long id { get; set; }
        public int DSRId { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string ProjectAddress { get; set; }
        [Required]
        public string ProjectDescription { get; set; }
        public bool IsMeasurment { get; set; }
        public bool IsAbstractSheet { get; set; }
        public bool IsLeadCharges { get; set; }
        public bool IsMaterialConsumption { get; set; }
        public bool IsRateAnalysis { get; set; }
        public bool IsRecaptionalSheet { get; set; }
        public bool IsBilling { get; set; }
        public bool IsDocumnetUploaded { get; set; }
    }
    
}