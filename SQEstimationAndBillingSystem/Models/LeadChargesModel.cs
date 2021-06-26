using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SQEstimationAndBillingSystem.Models
{
    public class LeadChargesModel
    {
        public long id { get; set; }
        [Required(ErrorMessage = "DSR Required")]
        public int DSRId { get; set; }
        public long? DSRDetailsId { get; set; }
        [Required(ErrorMessage = "Project Required")]
        public long ProjectId { get; set; }
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        [Required(ErrorMessage ="Material Required")]
        [MaxLength(500,ErrorMessage = "Maximum length of material can be 500 characters")]
        public string Material { get; set; }
        [Required(ErrorMessage = "Unit Required")]
        public string Unit { get; set; }
        [Required(ErrorMessage = "Lead In Km Required")]
        [DisplayName("Lead In Km")]
        public Nullable<decimal> LeadInKm { get; set; }
        [Required(ErrorMessage = "Total Lead Charges At Quarry Required")]
        [DisplayName("Total Lead Charges At Quarry")]
        public Nullable<decimal> TotalLeadChargesAtQuarry { get; set; }
        [Required(ErrorMessage = "Net Lead Charges For Completed Col Required")]
        [DisplayName("Net Lead Charges For Completed Col")]
        public Nullable<decimal> NetLeadChargesForCompletedCol { get; set; }
     
        public long CreatedBy { get; set; }
        public Nullable<DateTime> CreadtedOn { get; set; }
        public long ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
    }
    public class LeadInKMDataModel
    {
        public string Type { get; set; }
        public decimal LeadCharges { get; set; }
    }
    }