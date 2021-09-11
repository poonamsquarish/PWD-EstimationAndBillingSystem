using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQEstimationAndBillingSystem.Models
{
    public class RecaptionalModel
    {
        public long    ProjectId { get; set; }
        public decimal WorkPortion { get; set; }
        public decimal Total { get; set; }
        public decimal DeductForRoyalty { get; set; }
        public decimal DeductForTestingCharges { get; set; }
        public decimal TotalCostofWorkPortion { get; set; }
        public decimal GST { get; set; }
        public decimal LabourInsurancePer { get; set; }
        public decimal ContingencyChargesPer { get; set; }
        public decimal AdministrativeChargesPer { get; set; }

        public decimal GSTAmt { get; set; }
        public decimal LabourInsuranceAmt { get; set; }
        public decimal ContigencyAmt { get; set; }
        public decimal AdministrativeAmt { get; set; }

        public decimal TotalGrossAmount { get; set; }
        public decimal Say { get; set; }

        public int DSRId { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
        public long id { get; set; }

    }
}