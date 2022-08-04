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
    public class ProjectController : ApiController
    {
        RepositoriesDB _repositoriesDB = new RepositoriesDB();

        [HttpGet]
        public List<ListProjects> GetProjects()
        {
            List<ListProjects> listProjects = new List<ListProjects>();

            try
            {
                /** In die Modell Liste werden die Projekte aus der Datenbank geladen **/
                listProjects = _repositoriesDB.GetProjects();
            }
            catch (Exception ex)
            {
                Log4net.Logger.Error(ex.Message);
            }

            return listProjects;
        }
    }
}
