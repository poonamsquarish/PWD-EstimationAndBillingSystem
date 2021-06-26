using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SQEstimationAndBillingSystem.Models
{
    public class AbstractSheetModel
    {
        public long id{ get; set; }
		[Required(ErrorMessage = "DSR Required")]
		public int DSRId { get; set; }
		[Required(ErrorMessage = "Project Required")]
		public long ProjectId { get; set; }
		[DisplayName("Project Name")]
		public string ProjectName { get; set; }
		[Required(ErrorMessage = "Description Required")]
		[MaxLength(500, ErrorMessage = "Maximum length of description can be 500 characters")]
		public string Description { get; set; }
		[Required(ErrorMessage = "Quantity Required")]
		public Nullable<decimal> Quantity { get; set; }
		[Required(ErrorMessage = "Rate Required")]
		public Nullable<decimal> Rate { get; set; }
		[Required(ErrorMessage = "Unit Required")]
		public string Unit { get; set; }
		[Required(ErrorMessage = "Amount Required")]
		public Nullable<decimal> Amount { get; set; }
        [Required(ErrorMessage = "Abstract Amount Required")]
        public Nullable<decimal> AbstractAmount { get; set; }
        public int CreatedBy { get; set; }
		public Nullable<DateTime> CreadtedOn { get; set; }
		public int ModifiedBy { get; set; }
		public Nullable<DateTime> ModifiedOn { get; set; }
	}
}