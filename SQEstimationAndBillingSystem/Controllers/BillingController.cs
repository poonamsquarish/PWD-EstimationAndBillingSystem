using SQEstimationAndBillingSystem.Models;
using SQEstimationAndBillingSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQEstimationAndBillingSystem.Controllers
{
    public class BillingController : Controller
    {
        BillingRepository _repository = new BillingRepository();

        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult GetAllBilling()
        {
            var Result = _repository.GetAllBilling();

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateEdit(long Id = 0)
        {
            BillingModel objBillingModel;
            if (Id == 0)
            {
                objBillingModel = new BillingModel();
            }
            else
            {
                objBillingModel = _repository.GetBillingById(Id);
           
            }
            return View("CreateEdit", objBillingModel);
        }

        [HttpPost]
        public ActionResult AddEditBilling(BillingModel objBillingModel)
        {
            var Result = _repository.AddEditBilling(objBillingModel);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        
    }
}