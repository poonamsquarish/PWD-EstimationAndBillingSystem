using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQEstimationAndBillingSystem.Models
{
    public class RecaptionalModel
    {
        public int    ProjectId { get; set; }
        public decimal WorkPortion { get; set; }
        public decimal Total { get; set; }
        public decimal DeductForRoyalty { get; set; }
        public decimal DeductForTestingCharges { get; set; }
        public decimal TotalCostofWorkPortion { get; set; }
        public decimal GST { get; set; }
        public decimal LabourInsurancePer { get; set; }
        public decimal ContingencyChargesPer { get; set; }
        public decimal AdministrativeChargesPer { get; set; }

        public decimal TotalGrossAmount { get; set; }
        public decimal Say { get; set; }
        
    }
}