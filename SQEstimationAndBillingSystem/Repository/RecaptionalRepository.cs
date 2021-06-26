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

        //public RecaptionalModel GetRecaptionalById(long id)
        //{
        //    return _dbContext.Database.SqlQuery<RecaptionalModel>("SQSPGetAllDSRDetails").Where(x=>x.id == id).FirstOrDefault();
        //}

        public string AddEditRecaptional(RecaptionalModel objRecaptionalModel)
        {
            return "Success";
        }
    }
}