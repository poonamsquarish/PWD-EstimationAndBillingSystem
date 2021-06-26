using SQEstimationAndBillingSystem.Helper;
using SQEstimationAndBillingSystem.Models;
using SQEstimationAndBillingSystem.Repository;
using System;
using System.Web.Mvc;

namespace SQEstimationAndBillingSystem.Controllers
{
    public class LeadChargesController : Controller
    {
        LeadChargesRepository _repository = new LeadChargesRepository();
        // GET: LeadCharges
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllLeadCharges(jQueryDataTableParamModel param)
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var Result = _repository.GetAllLeadCharges(objLoggedInUserSession.SelectedProjectId != 0 ? objLoggedInUserSession.SelectedProjectId : 0);

            if (!(Result is null))
            {
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = Result.Count,
                    iTotalDisplayRecords = Result.Count,
                    aaData = Result
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = Result
                }, JsonRequestBehavior.AllowGet);
            }



        }
        public ActionResult CreateEdit(long Id = 0)
        {
            LeadChargesModel objLeadChargesModel;
            if (Id == 0)
            {
                objLeadChargesModel = new LeadChargesModel();
            }
            else
            {
                objLeadChargesModel = _repository.GetLeadChargesById(Id);
            }
            return Json(objLeadChargesModel, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AddEditLeadCharges(LeadChargesModel objLeadChargesModel)
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
                    if (objLeadChargesModel.id == 0)
                    {
                        objLeadChargesModel.CreatedBy = objLeadChargesModel.ModifiedBy = objLoggedInUserSession.LoggedInUserId;
                    }
                    else
                    {
                        objLeadChargesModel.ModifiedBy = Convert.ToInt32(objLoggedInUserSession.LoggedInUserId);
                    }
                    objLeadChargesModel.ProjectId = objLoggedInUserSession.SelectedProjectId;
                    objLeadChargesModel.DSRId = Convert.ToInt32(objLoggedInUserSession.SelectedDSRId);
                    var Result = _repository.AddEditLeadCharges(objLeadChargesModel);

                    return View("Index");
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult DeleteLeadCharges(LeadChargesModel objLeadChargesModel)
        //{
        //    objLeadChargesModel.ModifiedBy = Convert.ToInt32(Session["UserId"]);
        //    var Result = _repository.AddEditLeadCharges(objLeadChargesModel);

        //    return Json(Result, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult DeleteLeadCharges(long id)
        {
            try
            {
                var Result = _repository.DeleteLeadChargesById(id);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetLeadInKMData(string Material, string LeadInKM)
        {
            try
            {
                LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
                objLoggedInUserSession.SelectedDSRId = 6;
                var Result = _repository.GetLeadInKMData(Material, LeadInKM, Convert.ToInt16(objLoggedInUserSession.SelectedDSRId));
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMaterialList(string request)
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var lists = _repository.GetMaterialList(request, Convert.ToInt16(objLoggedInUserSession.SelectedDSRId));

            return Json(new { ItemOfWorkList = lists }, JsonRequestBehavior.AllowGet);
        }
    }
}