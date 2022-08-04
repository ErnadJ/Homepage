using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.GlobalDefinitions.Collections;
using Homepage.Web.RESTCaller.APIAccess;

namespace Homepage.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectAPI _projectAPI;

        public ProjectsController()
        {
            _projectAPI = new ProjectAPI();
            _projectAPI.OnError += new ProjectAPI.OnErrorEventHandler(ErrorLogger);
        }

        public ActionResult Projects()
        {
            List<ListProjects> listProjects = new List<ListProjects>();

            /** Auf ViewBag wird die Liste 'listProjects' übergeben, damit 
             * man es auf der View visualisieren kann **/
            ViewBag.ListProjects = listProjects;

            try
            {
                if (proveCookies())
                {
                    setSettings("Project");

                    ViewBag.ListProjects = _projectAPI.GetProjects();
                }
                else
                {
                    return RedirectToAction("Login", "Secure");
                }
            }
            catch(Exception ex)
            {
                Log4net.Logger.Error(ex.Message);
            }

            return View();
        }

        internal void setSettings(string activeSite) => ViewBag.ActiveSite = activeSite;

        /** Methode überprüft ob die Session gültig ist , über Cookies **/
        internal bool proveCookies()
        {
            HttpCookie cookie = Request.Cookies["userData"];

            if (cookie != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void ErrorLogger(string message) => Log4net.Logger.Error("[PROJECTCONTROLLER] " + message);
    }
}