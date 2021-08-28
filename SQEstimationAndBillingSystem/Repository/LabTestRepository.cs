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
    public class LabTestRepository
    {
        private SQEstimationAndBillingSystemDbEntities _dbContext;
        public string connectionString = string.Empty;
        public string fileName = string.Empty;
        public string localFileName = string.Empty;
        public bool IsErrorOccured = false;

        public LabTestRepository()
        {
            _dbContext = new SQEstimationAndBillingSystemDbEntities();
        }

        public List<DSRModel> GetAllSearchDSR(string search, int DSRId)
        {
            return _dbContext.Database.SqlQuery<DSRModel>("SQSPGetAllSearchDSR @search, @DSRId", new SqlParameter("search", search), new SqlParameter("DSRId", DSRId)).ToList();
        }

       
        public List<MaterialModel> GetAllSearchMaterial(string search, int DSRId, int ProjectId)
        {
            return _dbContext.Database.SqlQuery<MaterialModel>("SQSPGetAllSearchMaterial @search, @DSRId, @ProjectId", new SqlParameter("search", search), new SqlParameter("DSRId", DSRId), new SqlParameter("ProjectId", ProjectId)).ToList();
        }

         //pdf file page 538-545
        public List<NameOfTestMasterModel> GetAllNameOfTest(string search, int DSRId)
        {
            return _dbContext.Database.SqlQuery<NameOfTestMasterModel>("SQSPGetAllSearchNameOfTest @search, @DSRId", new SqlParameter("search", search), new SqlParameter("DSRId", DSRId)).ToList();
        }

        public List<NameOfTestModel> GetMaterialNameOfTests(long id)
        {
            List<NameOfTestModel> nameOfTestModel = null;

            nameOfTestModel = _dbContext.Database.SqlQuery<NameOfTestModel>("SQSPGetMaterialNameOfTests @LabTestId", new SqlParameter("LabTestId", id)).ToList();

            return nameOfTestModel;
        }

        public List<DSRModel> GetAllDSRDetails()
        {
            return _dbContext.Database.SqlQuery<DSRModel>("SQSPGetAllDSRDetails").ToList();
        }

        public List<LabTestModel> GetAllLabTestDetails(long PId)
        {
            return _dbContext.Database.SqlQuery<LabTestModel>("SQSPGetAllLabTestDetails @ID", new SqlParameter("ID", PId)).ToList();
        }

        public LabTestBasicModel GetLabTestById(long id)
        {
            return _dbContext.Database.SqlQuery<LabTestBasicModel>("SQSPGetLabTestById @ID", new SqlParameter("ID", id)).First();
        }

        public string GetUnitOfDSRItem(long id)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPGetUnitOfDSRItem @ID", new SqlParameter("ID", id)).First();
        }

        public string DeleteLabTestById(long id)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPDeleteLabTestById @ID", new SqlParameter("ID", id)).First();
        }

        public string AddEditLabTest(LabTestBasicModel model)
        {
            var pList = new SqlParameter("@NameOfTestList", SqlDbType.Structured);
            pList.TypeName = "dbo.LabTestMappingTableType";
            pList.Value = model.NameOfTestList == null ? ToDataTable(new List<NameOfTestModel>()) : ToDataTable(model.NameOfTestList);
            model.NameOfTestList.ForEach(f=> { f.LabTestId = model.id; });

            string result = string.Empty;

           var res = _dbContext.Database.SqlQuery<long>("SQSPAddEditLabTest @Id ,@ProjectId,@ItemOfWork,@DSRDetailId ,@MaterialId,@Quantity ,@Unit ,@NameOfTestList ,@DSRId ,@CreatedBy,@ModifiedBy",
              new SqlParameter("Id", model.id),
              new SqlParameter("ProjectId", model.ProjectId),
              new SqlParameter("ItemOfWork", model.ItemOfWork),
              new SqlParameter("DSRDetailId", model.DSRDetailId.HasValue ? model.DSRDetailId.Value : ToDBNull(null)),
                new SqlParameter("MaterialId", model.MaterialId),
                new SqlParameter("Quantity", model.Quantity),
                new SqlParameter("Unit", model.Unit),
                pList,
              new SqlParameter("DSRId", model.DSRId),
              new SqlParameter("CreatedBy", model.CreatedBy),
              new SqlParameter("ModifiedBy", model.ModifiedBy)
                ).FirstOrDefault();
            result = Convert.ToString(res);
            return result;
        }
        public static object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
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