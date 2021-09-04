using SQEstimationAndBillingSystem.DAL;
using SQEstimationAndBillingSystem.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SQEstimationAndBillingSystem.Repository
{
    public class AbstractSheetRepository
    {
        private SQEstimationAndBillingSystemDbEntities _dbContext;
        public string connectionString = string.Empty;
        public string fileName = string.Empty;
        public string localFileName = string.Empty;
        public bool IsErrorOccured = false;

        public AbstractSheetRepository()
        {
            _dbContext = new SQEstimationAndBillingSystemDbEntities();
        }
        public bool IsAbstractSheetDetails(long id)
        {
            return _dbContext.Database.SqlQuery<bool>("SQSPIsAbstractSheetPresent @ID", new SqlParameter("ID", id)).First();
        }

        public List<AbstractSheetModel> GetAllAbstractSheetDetails(long id)
        {
            return _dbContext.Database.SqlQuery<AbstractSheetModel>("SQSPGetAllAbstractSheetDetails @ID", new SqlParameter("ID", id)).ToList();
        }

        public AbstractSheetModel GetAbstractSheetById(long id)
        {
            return _dbContext.Database.SqlQuery<AbstractSheetModel>("SQSPGetAbstractSheetById @ID", new SqlParameter("ID", id)).First();
        }

        public string DeleteAbstractSheetById(long id)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPDeleteAbstractSheetById @ID", new SqlParameter("ID", id)).First();
        }


        public string AddMeasurementSheet(AbstractSheetModel model)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPAddAbstractSheet @ID ,@ProjectId,@DSRId,@CreatedBy,@ModifiedBy",
              new SqlParameter("ID", model.id),
              new SqlParameter("ProjectId", model.ProjectId),
              new SqlParameter("DSRId", model.DSRId),
              new SqlParameter("CreatedBy", model.CreatedBy),
              new SqlParameter("ModifiedBy", model.ModifiedBy)
                ).FirstOrDefault();
        }


        public string AddEditAbstractSheet(AbstractSheetModel model)
        {
            //model.DSRId = 2;
            //model.ProjectId = 1;

            return _dbContext.Database.SqlQuery<string>("SQSPAddEditAbstractSheet @ID ,@ProjectId,@Description ,@Quantity,@Rate,@Unit ,@Amount ,@DSRId,@CreatedBy,@ModifiedBy",
              new SqlParameter("ID", model.id),
              new SqlParameter("ProjectId", model.ProjectId),
              new SqlParameter("Description", model.Description),
              new SqlParameter("Quantity", model.Quantity),
              new SqlParameter("Rate", model.Rate),
               new SqlParameter("Unit", model.Unit),
              new SqlParameter("Amount", model.Amount),
              new SqlParameter("DSRId", model.DSRId),
              new SqlParameter("CreatedBy", model.CreatedBy),
              new SqlParameter("ModifiedBy", model.ModifiedBy)
                ).FirstOrDefault();
        }

    }
}