using SQEstimationAndBillingSystem.Repository;
using SQEstimationAndBillingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQEstimationAndBillingSystem.FilterAttributes;

namespace SQEstimationAndBillingSystem.Controllers
{
    public class MaterialConsumptionController : Controller
    {
        MaterialConsumptionRepository _repository = new MaterialConsumptionRepository();

        public ActionResult Index()
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            ViewBag.MaterialIdList = _repository.GetMaterialList(Convert.ToInt16(objLoggedInUserSession.SelectedDSRId)).Select(f => new { f.id, f.DescriptionOfTheItem }).Distinct().ToList().
           Select(r => new SelectListItem { Text = string.Concat(r.DescriptionOfTheItem), Value = r.id.ToString() }).ToList();
            return View();
        }

        public ActionResult GetAllMaterialConsumption()
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var Result = _repository.GetAllMaterialConsumption(objLoggedInUserSession.SelectedProjectId != 0 ? objLoggedInUserSession.SelectedProjectId : 0);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateEdit(long Id = 0)
        {
            MaterialConsumptionViewModel objMaterialConsumptionModel;
            if (Id == 0)
            {
                objMaterialConsumptionModel = new MaterialConsumptionViewModel();
            }
            else
            {
                objMaterialConsumptionModel = _repository.GetMaterialConsumptionById(Id);
            }
            return Json(objMaterialConsumptionModel, JsonRequestBehavior.AllowGet);
        }

      
        public ActionResult AddEditMaterialConsumption(MaterialConsumptionModel objMaterialConsumptionModel,List<MaterialConsumptionDetailsModel> obj)
        {
            try
            {
               // MaterialConsumptionModel objMaterialConsumptionModel = new MaterialConsumptionModel();


                    LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
                    if (Session["LoggedInUserSession"] != null)
                    {
                        objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
                    }
                    objMaterialConsumptionModel.ProjectId = objLoggedInUserSession.SelectedProjectId;
                    objMaterialConsumptionModel.DSRId = Convert.ToInt16(objLoggedInUserSession.SelectedDSRId);
                    if (objMaterialConsumptionModel.id == 0)
                    {
                       objMaterialConsumptionModel.CreatedBy = objMaterialConsumptionModel.ModifiedBy = objLoggedInUserSession.LoggedInUserId;
                    }
                    else
                    {
                        objMaterialConsumptionModel.ModifiedBy = objLoggedInUserSession.LoggedInUserId;
                    }
                var Result = _repository.AddEditMaterialConsumption(objMaterialConsumptionModel, obj);

                    return Json(new { success = true, message = "Saved successfully !!!" });
                
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// GetItemOfWorkList
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllMeasurementSheetItems()
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var lists = _repository.GetAllMeasurementSheetItems(objLoggedInUserSession.SelectedProjectId).ToList();
            var list = lists.Select(r => new { DescriptionOfTheItem= Convert.ToString(r.DescriptionOfTheItem), qty = Convert.ToString(r.TotalQty), id = r.ItemId.ToString() });
            return Json(new { MeasurementSheetItemList = list }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetItemOfWorkList
        /// </summary>
        /// <returns></returns>
        public JsonResult GetItemOfWorkList(string request)
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var lists = _repository.GetAllSearchDSR(request, Convert.ToInt16(objLoggedInUserSession.SelectedDSRId)).ToList();
            var list = lists.Select(r => new { value = Convert.ToString(r.DescriptionOfTheItem), id = r.id.ToString() });

            return Json(new { ItemOfWorkList = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageMaterialConsumption(string id)
        {
            try
            {
                //MaterialConsumptionModel objMaterialConsumption;
                //LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
                //if (Session["LoggedInUserSession"] != null)
                //{
                //    objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
                //}


                //if (Convert.ToInt64(id) != 0)
                //{
                //    objMaterialConsumption = _repository.GetMaterialConsumptionById(Convert.ToInt64(id));

                //    if (objMaterialConsumption != null)
                //    {
                //        objMaterialConsumption = _repository.GetMaterialConsumptionById(Convert.ToInt64(id));
                //    }
                //}
                //else
                //{
                //    objMaterialConsumption = new MaterialConsumptionModel();
                //}
                // return PartialView("CreateEdit", objMaterialConsumption);
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DeleteMaterialConsumption(long id)
        {
            try
            {
                var Result = _repository.DeleteMaterialConsumptionById(id);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        
    }


}