using SQEstimationAndBillingSystem.Models;
using SQEstimationAndBillingSystem.Repository;
using System;
using System.Web.Mvc;

namespace SQEstimationAndBillingSystem.Controllers
{
    [Authorize]
    public class RecaptionalSheetController : Controller
    {
        RecaptionalRepository _repository = new RecaptionalRepository();
        
        public ActionResult Index()
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var Result = _repository.GetAllRecaptional(objLoggedInUserSession.SelectedProjectId != 0 ? objLoggedInUserSession.SelectedProjectId : 0);
            return View(Result);
        }

        [HttpPost]
        public ActionResult AddEditRecaptional(RecaptionalModel objRecaptionalModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
                    if (Session["LoggedInUserSession"] != null)
                    {
                        objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
                        objRecaptionalModel.DSRId = Convert.ToInt32(objLoggedInUserSession.SelectedDSRId);
                        objRecaptionalModel.ProjectId = Convert.ToInt32(objLoggedInUserSession.SelectedProjectId);
                    }
                    if (objRecaptionalModel.id == 0)
                    {
                        objRecaptionalModel.CreatedBy = objRecaptionalModel.ModifiedBy = objLoggedInUserSession.LoggedInUserId;
                    }
                    else
                    {
                        objRecaptionalModel.ModifiedBy = objLoggedInUserSession.LoggedInUserId;
                    }

                    var Result = _repository.AddEditRecaptional(objRecaptionalModel);

                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }
    }
}