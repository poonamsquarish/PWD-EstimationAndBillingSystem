using SQEstimationAndBillingSystem.DAL;
using SQEstimationAndBillingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SQEstimationAndBillingSystem.Repository
{
    public class RateAnalysisRepository
    {
        private SQEstimationAndBillingSystemDbEntities _dbContext;
        public RateAnalysisRepository()
        {
            _dbContext = new SQEstimationAndBillingSystemDbEntities();
        }
        public List<RateAnalysisModel> GetAllRateAnalysis(long ProjectId)
        {
            return _dbContext.Database.SqlQuery<RateAnalysisModel>("SQSPGetAllRateAnalysis @ID", new SqlParameter("ID", ProjectId)).ToList();
        }

        public RateAnalysisModel GetRateAnalysisById(long id)
        {
            return _dbContext.Database.SqlQuery<RateAnalysisModel>("SQSPGetRateAnalysisById @ID", new SqlParameter("ID", id)).FirstOrDefault();
        }

        public string AddEditRateAnalysis(RateAnalysisModel model)
        {
            //var pList = new SqlParameter("@AnswerOptionList", SqlDbType.Structured);
            //pList.TypeName = "dbo.AnswerOptionTableType";
            //pList.Value = model.lstAnswerOption == null ? ToDataTable(new List<OptionModel>()) : ToDataTable(model.lstAnswerOption);
            //model.DSRId = 2;
            //model.ProjectId = 1;

            return _dbContext.Database.SqlQuery<string>("SQSPAddEditRateAnalysis @ID," +
                "@ProjectId," +
                "@DSRId, " +
                "@DSRDetailsId, " +
                "@SSRItemNo, " +
                "@Unit," +
                "@CompletedRateForSSR," +
                "@DeductForContractorsProfit," +
                "@Total," +
                "@Material," +
                "@BasicLead," +
                "@Consumption," +
                "@LeadCharge," +
                "@TotalLeadCharges," +
                "@AddForTribalArea," +
                "@FinalCompletedRate," +
                "@ContractorsProfitPer,"+
                "@TribalAreaPer," +
                "@CreatedBy," +
                "@ModifiedBy",
              new SqlParameter("ID", model.id),
              new SqlParameter("ProjectId", model.ProjectId),
               new SqlParameter("DSRId", model.DSRId),
              new SqlParameter("DSRDetailsId", ToDBNull(model.DSRDetailsId == Convert.ToInt64(0) ? (long?)null : model.DSRDetailsId)),
              new SqlParameter("SSRItemNo", model.SSRItemNo),
              new SqlParameter("Unit", model.Unit),
               new SqlParameter("CompletedRateForSSR", model.CompletedRateForSSR),
              new SqlParameter("DeductForContractorsProfit", model.DeductForContractorsProfit),
               new SqlParameter("Total", model.Total),
                new SqlParameter("Material", ToDBNull(model.Material)),
                 new SqlParameter("BasicLead", model.BasicLead),
                  new SqlParameter("Consumption", model.Consumption),
                   new SqlParameter("LeadCharge", model.LeadCharge),
                    new SqlParameter("TotalLeadCharges", model.TotalLeadCharges),
                     new SqlParameter("AddForTribalArea", model.AddForTribalArea),
                      new SqlParameter("FinalCompletedRate", model.FinalCompletedRate),

                           new SqlParameter("ContractorsProfitPer", model.ContractorsProfitPer),
                      new SqlParameter("TribalAreaPer", model.AddForTribalAreaPer),
                       new SqlParameter("CreatedBy", model.CreatedBy),
              new SqlParameter("ModifiedBy", model.ModifiedBy)
                //new SqlParameter("AnswerOptionList", model.lstAnswerOption)
                //pList
                ).FirstOrDefault();
            //return "Success";
        }

        public string DeleteRateAnalysisById(long id)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPDeleteRateAnalysisById @ID", new SqlParameter("ID", id)).First();
        }

        public List<DSRModel> GetAllSearchDSR(string search, int DSRId)
        {
            return _dbContext.Database.SqlQuery<DSRModel>("SQSPGetAllSearchDSR @search, @DSRId", new SqlParameter("search", search), new SqlParameter("DSRId", DSRId)).ToList();
        }

        public List<RALeadChargesModel> GetItemOfLeadCharges(string search, int ProjectId)
        {
            // int ProjectId = 1;
            return _dbContext.Database.SqlQuery<RALeadChargesModel>("SQSPGetItemOfLeadCharges @search, @ProjectId", new SqlParameter("search", search), new SqlParameter("ProjectId", ProjectId)).ToList();
        }

        public static object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }

    }
}