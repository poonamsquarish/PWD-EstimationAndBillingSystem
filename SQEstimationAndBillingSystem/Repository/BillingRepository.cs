using SQEstimationAndBillingSystem.DAL;
using SQEstimationAndBillingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQEstimationAndBillingSystem.Repository
{
    public class BillingRepository
    {
        private SQEstimationAndBillingSystemDbEntities _dbContext;
        public BillingRepository()
        {
            _dbContext = new SQEstimationAndBillingSystemDbEntities();
        }
        public List<BillingModel> GetAllBilling()
        {
            return _dbContext.Database.SqlQuery<BillingModel>("SQSPGetAllDSRDetails").ToList();
        }

        public BillingModel GetBillingById(long id)
        {
            return _dbContext.Database.SqlQuery<BillingModel>("SQSPGetAllDSRDetails").Where(x => x.id == id).FirstOrDefault();
        }

        public string AddEditBilling(BillingModel objBillingModel)
        {
            return "Success";
        }

        
    }
}