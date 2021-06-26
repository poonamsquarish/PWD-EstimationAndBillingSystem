using SQEstimationAndBillingSystem.Helper;
using SQEstimationAndBillingSystem.Models;
using SQEstimationAndBillingSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SQEstimationAndBillingSystem.Controllers
{
    public class DSRController : Controller
    {
        DSRRepository _repository = new DSRRepository();
        // GET: DSR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllDSRDetails(jQueryDataTableParamModel param)
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var Result = _repository.GetAllDSRDetails(Convert.ToInt32(objLoggedInUserSession.SelectedDSRId));

            if (!(Result is null))
            {
                var jsonResult = Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = Result.Count,
                    iTotalDisplayRecords = Result.Count,
                    aaData = Result
                }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
                //return Json(new
                //{
                //    sEcho = param.sEcho,
                //    iTotalRecords = Result.Count,
                //    iTotalDisplayRecords = Result.Count,
                //    aaData = Result
                //}, JsonRequestBehavior.AllowGet);
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

        public ActionResult CheckDSRExists()
        {
            var ResultDsr = _repository.CheckDSRExists();
            if(ResultDsr > 0)
            {
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
          

        }

        public ActionResult UploadDSR()
        {

            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            int DSRId = 0;
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
                DSRId = Convert.ToInt32(objLoggedInUserSession.SelectedDSRId);
            }

            var Result = _repository.InsertData(DSRId);
                return Json(Result, JsonRequestBehavior.AllowGet);
           
        }

        public ActionResult CreateEdit(long Id = 0)
        {
            DSRModel objDSRDetailsModel;
            if (Id == 0)
            {
                objDSRDetailsModel = new DSRModel();
            }
            else
            {
                objDSRDetailsModel = _repository.GetDSRDetailsById(Id);
            }
            return Json(objDSRDetailsModel, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AddEditDSRDetails(DSRModel objDSRDetailsModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
                    if (Session["LoggedInUserSession"] != null)
                    {
                        objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
                        objDSRDetailsModel.DSRId = Convert.ToInt32(objLoggedInUserSession.SelectedDSRId);
                    }
                    if (objDSRDetailsModel.id == 0)
                    {
                        objDSRDetailsModel.CreatedBy = objDSRDetailsModel.ModifiedBy = objLoggedInUserSession.LoggedInUserId;
                    }
                    else
                    {
                        objDSRDetailsModel.ModifiedBy = objLoggedInUserSession.LoggedInUserId;
                    }

                    var Result = _repository.AddEditDSRDetails(objDSRDetailsModel);

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

        public ActionResult DeleteDSRDetails(long id)
        {
            try
            {
                var Result = _repository.DeleteDSRDetailsById(id);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}