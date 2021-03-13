using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.Web.RESTCaller.APIAccess
{
    public class MailAPI
    {
        private string _username = Properties.Settings.Default.REST_Username;
        private string _password = Properties.Settings.Default.REST_Password;
        private string _restUrl = Properties.Settings.Default.REST_Url;

        public  string sendMailMessage(string mailAddress, string message)
        {
            string responseBody = "";

            try
            {
                string data = string.Format("mailAddress={0}&message={1}", mailAddress, message);

                HttpClient client = new HttpClient();
                StringContent queryString = new StringContent("");

                /** Credentials benötigt für Authentification **/
                var byteArray = Encoding.ASCII.GetBytes(_username+":"+_password);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                string url = _restUrl + "/Mail/SendMailMessage?" + data;

                using (HttpResponseMessage response =  client.PostAsync(new Uri(url), queryString).Result)
                {
                    response.EnsureSuccessStatusCode();
                    responseBody =  response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseBody;
        }
    }
}
