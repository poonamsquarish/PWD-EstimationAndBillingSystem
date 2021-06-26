using SQEstimationAndBillingSystem.Models;
using SQEstimationAndBillingSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQEstimationAndBillingSystem.Controllers
{
    public class UserController : LanguageController
    {
        // GET: User
        UserRepository _repository = new UserRepository();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllUser()
        {

            var Result = _repository.GetAllUser();
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserById(int UserId)
        {
            var Result = _repository.GetUserById(UserId);
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddEditUser(UserModel objEmployeeModel)
        {
            var Result = "Added";
            try
            {
                Result = _repository.AddEditUser(objEmployeeModel);
                return Json(Result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteUser(int UserId)
        {
            var Result = "Deleted";
            try
            {
                Result = _repository.DeleteUserById(UserId);
                return Json(Result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult UserDropDownLookUp()
        //{
        //    var Result = _repository.UserDropDownLookUp();
        //    return Json(Result, JsonRequestBehavior.AllowGet);
        //}
    }
}