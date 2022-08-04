using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homepage.Web.RESTCaller.APIAccess;
using Homepage.GlobalDefinitions.Models;
using System.Threading.Tasks;

namespace Homepage.Web.Controllers
{
    public class ContactController : Controller
    {
        private MailAPI _mailAPI;
        private string _result;

        public ContactController()
        {
            _mailAPI = new MailAPI();
            _result="";
        }

        [HttpPost]
        public  ActionResult ContactFormular(Contact currentContact)
        {
            if (ModelState.IsValid)
            {
                string mailAddress = currentContact.MailAddress;
                string mailMessage = currentContact.Message;

                try
                {
                    /** Das Kontakt-Formular versendet über die REST-API eine E-Mail **/
                    sendMailMessage(mailAddress, mailMessage);

                    if (string.IsNullOrEmpty(_result))
                    {
                        ViewBag.Error = "Fehlgeschlagen";
                    }
                    else
                    {
                        ViewBag.Successful = "Erfolgreich versendet !";
                    }
                }
                catch (Exception ex)
                {
                    Log4net.Logger.Error(ex.Message);
                }
            }

            return View();
        }

        public ActionResult ContactFormular()
        {
            if (proveCookies())
            {
                setSettings("Contact");
            }
            else
            {
                return RedirectToAction("Login", "Secure");
            }

            return View();
        }

        internal void setSettings(string activeSite)
        {
            ViewBag.ActiveSite = activeSite;
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

        /** REST-API Aufruf zum versenden der E-Mail **/
        private void sendMailMessage(string mailAddress, string message)
        {
            if (!string.IsNullOrEmpty(mailAddress) && !string.IsNullOrEmpty(message))
            {
                _result =  _mailAPI.sendMailMessage(mailAddress, message);
            }
        }
    }
}