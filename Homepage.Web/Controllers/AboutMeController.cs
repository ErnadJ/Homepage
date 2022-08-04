using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.GlobalDefinitions.Collections;
using Homepage.Web.RESTCaller.APIAccess;

namespace Homepage.Web.Controllers
{
    public class AboutMeController : Controller
    {
        private SkillAPI _skillAPI;

        public AboutMeController()
        {
            _skillAPI = new SkillAPI();
            _skillAPI.OnError += new SkillAPI.OnErrorEventHandler(ErrorLogger)
        }

        public ActionResult Me()
        {
            if (proveCookies())
            {
                setSettings("AboutMe");
            }
            else
            {
                return RedirectToAction("Login", "Secure");
            }

            return View();
        }

        public ActionResult Skills()
        {
            List<ListSkills> listSkills = new List<ListSkills>();

            /** Auf ViewBag wird die Liste 'listSkills' übergeben, damit 
             * man es auf der View visualisieren kann **/
            ViewBag.ListSkills = listSkills;

            try
            {
                if (proveCookies())
                {
                    setSettings("Skills");

                    ViewBag.ListSkills = _skillAPI.GetSkills();
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

        public ActionResult CV()
        {
            if (proveCookies())
            {
                setSettings("CV");
            }
            else
            {
                return RedirectToAction("Login", "Secure");
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

        void ErrorLogger(string message) => Log4net.Logger.Error("[ABOUTMECONTROLLER] " + message);
    }
}