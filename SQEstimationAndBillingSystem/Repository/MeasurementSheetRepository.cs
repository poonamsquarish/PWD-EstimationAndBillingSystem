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
    public class MeasurementSheetRepository
    {
        private SQEstimationAndBillingSystemDbEntities _dbContext;
        public string connectionString = string.Empty;
        public string fileName = string.Empty;
        public string localFileName = string.Empty;
        public bool IsErrorOccured = false;

        public MeasurementSheetRepository()
        {
            _dbContext = new SQEstimationAndBillingSystemDbEntities();
        }

        public List<DSRModel> GetAllSearchDSR(string search,int DSRId)
        {
            return _dbContext.Database.SqlQuery<DSRModel>("SQSPGetAllSearchDSR @search, @DSRId", new SqlParameter("search", search), new SqlParameter("DSRId", DSRId)).ToList();
        }

        public List<DSRModel> GetAllDSRDetails()
        {
            return _dbContext.Database.SqlQuery<DSRModel>("SQSPGetAllDSRDetails").ToList();
        }

        public List<MeasurementSheetModel> GetAllMeasurementSheetDetails(long PId)
        {
            return _dbContext.Database.SqlQuery<MeasurementSheetModel>("SQSPGetAllMeasurementSheetDetails @ID", new SqlParameter("ID", PId)).ToList();
        }

        public MeasurementSheetModel GetMeasurementSheetById(long id)
        {
            return _dbContext.Database.SqlQuery<MeasurementSheetModel>("SQSPGetMeasurementSheetById @ID", new SqlParameter("ID", id)).First();
        }

        public string GetUnitOfDSRItem(long id)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPGetUnitOfDSRItem @ID", new SqlParameter("ID", id)).First();
        }

        public string DeleteMeasurementSheetById(long id)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPDeleteMeasurementSheetById @ID", new SqlParameter("ID", id)).First();
        }

        public string AddEditMeasurementSheet(MeasurementSheetModel model)
        {
            //var pList = new SqlParameter("@AnswerOptionList", SqlDbType.Structured);
            //pList.TypeName = "dbo.AnswerOptionTableType";
            //pList.Value = model.lstAnswerOption == null ? ToDataTable(new List<OptionModel>()) : ToDataTable(model.lstAnswerOption);
            //model.DSRId = 2;
            //model.ProjectId = 1;

            return _dbContext.Database.SqlQuery<string>("SQSPAddEditMeasurementSheet @Id ,@ProjectId ,@ItemOfWork,@ItemOfWorkBriefDescription,@DSRDetailId ,@No,@Length ,@Breadth ,@Height ,@Quantity ,@Unit ,@DSRId ,@CreatedBy,@ModifiedBy",
              new SqlParameter("ID", model.id),
              new SqlParameter("ProjectId", model.ProjectId),
              new SqlParameter("ItemOfWork", model.ItemOfWork),
              new SqlParameter("ItemOfWorkBriefDescription", model.ItemOfWorkBriefDescription),
              new SqlParameter("No", model.No),
              new SqlParameter("Length", model.Length),
              new SqlParameter("Breadth", model.Breadth),
              new SqlParameter("Height", model.Height),
              new SqlParameter("Quantity", model.Quantity),
              new SqlParameter("Unit", model.Unit),
              new SqlParameter("DSRDetailId", model.DSRDetailId),
              new SqlParameter("DSRId", model.DSRId),
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
    }
}