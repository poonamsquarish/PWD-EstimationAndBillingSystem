using SQEstimationAndBillingSystem.Models;
using SQEstimationAndBillingSystem.Repository;
using System.Web.Mvc;

namespace SQEstimationAndBillingSystem.Controllers
{
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
    }
}