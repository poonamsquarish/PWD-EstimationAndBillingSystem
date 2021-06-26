using System;
using System.Web.Mvc;
using SQEstimationAndBillingSystem.Models;
using SQEstimationAndBillingSystem.Repository;

namespace SQEstimationAndBillingSystem.Controllers
{
    public class AbstractSheetController : Controller
    {
        AbstractSheetRepository _repository = new AbstractSheetRepository();
        // GET: AbstractSheet
        public ActionResult Index()
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var Result = _repository.IsAbstractSheetDetails(objLoggedInUserSession.SelectedProjectId != 0 ? objLoggedInUserSession.SelectedProjectId : 0);

            ViewBag.IsAbstractSheetSaveEnable = Result;
            return View();
        }

        public ActionResult GetAllAbstractSheetDetails()
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var Result = _repository.GetAllAbstractSheetDetails(objLoggedInUserSession.SelectedProjectId != 0 ? objLoggedInUserSession.SelectedProjectId : 0);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateEdit(long Id = 0)
        {
            AbstractSheetModel objAbstractSheetModel;
            if (Id == 0)
            {
                objAbstractSheetModel = new AbstractSheetModel();
            }
            else
            {
                objAbstractSheetModel = _repository.GetAbstractSheetById(Id);
            }
            return View("CreateEdit", objAbstractSheetModel);
        }

        [HttpPost]
        public ActionResult AddEditAbstractSheet(AbstractSheetModel objAbstractSheetModel)
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
                    objAbstractSheetModel.ProjectId = objLoggedInUserSession.SelectedProjectId;
                    objAbstractSheetModel.ProjectId = objLoggedInUserSession.SelectedDSRId;
                    if (objAbstractSheetModel.id == 0)
                    {
                        objAbstractSheetModel.CreatedBy = objAbstractSheetModel.ModifiedBy = Convert.ToInt32(objLoggedInUserSession.LoggedInUserId);
                    }
                    else
                    {
                        objAbstractSheetModel.ModifiedBy = Convert.ToInt32(objLoggedInUserSession.LoggedInUserId);
                    }
                    var Result = _repository.AddEditAbstractSheet(objAbstractSheetModel);

                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View("CreateEdit", objAbstractSheetModel);
                }
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        
       
        public ActionResult ManageAbstractSheet(long id)
        {
            try
            {
                AbstractSheetModel model = new AbstractSheetModel();
                model.id = 0;
                if (ModelState.IsValid)
                {
                    if (model.id == 0)
                    {
                        model.CreatedBy = model.ModifiedBy = Convert.ToInt32(Session["UserId"]);
                    }
                    var Result = _repository.AddMeasurementSheet(model);

                    return Json(new { success = true, message = "Saved successfully !!!" },JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Error occurred" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteAbstractSheet(long id)
        {
            try
            {
                var Result = _repository.DeleteAbstractSheetById(id);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}