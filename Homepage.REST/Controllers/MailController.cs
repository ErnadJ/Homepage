using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Homepage.Backend.BusinessLogic;
using Homepage.Backend.DataAccess;
using Homepage.GlobalDefinitions.Models;

namespace Homepage.REST.Controllers
{
    public class MailController : ApiController
    {
        private MailManager _mailManager;
        private SMTPDB _smtpDb;

        public MailController()
        {
            _mailManager = new MailManager();
            _smtpDb = new SMTPDB();
        }

        [HttpPost]
        public int SendMailMessage(string mailAddress, string message)
        {
            /* Result 0 = "Fehlgeschlagen "
             * Result 1 = "Erfolgreich"
             */

            int result = 0;

            try
            {
                if(!string.IsNullOrEmpty(mailAddress) && !string.IsNullOrEmpty(message))
                {
                    _mailManager.CurrentSMTPConfig = _smtpDb.GetSMTPConfig();
                    _mailManager.CurrentContact.MailAddress = mailAddress;
                    _mailManager.CurrentContact.Message = message;

                    /** Prozess wird vom Kontakt-Formular gestartet **/
                    if (_mailManager.SendMessageMail())
                    {
                        result = 1;
                    }
                }
            }
            catch(Exception ex)
            {
                Log4net.Logger.Error(ex.Message);
            }

            return result;
        }
    }
}
