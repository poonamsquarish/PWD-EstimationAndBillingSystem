using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace SQEstimationAndBillingSystem.Models
{
    public class MaterialConsumptionModel
    {
        public long id { get; set; }
        public long DSRDetailId { get; set; }
        public int DSRId { get; set; }
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ItemOfWork { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }
        //List<MaterialConsumptionDetailsModel> MaterialList { get; set; }
    }

    public class MaterialConsumptionDetailsModel
    {
        public long MaterialConsumptionID { get; set; }
        public int DSRId { get; set; }
        public long MaterialId { get; set; }
        public decimal MaterialValue { get; set; }
        public decimal MaterialFactor { get; set; }
        public decimal Quantity { get; set; }
    }

    public class MaterialConsumptionViewModel
    {
        public long id { get; set; }
        public long DSRDetailId { get; set; }
        public int DSRId { get; set; }
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ItemOfWork { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }
        public List<MaterialConsumptionDetailsModel> MaterialList { get; set; }
    }

    public class MeasurementSheetItemAndQuantity
    {
        public long ItemId { get; set; }
        public Nullable<decimal> TotalQty { get; set; }
    }
}