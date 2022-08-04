using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Homepage.Backend.DataAccess;
using Homepage.GlobalDefinitions.Models;

namespace Homepage.REST.Controllers
{
    public class SMTPConfigController : ApiController
    {
        SMTPDB _smtpConfigDB = new SMTPDB();

        public SMTPConfig GetSMTPConfig()
        {
            SMTPConfig currentSMTPConfig = new SMTPConfig();

            try
            {
                /** Ins Modell wird die SMTP Config aus der Datenbank geladen **/
                currentSMTPConfig = _smtpConfigDB.GetSMTPConfig();
            }
            catch (Exception ex)
            {
                Log4net.Logger.Error(ex.Message);
            }

            return currentSMTPConfig;
        }
    }
}
