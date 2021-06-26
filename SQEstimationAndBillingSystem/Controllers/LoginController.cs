using SQEstimationAndBillingSystem.Models;
using SQEstimationAndBillingSystem.Repository;
using System.Web.Mvc;
using System.Web.Security;


namespace SQEstimationAndBillingSystem.Controllers
{
    public class LoginController : LanguageController
    {
        UserRepository _repository = new UserRepository();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize]
        //public ActionResult Profile()
        //{
        //    return View();
        //}

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(UserModel user)
        {
            UserModel objUserModel = new UserModel();
            objUserModel = _repository.ValidateUser(user.UserName, user.Password);

            if (objUserModel != null)
            {
                if (objUserModel.ID != 0)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
                    objLoggedInUserSession.LoggedInUserId= objUserModel.ID;
                    objLoggedInUserSession.LoggedInUserName= objUserModel.UserName;
                    objLoggedInUserSession.RoleId=objUserModel.RoleID;
                    objLoggedInUserSession.RoleName=objUserModel.RoleName;
                    Session["LoggedInUserSession"] = objLoggedInUserSession;
                    //Session["UName"] = objUserModel.UserName;
                    //Session["URoleName"] = objUserModel.RoleName;
                    //Session["UserId"] = objUserModel.ID;
                    //Session["RoleId"] = objUserModel.RoleID;
                    return RedirectToAction("Index", "Home");
                }

            }
            ViewBag.Message = SQEstimationAndBillingSystem.Resources.InvalidUserLoginMsg;
            return View(objUserModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AdminLoginForClose(string UserName, string Password)
        {
            UserModel objUserModel = new UserModel();
            objUserModel = _repository.ValidateUser(UserName,Password);

            if (objUserModel != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.Message = SQEstimationAndBillingSystem.Resources.InvalidUserLoginMsg;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
          
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}