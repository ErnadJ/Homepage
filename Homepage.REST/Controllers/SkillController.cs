using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Homepage.GlobalDefinitions.Collections;
using Homepage.Backend.DataAccess;

namespace Homepage.REST.Controllers
{
    public class SkillController : ApiController
    {
        RepositoriesDB _repositoriesDB = new RepositoriesDB();

        [HttpGet]
        public List<ListSkills> GetSkills()
        {
            List<ListSkills> listSkills = new List<ListSkills>();

            try
            {
                /** In die Modell Liste werden die Skills aus der Datenbank geladen **/
                listSkills = _repositoriesDB.GetSkills();
            }
            catch (Exception ex)
            {
                Log4net.Logger.Error(ex.Message);
            }

            return listSkills;
        }
    }
}
