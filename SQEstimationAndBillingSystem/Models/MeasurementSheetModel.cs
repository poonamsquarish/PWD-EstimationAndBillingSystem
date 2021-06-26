using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SQEstimationAndBillingSystem.Models
{
    public class MeasurementSheetModel
    {
        public long id { get; set; }
        public long DSRDetailId { get; set; }
        [Required(ErrorMessage = "DSR Required")]
        public int DSRId { get; set; }
        [Required(ErrorMessage = "Project Required")]
        public long ProjectId { get; set; }
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "Item Of Work Required")]
        [MaxLength(2500, ErrorMessage = "Maximum length of item of work can be 500 characters")]
        [DisplayName("Item Of Work")]
        public string ItemOfWork { get; set; }
        //[Required(ErrorMessage = "Brief Description Required")]
        [MaxLength(1000, ErrorMessage = "Maximum length of item of work can be 500 characters")]
        [DisplayName("Brief Description")]
        public string ItemOfWorkBriefDescription { get; set; }
        [DisplayName("No.")]
        [Required(ErrorMessage = "No Required")]
        public int No { get; set; }
        [Required(ErrorMessage = "Length Required")]
        public Nullable<decimal> Length { get; set; }
        [Required(ErrorMessage = "Breadth Required")]
        public Nullable<decimal> Breadth { get; set; }
        [Required(ErrorMessage = "Height Required")]
        public Nullable<decimal> Height { get; set; }
        [Required(ErrorMessage = "Quantity Required")]
        public Nullable<decimal> Quantity { get; set; }
        //[Required(ErrorMessage = "Unit Required")]
        public string Unit { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }
    }

    public class MeasurementSheetItemModel
    {
        public long id { get; set; }

        public long MMSid { get; set; }
        public long DSRDetailId { get; set; }
        [Required(ErrorMessage = "DSR Required")]
        public int DSRId { get; set; }
        [Required(ErrorMessage = "Project Required")]
        public long ProjectId { get; set; }
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "Item Of Work Required")]
        [MaxLength(500, ErrorMessage = "Maximum length of item of work can be 500 characters")]
        public string ItemOfWork { get; set; }
        [Required(ErrorMessage = "No Required")]
        public int No { get; set; }
        [Required(ErrorMessage = "Length Required")]
        public Nullable<decimal> Length { get; set; }
        [Required(ErrorMessage = "Breadth Required")]
        public Nullable<decimal> Breadth { get; set; }
        [Required(ErrorMessage = "Height Required")]
        public Nullable<decimal> Height { get; set; }
        [Required(ErrorMessage = "Quantity Required")]
        public Nullable<decimal> Quantity { get; set; }
        [Required(ErrorMessage = "Unit Required")]
        public string Unit { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
    }

    //public class ItemOfWork
    //{
    //    public long ItemOfWorkId { get; set; }
    //    public string ItemOfWorkDescription { get; set; }
    //}
}