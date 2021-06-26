using SQEstimationAndBillingSystem.DAL;
using SQEstimationAndBillingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SQEstimationAndBillingSystem.Repository
{
    public class MaterialConsumptionRepository
    {
        private SQEstimationAndBillingSystemDbEntities _dbContext;
        public MaterialConsumptionRepository()
        {
            _dbContext = new SQEstimationAndBillingSystemDbEntities();
        }

        public List<DSRModel> GetAllSearchDSR(string search,int DSRId)
        {
            return _dbContext.Database.SqlQuery<DSRModel>("SQSPGetAllSearchDSR @search, @DSRId", new SqlParameter("search", search), new SqlParameter("DSRId", DSRId)).ToList();
        }

        public string GetAllMaterialConsumption(long id)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPGetAllMaterialConsumptionDetails @ID", new SqlParameter("ID", id)).FirstOrDefault();
        }
        public List<DSRModel> GetMaterialList(int DSRId)
        {
            return _dbContext.Database.SqlQuery<DSRModel>("SQSPGetMaterialList  @DSRId", new SqlParameter("DSRId", DSRId)).ToList();
        }

        public MaterialConsumptionViewModel GetMaterialConsumptionById(long id)
        {
            var mainResult = _dbContext.Database.SqlQuery<MaterialConsumptionViewModel>("SQSPGetMaterialConsumptionById @ID", new SqlParameter("ID", id)).First();
           var ChildResult = _dbContext.Database.SqlQuery<MaterialConsumptionDetailsModel>("SQSPGetMaterialConsumptionDetailsById @ID", new SqlParameter("ID", id)).ToList();

            MaterialConsumptionViewModel Obj = new MaterialConsumptionViewModel();
            if(mainResult != null)
            {
                Obj.id = mainResult.id;
                Obj.DSRDetailId = mainResult.DSRDetailId;
                Obj.DSRId = mainResult.DSRId;
                Obj.ProjectId = mainResult.ProjectId;
                Obj.ProjectName = mainResult.ProjectName;
                Obj.ItemOfWork = mainResult.ItemOfWork;
                Obj.Quantity = mainResult.Quantity;
                Obj.MaterialList = ChildResult;
               

            }
            return Obj;


        }
        public List<MeasurementSheetItemAndQuantity> GetAllMeasurementSheetItems(long id)
        {
            return _dbContext.Database.SqlQuery<MeasurementSheetItemAndQuantity>("SQSPGetMeasurementSheetItems @ID", new SqlParameter("ID", id)).ToList();
        }

        public string DeleteMaterialConsumptionById(long id)
        {
            return _dbContext.Database.SqlQuery<string>("SQSPDeleteMaterialConsumptionById @ID", new SqlParameter("ID", id)).First();
        }
        public string AddEditMaterialConsumption(MaterialConsumptionModel model, List<MaterialConsumptionDetailsModel> Obj)
        {
            //model.DSRId = 2;
            //model.ProjectId = 1;
            //Cement ,Sand ,Steel ,CrMetal ,Bricks,Rubble,Murum ,Asphalt,
            var result = _dbContext.Database.SqlQuery<long>("SQSPAddEditMaterialConsumption @Id ,@ProjectId ,@ItemOfWork,@DSRDetailId, @Quantity, @DSRId, @CreatedBy, @ModifiedBy ",
              new SqlParameter("Id", model.id),
              new SqlParameter("ProjectId", model.ProjectId),
              new SqlParameter("ItemOfWork", model.ItemOfWork),
               //new SqlParameter("ItemOfWorkBriefDescription", model.ItemOfWorkBriefDescription),
               //new SqlParameter("Cement", ToDBNull(model.Cement)),
               //new SqlParameter("Sand", ToDBNull(model.Sand)),
               //new SqlParameter("Steel", ToDBNull(model.Steel)),
               //new SqlParameter("CrMetal", ToDBNull(model.CrMetal)),
               //new SqlParameter("Bricks", ToDBNull(model.Bricks)),
               //new SqlParameter("Rubble", ToDBNull(model.Rubble)),
               //new SqlParameter("Murum", ToDBNull(model.Murum)),
               //new SqlParameter("Asphalt", ToDBNull(model.Asphalt)),
               //new SqlParameter("CementFactor", ToDBNull(model.CementFactor)),
               //new SqlParameter("SandFactor", ToDBNull(model.SandFactor)),
               //new SqlParameter("SteelFactor", ToDBNull(model.SteelFactor)),
               //new SqlParameter("CrMetalFactor", ToDBNull(model.CrMetalFactor)),
               //new SqlParameter("BricksFactor", ToDBNull(model.BricksFactor)),
               //new SqlParameter("RubbleFactor", ToDBNull(model.RubbleFactor)),
               //new SqlParameter("MurumFactor", ToDBNull(model.MurumFactor)),
               //new SqlParameter("AsphaltFactor", ToDBNull(model.AsphaltFactor)),
               new SqlParameter("DSRDetailId", ToDBNull(model.DSRDetailId)),
              new SqlParameter("Quantity", ToDBNull(model.Quantity)),
              new SqlParameter("DSRId", ToDBNull(model.DSRId)),
              new SqlParameter("CreatedBy", ToDBNull(model.CreatedBy)),
              new SqlParameter("ModifiedBy", ToDBNull(model.ModifiedBy))
                ).FirstOrDefault();

            //if (result == "success")
            //{
                foreach (var item in Obj)
                {
                    _dbContext.Database.SqlQuery<string>("SQSPAddEditMaterialConsumptionDetails @Id ,@MaterialConsumptionID ,@DSRId,@MaterialId, @MaterialValue, @MaterialFactor",
                   new SqlParameter("Id", model.id),
                   new SqlParameter("MaterialConsumptionID", result),
                   new SqlParameter("DSRId", model.DSRId),
                   new SqlParameter("MaterialId", ToDBNull(item.MaterialId)),
                   new SqlParameter("MaterialValue", ToDBNull(item.MaterialValue)),
                   new SqlParameter("MaterialFactor", ToDBNull(item.MaterialFactor))
                  ).FirstOrDefault();

                }
           // }
            return "Success";
        }

        public static object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }
    }
}