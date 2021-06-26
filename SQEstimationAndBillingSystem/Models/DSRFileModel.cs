namespace SQEstimationAndBillingSystem.Models
{
    public class DSRFileModel
    {
        public int id { get; set; }
        public string DSRName { get; set; }
        public string FileName { get; set; }
        public string CreatedOn { get; set; }
        public bool IsLatestDSR { get; set; }
       
    }
}