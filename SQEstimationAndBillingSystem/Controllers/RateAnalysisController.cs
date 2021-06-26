using SQEstimationAndBillingSystem.Helper;
using SQEstimationAndBillingSystem.Models;
using SQEstimationAndBillingSystem.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SQEstimationAndBillingSystem.Controllers
{
    [Authorize]
    public class RateAnalysisController : Controller
    {
        RateAnalysisRepository _repository = new RateAnalysisRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllRateAnalysis(jQueryDataTableParamModel param)
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var Result = _repository.GetAllRateAnalysis(objLoggedInUserSession.SelectedProjectId);

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

            // return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateEdit(long Id = 0)
        {
            RateAnalysisModel objRateAnalysisModel;
            if (Id == 0)
            {
                objRateAnalysisModel = new RateAnalysisModel();
            }
            else
            {
                objRateAnalysisModel = _repository.GetRateAnalysisById(Id);
            }
            return Json(objRateAnalysisModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddEditRateAnalysis(RateAnalysisModel objRateAnalysisModel)
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            objRateAnalysisModel.ProjectId = objLoggedInUserSession.SelectedProjectId;
            objRateAnalysisModel.DSRId = objLoggedInUserSession.SelectedDSRId;
            if (objRateAnalysisModel.id == 0)
            {
                objRateAnalysisModel.CreatedBy = objRateAnalysisModel.ModifiedBy = Convert.ToInt32(objLoggedInUserSession.LoggedInUserId);
            }
            else
            {
                objRateAnalysisModel.ModifiedBy = Convert.ToInt32(objLoggedInUserSession.LoggedInUserId);
            }
            var Result = _repository.AddEditRateAnalysis(objRateAnalysisModel);

            return View("Index");
        }

        public ActionResult DeleteRateAnalysis(long id)
        {
            try
            {
                var Result = _repository.DeleteRateAnalysisById(id);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
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

            return Json(new { ItemOfWorkList = lists }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItemOfLeadCharges(string request)
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var lists = _repository.GetItemOfLeadCharges(request, Convert.ToInt16(objLoggedInUserSession.SelectedProjectId)).ToList();
            return Json(new { ItemOfWorkList = lists }, JsonRequestBehavior.AllowGet);
        }
    }
}