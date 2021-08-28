using Newtonsoft.Json;
using SQEstimationAndBillingSystem.FilterAttributes;
using SQEstimationAndBillingSystem.Models;
using SQEstimationAndBillingSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQEstimationAndBillingSystem.Controllers
{
    [Authorize]
    public class LabTestController : Controller
    {
        LabTestRepository _repository = new LabTestRepository();
        // GET: LabTest
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Add Edit Lab Test
        /// </summary>
        /// <param name="objLabTestModel"></param>
        /// <returns></returns>
        [ValidateAjax]
        [HttpPost]
        public ActionResult AddEditLabTest(LabTestBasicModel objLabTestModel)
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
                    objLabTestModel.ProjectId = objLoggedInUserSession.SelectedProjectId;
                    objLabTestModel.DSRId = Convert.ToInt16(objLoggedInUserSession.SelectedDSRId);
                    if (objLabTestModel.id == 0)
                    {
                        objLabTestModel.CreatedBy = objLabTestModel.ModifiedBy = objLoggedInUserSession.LoggedInUserId;
                    }
                    else
                    {
                        objLabTestModel.ModifiedBy = objLoggedInUserSession.LoggedInUserId;
                    }
                    if (!String.IsNullOrEmpty(objLabTestModel.NameOfTestJson))
                    {
                        objLabTestModel.NameOfTestList = JsonConvert.DeserializeObject<List<NameOfTestModel>>(objLabTestModel.NameOfTestJson);
                    }
                    var Result = _repository.AddEditLabTest(objLabTestModel);
                    return Json(new { success = true, message = "Saved successfully !!!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return PartialView("CreateEdit", objLabTestModel);
                }
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteLabTest(long id)
        {
            try
            {
                var Result = _repository.DeleteLabTestById(id);
                return Json(new { success = Result, message = "Record Deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Manage Lab Test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ManageLabTest(string id)
        {
            try
            {
                LabTestBasicModel objLabTestModel;
                if (Convert.ToInt64(id) != 0)
                {
                    objLabTestModel = _repository.GetLabTestById(Convert.ToInt64(id));

                    if (objLabTestModel != null)
                    {
                        objLabTestModel = _repository.GetLabTestById(Convert.ToInt64(id));
                        objLabTestModel.NameOfTestList = JsonConvert.DeserializeObject<List<NameOfTestModel>>(objLabTestModel.NameOfTestJson);
                    }
                }
                else
                {
                    objLabTestModel = new LabTestBasicModel();
                }
                return PartialView("CreateEdit", objLabTestModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllLabTestDetails()
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var Result = _repository.GetAllLabTestDetails(objLoggedInUserSession.SelectedProjectId);
            return Json(Result, JsonRequestBehavior.AllowGet);

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
            var list = lists.Select(r => new { value = Convert.ToString(r.DescriptionOfTheItem), id = r.id.ToString() });

            return Json(new { ItemOfWorkList = list }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetMaterialList
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMaterialList(string request)
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var lists = _repository.GetAllSearchMaterial(request, Convert.ToInt16(objLoggedInUserSession.SelectedDSRId), Convert.ToInt16(objLoggedInUserSession.SelectedProjectId)).ToList();
            var list = lists.Select(r => new { value = Convert.ToString(r.Material), id = r.Id.ToString(), unit = r.Unit.ToString() });

            return Json(new { MaterialList = list }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetNameOfTestList
        /// </summary>
        /// <returns></returns>
        public JsonResult GetNameOfTestList(string request)
        {
            LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
            if (Session["LoggedInUserSession"] != null)
            {
                objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
            }
            var lists = _repository.GetAllNameOfTest(request, Convert.ToInt16(objLoggedInUserSession.SelectedDSRId)).ToList();
            var list = lists.Select(r => new { value = Convert.ToString(r.NameOfTest), id = r.Id.ToString(), rate = r.RateInRs.ToString() });

            return Json(new { MaterialList = list }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Client single point of contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetNameOfTest(long? id)
        {
            List<NameOfTestModel> nameOfTestModel = null;
            try
            {
                if (id != null && id > 0)
                {
                    var nameOfTestsForMaterial = _repository.GetMaterialNameOfTests(id.Value);

                    if (nameOfTestsForMaterial != null)
                    {
                        nameOfTestModel = nameOfTestsForMaterial;
                    }
                }
                else
                {
                    nameOfTestModel = new List<NameOfTestModel>();
                }
                return Json(new { data = nameOfTestModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}