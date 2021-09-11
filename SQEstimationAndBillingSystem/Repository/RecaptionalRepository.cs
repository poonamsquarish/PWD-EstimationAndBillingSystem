using SQEstimationAndBillingSystem.DAL;
using SQEstimationAndBillingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SQEstimationAndBillingSystem.Repository
{
    public class RecaptionalRepository
    {
        private SQEstimationAndBillingSystemDbEntities _dbContext;
        public RecaptionalRepository()
        {
            _dbContext = new SQEstimationAndBillingSystemDbEntities();
        }
        public RecaptionalModel GetAllRecaptional(long projectId)
        {
            return _dbContext.Database.SqlQuery<RecaptionalModel>("SQSPGetAllRecaptional @ProjectID ", new SqlParameter("ProjectID", projectId)).FirstOrDefault();
        }

        public string AddEditRecaptional(RecaptionalModel model)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPAddEditRecaptional" +
                                                        "  @ProjectID" +
                                                        ", @WorkPortion" +
                                                        ", @TotalWorkPortion" +
                                                        ", @DeductForRoyalty" +
                                                        ", @DeductForTestingCharges" +
                                                        ", @TotalCostofWorkPortion" +
                                                        ", @GSTPer" +
                                                        ", @GSTAmt" +
                                                        ", @LabourInsurancePer" +
                                                        ", @LabourInsuranceAmt" +
                                                        ", @ContigencyPer" +
                                                        ", @ContigencyAmt" +
                                                        ", @AdministrativePer" +
                                                        ", @AdministrativeAmt" +
                                                        ", @TotalGrossAmount" +
                                                        ", @TotalGrossRoundAmt" +
                                                        ", @DSRId" +
                                                        ", @CreatedBy" +
                                                        ", @ModifiedBy",
              new SqlParameter("ProjectID", model.ProjectId),
               new SqlParameter("WorkPortion", model.WorkPortion),
                new SqlParameter("TotalWorkPortion", model.Total),
               new SqlParameter("DeductForRoyalty", model.DeductForRoyalty),
                new SqlParameter("DeductForTestingCharges", model.DeductForTestingCharges),
               new SqlParameter("TotalCostofWorkPortion", model.TotalCostofWorkPortion),
                 new SqlParameter("GSTPer", model.GST),
               new SqlParameter("GSTAmt", model.GSTAmt),
                new SqlParameter("LabourInsurancePer", model.LabourInsurancePer),
               new SqlParameter("LabourInsuranceAmt", model.LabourInsuranceAmt),
                new SqlParameter("ContigencyPer", model.ContingencyChargesPer),
               new SqlParameter("ContigencyAmt", model.ContigencyAmt),
               new SqlParameter("AdministrativePer", model.AdministrativeChargesPer),
                new SqlParameter("AdministrativeAmt", model.AdministrativeAmt),
               new SqlParameter("TotalGrossAmount", model.TotalGrossAmount),
               new SqlParameter("TotalGrossRoundAmt", model.Say),
              new SqlParameter("DSRId", model.DSRId),
              new SqlParameter("CreatedBy", model.CreatedBy),
              new SqlParameter("ModifiedBy", model.ModifiedBy)
                ).FirstOrDefault();
        }

    }
}