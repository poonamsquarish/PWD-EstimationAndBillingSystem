using SQEstimationAndBillingSystem.Models;
using SQEstimationAndBillingSystem.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SQEstimationAndBillingSystem.Controllers
{
    public class HomeController : LanguageController
    {

        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            return View();
        }

        public ActionResult GetAllSlideShowImages()
        {

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/images"));
            FileInfo[] file = dir.GetFiles();
            List<string> list = new List<string>();
            foreach (FileInfo file2 in file)
            {
                if (file2.Extension == ".jpg" || file2.Extension == ".jpeg" || file2.Extension == ".gif" || file2.Extension == ".png")
                {
                    list.Add(file2.Name);
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Bind DSR
        /// </summary>
        private List<DSRFileModel> BindDSR()
        {
            List<DSRFileModel> lstDSRModel = null;
            try
            {
                lstDSRModel = (new DSRRepository()).GetAllDSR();
                return lstDSRModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Bind DSR Projcets
        /// </summary>
        public List<ProjectDetailsModel> BindDSRProjects(long dsrid)
        {
            List<ProjectDetailsModel> lstDSRProjectModel = null;
            try
            {
                lstDSRProjectModel = (new DSRRepository()).GetAllProjectsOfDSR(dsrid);
                return lstDSRProjectModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PartialViewResult RePopulateDSRProjects(long? id)
        {
            try
            {
                LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
                if (Session["LoggedInUserSession"] != null)
                {
                    objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
                }
                //objLoggedInUserSession.SelectedProjectName = null;
                //objLoggedInUserSession.SelectedProjectId = null;
                List<ProjectDetailsModel> DSRProjectModel = BindDSRProjects(objLoggedInUserSession.SelectedDSRId);

                if (!id.HasValue)
                {
                    if (DSRProjectModel.Count > 0)
                    {
                        objLoggedInUserSession.SelectedProjectName = DSRProjectModel.First().ProjectName;
                        objLoggedInUserSession.SelectedProjectId = DSRProjectModel.First().id;
                    }
                }
                else
                {
                    if (DSRProjectModel.Count > 0 && id > 0)
                    {
                        objLoggedInUserSession.SelectedProjectName = DSRProjectModel.FirstOrDefault(d => d.id == id).ProjectName;
                        objLoggedInUserSession.SelectedProjectId = DSRProjectModel.FirstOrDefault(d => d.id == id).id;
                    }
                }

                return PartialView("_ListDSRProjectsPartial", DSRProjectModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PartialViewResult PopulateDSRProjects(long? id)
        {
            try
            {
                LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
                if (Session["LoggedInUserSession"] != null)
                {
                    objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
                }

                List<ProjectDetailsModel> DSRProjectModel = BindDSRProjects(id.HasValue ? id.Value : 0);
                var df = objLoggedInUserSession.SelectedProjectId != 0 ? DSRProjectModel.Any(d => d.id == objLoggedInUserSession.SelectedProjectId) : false;
                if (!df)
                {
                    if (!id.HasValue)
                    {
                        if (DSRProjectModel.Count > 0)
                        {
                            objLoggedInUserSession.SelectedProjectName = DSRProjectModel.First().ProjectName;
                            objLoggedInUserSession.SelectedProjectId = DSRProjectModel.First().id;
                        }
                    }
                    else
                    {
                        if (DSRProjectModel.Count > 0 && id > 0)
                        {
                            objLoggedInUserSession.SelectedProjectName = DSRProjectModel.First(d => d.DSRId == id).ProjectName;
                            objLoggedInUserSession.SelectedProjectId = DSRProjectModel.First(d => d.DSRId == id).id;
                        }
                    }
                }
                return PartialView("_ListDSRProjectsPartial", DSRProjectModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PartialViewResult PopulateDSR(long? id)
        {
            try
            {
                LoggedInUserSessionModel objLoggedInUserSession = new LoggedInUserSessionModel();
                if (Session["LoggedInUserSession"] != null)
                {
                    objLoggedInUserSession = (LoggedInUserSessionModel)(Session["LoggedInUserSession"]);
                }

                List<DSRFileModel> DSRModel = BindDSR();
                if (objLoggedInUserSession.SelectedDSRName == null || id.HasValue)
                {
                    if (!id.HasValue)
                    {
                        if (DSRModel.Count > 0)
                        {
                            objLoggedInUserSession.SelectedDSRName = DSRModel.First().DSRName;
                            objLoggedInUserSession.SelectedDSRId = DSRModel.First().id;
                            objLoggedInUserSession.IsLatestDSR = DSRModel.First().IsLatestDSR;
                        }
                    }
                    else
                    {
                        if (DSRModel.Count > 0 && id > 0 && DSRModel.Any(d => d.id == id))
                        {
                            objLoggedInUserSession.SelectedDSRName = DSRModel.FirstOrDefault(d => d.id == id).DSRName;
                            objLoggedInUserSession.SelectedDSRId = id.Value;
                            objLoggedInUserSession.IsLatestDSR = DSRModel.FirstOrDefault(d => d.id == id).IsLatestDSR;
                        }
                    }
                }
                else
                {
                    //objLoggedInUserSession.SelectedDSRName = null;
                    //objLoggedInUserSession.SelectedDSRId = null;
                }


                return PartialView("_ListDSRPartial", DSRModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}