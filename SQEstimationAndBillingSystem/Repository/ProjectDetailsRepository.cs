using SQEstimationAndBillingSystem.DAL;
using SQEstimationAndBillingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SQEstimationAndBillingSystem.Repository
{
    public class ProjectDetailsRepository
    {
        private SQEstimationAndBillingSystemDbEntities _dbContext;
        public ProjectDetailsRepository()
        {
            _dbContext = new SQEstimationAndBillingSystemDbEntities();
        }
        public List<ProjectDetailsModel> GetAllProjectDetails()
        {
            return _dbContext.Database.SqlQuery<ProjectDetailsModel>("SQSPGetAllProjectDetails").ToList();
        }

        public string AddEditProjectDetails(ProjectDetailsModel model)
        {

            return _dbContext.Database.SqlQuery<string>("SQSPAddEditProjectDetails @ID,@DSRId, @ProjectName, @ProjectAddress, @ProjectDescription",
              new SqlParameter("ID", model.id),
                    new SqlParameter("DSRId", model.DSRId),
              new SqlParameter("ProjectName", model.ProjectName),
              new SqlParameter("ProjectAddress", model.ProjectAddress),
              new SqlParameter("ProjectDescription", model.ProjectDescription)

                ).FirstOrDefault();
            //return "Success";
        }
        public string DeleteProjectDetailsById(long id)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPDeleteProjectDetailsById @ID", new SqlParameter("ID", id)).First();
        }

        public ProjectDetailsModel GetProjectDetailsById(long id)
        {
            return _dbContext.Database.SqlQuery<ProjectDetailsModel>("SQSPGetProjectDetailsById @ID", new SqlParameter("ID", id)).First();
        }

    }
}