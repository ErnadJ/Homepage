using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Homepage.Web.RESTCaller.APIAccess;
using Homepage.GlobalDefinitions.Models;

namespace Homepage.Web.Controllers
{
    public class SecureController : Controller
    {
        private ConfigAPI _configAPI;
        private Config _currentConfig;

        public SecureController()
        {
            _configAPI = new ConfigAPI();
            _currentConfig = new Config();

            _currentConfig = _configAPI.GetConfig();

            _configAPI.OnError += new ConfigAPI.OnErrorEventHandler(ErrorLogger);
        }

        public ActionResult Login(string authCode)
        {
            string configAuthCode = _currentConfig.AuthCode;

            try
            {
                /** Wenn die Webseite im Wartungsmodus ist, ist Sie nicht erreichbar
                 * man wird weitergeleitet auf die Wartungsmodus View**/
                if (!_currentConfig.MaintenanceMode)
                {
                    if (proveCookies())
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(authCode))
                        {
                            /** Wenn der mitgegebene AuthCode der selbe AuthCode 
                             * wie aus der Datenbank ist, dann kann man die Webseite betreten **/
                            if (authCode.Equals(configAuthCode))
                            {
                                setCookies(authCode);

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.Error = "Der Code ist leider nicht gültig !";
                            }
                        }
                        else
                        {
                            removeCookies();
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Maintenance", "Secure");
                }
            }
            catch(Exception ex)
            {
                Log4net.Logger.Error(ex.Message);
            }

            return View();
        }

        public ActionResult Logout()
        {
            HttpCookie cookie = Request.Cookies["userData"];

            if (cookie != null)
            {
                removeCookies();
            }
            else
            {
                return RedirectToAction("Login", "Secure");
            }

            return RedirectToAction("Login", "Secure");
        }

        public ActionResult Maintenance()
        {
            if (!_currentConfig.MaintenanceMode)
            {
                return RedirectToAction("Login", "Secure");
            }

            return View();
        }

        /** Methode zum setzen der Cookies **/
        private void setCookies(string authCode)
        {
            HttpCookie cookie = new HttpCookie("userData");

            try
            {
                cookie.Values["authCode"] = authCode;

                cookie.Expires = DateTime.Now.AddHours(1);
                cookie.Secure = true;

                FormsAuthentication.SetAuthCookie(authCode, false);
            }
            catch (Exception ex)
            {
                Log4net.Logger.Error(ex.Message);
            }
        }

        /** Methode zum löschen der Cookies **/
        private void removeCookies()
        {
            HttpCookie cookie = new HttpCookie("userData");

            try
            {
                if (cookie != null)
                {
                    cookie.Values.Remove("authCode");

                    cookie.Expires = DateTime.Now.AddDays(-1);

                    Response.Cookies.Add(cookie);
                }

                FormsAuthentication.SignOut();
            }
            catch (Exception ex)
            {
                Log4net.Logger.Error(ex.Message);
            }
        }

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

        void ErrorLogger(string message) => Log4net.Logger.Error("[SECURECONTROLLER] " + message);
    }
}