using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using SQEstimationAndBillingSystem.DAL;
using SQEstimationAndBillingSystem.Models;

namespace SQEstimationAndBillingSystem.Repository
{
    public class LeadChargesRepository
    {
        private SQEstimationAndBillingSystemDbEntities _dbContext;
        public LeadChargesRepository()
        {
            _dbContext = new SQEstimationAndBillingSystemDbEntities();
        }
        public List<LeadChargesModel> GetAllLeadCharges(long ProjectId)
        {
            return _dbContext.Database.SqlQuery<LeadChargesModel>("SQSPGetAllLeadChargesDetails @ID", new SqlParameter("ID", ProjectId)).ToList();
        }

        public LeadChargesModel GetLeadChargesById(long id)
        {
            return _dbContext.Database.SqlQuery<LeadChargesModel>("SQSPGetLeadChargesById @ID", new SqlParameter("ID", id)).First();
        }

        public string DeleteLeadChargesById(long id)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPDeleteLeadChargesById @ID", new SqlParameter("ID", id)).First();
        }

        public List<LeadInKMDataModel> GetLeadInKMData(string Material, string LeadInKM, int DSRId)
        {
            return _dbContext.Database.SqlQuery<LeadInKMDataModel>("SQSPGetLeadInKMData @Material, @LeadInKM, @DSRId", new SqlParameter("Material", Material), new SqlParameter("LeadInKM", LeadInKM), new SqlParameter("DSRId", DSRId)).ToList();
        }

        public string AddEditLeadCharges(LeadChargesModel model)
        {

            return _dbContext.Database.SqlQuery<string>("SQSPAddEditLeadCharges @ID, @ProjectId, @Material, @Unit, @LeadInKm, @TotalLeadChargesAtQuarry,@NetLeadChargesForCompletedCol, @DSRId,@DSRDetailsId, @CreatedBy, @ModifiedBy",
              new SqlParameter("ID", model.id),
              new SqlParameter("ProjectId", model.ProjectId),
              new SqlParameter("Material", model.Material),
              new SqlParameter("Unit", model.Unit),
              new SqlParameter("LeadInKm", model.LeadInKm),
               new SqlParameter("TotalLeadChargesAtQuarry", model.TotalLeadChargesAtQuarry),
              new SqlParameter("NetLeadChargesForCompletedCol", model.NetLeadChargesForCompletedCol),
              new SqlParameter("DSRId", model.DSRId),
                new SqlParameter("DSRDetailsId", model.DSRDetailsId),
              new SqlParameter("CreatedBy", model.CreatedBy),
              new SqlParameter("ModifiedBy", model.ModifiedBy)
                ).FirstOrDefault();
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public List<DSRModel> GetMaterialList(string search, int DSRId)
        {
            return _dbContext.Database.SqlQuery<DSRModel>("SQSPGetAllMaterials @search, @DSRId", new SqlParameter("search", search), new SqlParameter("DSRId", DSRId)).ToList();
        }
    }
}