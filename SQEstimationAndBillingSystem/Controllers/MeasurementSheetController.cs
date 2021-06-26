using SQEstimationAndBillingSystem.Helper;
using SQEstimationAndBillingSystem.FilterAttributes;
using SQEstimationAndBillingSystem.Models;
using SQEstimationAndBillingSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SQEstimationAndBillingSystem.Controllers
{
    public class MeasurementSheetController : Controller
    {
        MeasurementSheetRepository _repository = new MeasurementSheetRepository();
        // GET: MeasurementSheet
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexNew()
        {
            return View();
        }

        /// <summary>
        /// EntityMappedSite
        /// </summary>
        /// <param name="EntityId"></param>
        /// <returns></returns>
        public ActionResult MeasurementSheetItems(long id)
        {

            return PartialView("_MeasurementSheetItemDetails", id);
        }

        public ActionResult GetAllMeasurementSheetItems(DataTableAjaxPostModel param)
        {
            long totalRecords = 10;
            List<MeasurementSheetModel> lstMMsheet = new List<MeasurementSheetModel>();

            lstMMsheet.Add(new MeasurementSheetModel() { id = 1, DSRDetailId = 1, ItemOfWork = "Item 1", No = 1, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 2, DSRDetailId = 2, ItemOfWork = "Item 2", No = 2, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 3, DSRDetailId = 2, ItemOfWork = "Item 2", No = 3, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 4, DSRDetailId = 3, ItemOfWork = "Item 3", No = 4, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 5, DSRDetailId = 3, ItemOfWork = "Item 3", No = 5, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 6, DSRDetailId = 3, ItemOfWork = "Item 3", No = 6, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 7, DSRDetailId = 4, ItemOfWork = "Item 4", No = 7, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 8, DSRDetailId = 4, ItemOfWork = "Item 4", No = 8, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 9, DSRDetailId = 5, ItemOfWork = "Item 5", No = 9, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 10, DSRDetailId = 5, ItemOfWork = "Item 5", No = 11, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 11, DSRDetailId = 5, ItemOfWork = "Item 5", No = 12, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });

            var lstdocument = lstMMsheet;// _entityService.GetMappedEntityByIds(entityIdlst, param.search.value, param.start, param.length, null, null, out totalRecords);
            return Json(new
            {
                draw = param.draw,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = lstMMsheet
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetAllMeasurementSheetItem(DataTableAjaxPostModel param)
        {
            long totalRecords = 10;
            List<MeasurementSheetModel> lstMMsheet = new List<MeasurementSheetModel>();

            lstMMsheet.Add(new MeasurementSheetModel() { id = 1, ItemOfWork = "Item 1", No = 1, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 2, ItemOfWork = "Item 2", No = 2, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 3, ItemOfWork = "Item 2", No = 3, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 4, ItemOfWork = "Item 2", No = 4, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 5, ItemOfWork = "Item 2", No = 5, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 6, ItemOfWork = "Item 2", No = 6, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 7, ItemOfWork = "Item 2", No = 7, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 8, ItemOfWork = "Item 2", No = 8, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 9, ItemOfWork = "Item 2", No = 9, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 10, ItemOfWork = "Item 2", No = 11, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetModel() { id = 11, ItemOfWork = "Item 2", No = 12, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });

            var lstdocument = lstMMsheet;// _entityService.GetMappedEntityByIds(entityIdlst, param.search.value, param.start, param.length, null, null, out totalRecords);
            return Json(new
            {
                draw = param.draw,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = lstMMsheet
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetAllMeasurementSheetItemDetails(jQueryDataTableParamModel param)
        {
            long totalRecords = 5;

            var data = new JavaScriptSerializer().Deserialize<MeasurementSheetModel>(param.more_data);

            List<MeasurementSheetItemModel> lstMMsheet = new List<MeasurementSheetItemModel>();

            lstMMsheet.Add(new MeasurementSheetItemModel() { MMSid = 1, id = 1, ItemOfWork = "Item 1", No = 1, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetItemModel() { MMSid = 1, id = 2, ItemOfWork = "Item 2", No = 2, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetItemModel() { MMSid = 1, id = 3, ItemOfWork = "Item 2", No = 3, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetItemModel() { MMSid = 2, id = 4, ItemOfWork = "Item 2", No = 4, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetItemModel() { MMSid = 2, id = 5, ItemOfWork = "Item 2", No = 5, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetItemModel() { MMSid = 3, id = 6, ItemOfWork = "Item 2", No = 6, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetItemModel() { MMSid = 4, id = 7, ItemOfWork = "Item 2", No = 7, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetItemModel() { MMSid = 4, id = 8, ItemOfWork = "Item 2", No = 8, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetItemModel() { MMSid = 4, id = 9, ItemOfWork = "Item 2", No = 9, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetItemModel() { MMSid = 4, id = 10, ItemOfWork = "Item 2", No = 11, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });
            lstMMsheet.Add(new MeasurementSheetItemModel() { MMSid = 5, id = 11, ItemOfWork = "Item 2", No = 12, Length = 2.30M, Breadth = 2.33M, Height = 5M, Quantity = 5, Unit = "CM" });

            var lstdocument = lstMMsheet.ToList().Where(f => f.MMSid == data.id).ToList();// _entityService.GetMappedEntityByIds(entityIdlst, param.search.value, param.start, param.length, null, null, out totalRecords);
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = lstdocument
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateEdit(long Id = 0)
        {
            MeasurementSheetModel objMeasurementSheetModel;
            if (Id == 0)
            {
                objMeasurementSheetModel = new MeasurementSheetModel();
            }
            else
            {
                objMeasurementSheetModel = _repository.GetMeasurementSheetById(Id);
            }
            return View("CreateEdit", objMeasurementSheetModel);
        }

        [ValidateAjax]
        [HttpPost]
        public ActionResult AddEditMeasurementSheet(MeasurementSheetModel objMeasurementSheetModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
                    if (Session["LoggedInUserSession"] != null)
                    {
                        objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
                    }
                    objMeasurementSheetModel.ProjectId = objLoggedInUserSession.SelectedProjectId;
                    objMeasurementSheetModel.DSRId = Convert.ToInt16(objLoggedInUserSession.SelectedDSRId);
                    if (objMeasurementSheetModel.id == 0)
                    {
                        objMeasurementSheetModel.CreatedBy = objMeasurementSheetModel.ModifiedBy = objLoggedInUserSession.LoggedInUserId;
                    }
                    else
                    {
                        objMeasurementSheetModel.ModifiedBy = objLoggedInUserSession.LoggedInUserId;
                    }
                    var Result = _repository.AddEditMeasurementSheet(objMeasurementSheetModel);

                    return Json(new { success = true, message = "Saved successfully !!!" });
                }
                else
                {
                    return PartialView("CreateEdit", objMeasurementSheetModel);
                }
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteMeasurementSheet(long id)
        {
            try
            {
                var Result = _repository.DeleteMeasurementSheetById(id);
                return Json(new { success = Result, message = "Record Deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        #region New Index And Crude

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
            //var lists = _repository.GetAllSearchDSR(request).ToList();
            var list = lists.Select(r => new { value = Convert.ToString(r.DescriptionOfTheItem), id = r.id.ToString() });

            return Json(new { ItemOfWorkList = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllMeasurementSheetDetails()
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            //if (objLoggedInUserSession.SelectedProjectId.HasValue)
            //{
                var Result = _repository.GetAllMeasurementSheetDetails(objLoggedInUserSession.SelectedProjectId != 0 ? objLoggedInUserSession.SelectedProjectId : 0);
                return Json(Result, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new List<MeasurementSheetModel>(), JsonRequestBehavior.AllowGet);
            //}

        }

        public ActionResult GetUnitOfDSRItem(long id)
        {
            var unit = _repository.GetUnitOfDSRItem(Convert.ToInt64(id));
            return Json(new { unit = unit }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageMeasurementSheet(string id)
        {
            try
            {
                MeasurementSheetModel objMeasurementSheetModel;
                if (Convert.ToInt64(id) != 0)
                {
                    objMeasurementSheetModel = _repository.GetMeasurementSheetById(Convert.ToInt64(id));

                    if (objMeasurementSheetModel != null)
                    {
                        objMeasurementSheetModel = _repository.GetMeasurementSheetById(Convert.ToInt64(id));
                    }
                }
                else
                {
                    objMeasurementSheetModel = new MeasurementSheetModel();
                }
                return PartialView("CreateEdit", objMeasurementSheetModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}