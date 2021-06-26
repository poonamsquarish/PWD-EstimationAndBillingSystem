using SQEstimationAndBillingSystem.Helper;
using SQEstimationAndBillingSystem.Models;
using SQEstimationAndBillingSystem.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQEstimationAndBillingSystem.Controllers
{
    public class ProjectDetailsController : Controller
    {
        ProjectDetailsRepository _repository = new ProjectDetailsRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllProjectDetails(jQueryDataTableParamModel param)
        {
            var Result = _repository.GetAllProjectDetails();

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
            ProjectDetailsModel objProjectDetailsModel;
            if (Id == 0)
            {
                objProjectDetailsModel = new ProjectDetailsModel();
            }
            else
            {
                objProjectDetailsModel = _repository.GetProjectDetailsById(Id);
            }
            return Json(objProjectDetailsModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddEditProjectDetails(HttpPostedFileBase postedFile, ProjectDetailsModel objProjectDetailsModel)
        {
          

            try
            {
                if (ModelState.IsValid)
                {
                    //if (objProjectDetailsModel.id == 0)
                    //{
                    //    objProjectDetailsModel.CreatedBy = objProjectDetailsModel.ModifiedBy = Convert.ToInt32(Session["UserId"]);
                    //}
                    //else
                    //{
                    //    objLeadChargesModel.ModifiedBy = Convert.ToInt32(Session["UserId"]);
                    //}
                    LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
                    if (Session["LoggedInUserSession"] != null)
                    {
                        objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
                    }
                    objProjectDetailsModel.DSRId =Convert.ToInt16( objLoggedInUserSession.SelectedDSRId);
                    var Result = _repository.AddEditProjectDetails(objProjectDetailsModel);
                    
                    if (postedFile != null )
                    {
                        string path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        if (objProjectDetailsModel.id == 0)
                        {
                            postedFile.SaveAs(path + Path.GetFileName(Result.Split('_')[1] + '_' + postedFile.FileName));
                            
                        }
                        else
                        {
                            postedFile.SaveAs(path + Path.GetFileName(objProjectDetailsModel.id.ToString() + '_' + postedFile.FileName));
                        }
                        //ViewBag.Message = "File uploaded successfully.";
                    }

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

        public ActionResult DeleteProjectDetails(long id)
        {
            try
            {
                var Result = _repository.DeleteProjectDetailsById(id);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetAllDocuments(long id)
        {

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Uploads"));
            FileInfo[] file = dir.GetFiles();
            List<string> list = new List<string>();
            foreach (FileInfo file2 in file)
            {
                if(file2.Name.Split('_')[0] == id.ToString())
                 list.Add(file2.Name);
                
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadFile(string FileName = null)
        {
           // string path = Server.MapPath("~/Uploads");
           // byte[] fileBytes = System.IO.File.ReadAllBytes(path +"\\"+ FileName);
           //// string fileName = "filename.extension";
           // return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
            var FileVirtualPath = Server.MapPath("~/Uploads/" + FileName);

            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));

        }

    }
}