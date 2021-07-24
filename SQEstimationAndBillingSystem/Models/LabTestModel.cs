using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SQEstimationAndBillingSystem.Models
{
    public class LabTestModel
    {
        public long id { get; set; }
        public long? DSRDetailId { get; set; }
        public int DSRId { get; set; }
        public long ProjectId { get; set; }
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        [DisplayName("Item Of Work")]
        public string ItemOfWork { get; set; }
        [MaxLength(1000, ErrorMessage = "Maximum length of item of work can be 500 characters")]
        [DisplayName("Brief Description")]
        public string ItemOfWorkBriefDescription { get; set; }

        //Material
        public Nullable<long> MaterialId { get; set; }
        public string Material { get; set; }
        [Required(ErrorMessage = "Quantity Required")]
        public Nullable<decimal> Quantity { get; set; }
        public string Unit { get; set; }


        //NameOfTest
        public string NameOfTest { get; set; }
        public string Frequency { get; set; }
        public Nullable<int> NoOfTestReqd { get; set; }
        public Nullable<int> TestToBeTakenFromLab { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Amount { get; set; }


        public Nullable<DateTime> CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }

    }
    public class LabTestBasicModel
    {
        public long id { get; set; }
        //[Required(ErrorMessage = "Item Of Work Required")]
        public long? DSRDetailId { get; set; }
        public int DSRId { get; set; }
        public long ProjectId { get; set; }
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        [DisplayName("Item Of Work")]
        public string ItemOfWork { get; set; }
        [MaxLength(1000, ErrorMessage = "Maximum length of item of work can be 500 characters")]
        [DisplayName("Brief Description")]
        public string ItemOfWorkBriefDescription { get; set; }

        //Material
        [Required(ErrorMessage = "Material Required")]
        public Nullable<long> MaterialId { get; set; }
        [Required(ErrorMessage = "Material Required")]
        public string Material { get; set; }
        [Required(ErrorMessage = "Quantity Required")]
        public Nullable<decimal> Quantity { get; set; }
        public string Unit { get; set; }

        //Name Of Test Section json and list
        public string NameOfTestJson { get; set; }
        public List<NameOfTestModel> NameOfTestList { get; set; }

        //Name Of Test Section controls
        [DisplayName("Name Of Test")]
        public string NameOfTest { get; set; }
        public long NameOfTestId { get; set; }
        public string Frequency { get; set; }
        [DisplayName("No. Of Test Required")]
        public Nullable<int> NoOfTestReqd { get; set; }
        [DisplayName("30% Test To Be Taken From Lab")]
        public Nullable<int> TestToBeTakenFromLab { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Amount { get; set; }

        public Nullable<DateTime> CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }

        //[Required(ErrorMessageResourceName = "AtleastOneNameOfTestReqd")]
        [Required(ErrorMessage = "Add atleast one Name Of Test")]
        [Display(Name = "Name Of Test")]
        public string NameOfTestJsonValidation { get; set; }

    }

    public class NameOfTestModel
    {
        public long MapId { get; set; }
        public long LabTestId { get; set; }
        public int NameOfTestId { get; set; }
        public string NameOfTest { get; set; }
        public string Frequency { get; set; }
        public Nullable<int> NoOfTestReqd { get; set; }
        public Nullable<int> TestToBeTakenFromLab { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }

    public class MaterialModel
    {
        public long Id { get; set; }
        public string Material { get; set; }
        public string Unit { get; set; }
    }

    public class NameOfTestMasterModel
    {
        public int Id { get; set; }
        public long NameOfTestId { get; set; }
        public string NameOfTest { get; set; }
        public Nullable<decimal> RateInRs { get; set; }
        public string Category { get; set; }
    }
}