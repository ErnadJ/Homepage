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
    public class ConfigController : ApiController
    {
        ConfigDB _configDB = new ConfigDB();

        [HttpGet]
        public Config GetConfig()
        {
            Config currentConfig = new Config();

            try
            {
                /** Ins Modell wird die Config aus der Datenbank geladen **/
                currentConfig = _configDB.GetConfig();
            }
            catch(Exception ex)
            {
                Log4net.Logger.Error(ex.Message);
            }

            return currentConfig;
        }
    }
}
