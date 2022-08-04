using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homepage.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (proveCookies())
            {
                setSettings("Homepage");
            }
            else
            {
                return RedirectToAction("Login", "Secure");
            }

            return View();
        }

        internal void setSettings(string activeSite) =>  ViewBag.ActiveSite = activeSite;


        /** Methode überprüft ob die Session gültig ist , über Cookies **/
        internal bool proveCookies()
        {
            HttpCookie cookie = Request.Cookies["userData"];

            if(cookie != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}