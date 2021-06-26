using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQEstimationAndBillingSystem.Models
{
    public class RALeadChargesModel
    {
        public decimal NetLeadChargesForCompletedCol { get; set; }
        public string DescriptionOfTheItem { get; set; }
        public long DSRDetailsId { get; set; }
    }
}