using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Homepage.GlobalDefinitions.Collections;
using Newtonsoft.Json;

namespace Homepage.Web.RESTCaller.APIAccess
{
    public class ProjectAPI
    {
        private string _username = Properties.Settings.Default.REST_Username;
        private string _password = Properties.Settings.Default.REST_Password;
        private string _restUrl = Properties.Settings.Default.REST_Url;

        public List<ListProjects> GetProjects()
        {
            List<ListProjects> listProjects = new List<ListProjects>();

            string url = _restUrl + "/Project/GetProjects";

            try
            {
                /** WebRequest wird generiert über meine URL **/
                WebRequest request = WebRequest.Create(url);
                request.Timeout = 20000;

                /** Credentials sind optional, jedoch werden sie hierbei benötigt
                 * aufgrund der Authentification **/
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(_username + ":" + _password));

                /** Response **/  
                WebResponse response = request.GetResponse();

                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    /** Ergebnis wird in Modell geladen **/
                    listProjects = JsonConvert.DeserializeObject<List<ListProjects>>(responseFromServer);
                }

                /** Response immer schließen **/
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listProjects;
        }
    }
}
