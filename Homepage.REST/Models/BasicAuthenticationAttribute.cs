using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Homepage.Backend.DataAccess;

namespace Homepage.REST.Models
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private static RESTUserDB _restUserDB;

        public BasicAuthenticationAttribute()
        {
            _restUserDB = new RESTUserDB();
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers
                    .Authorization.Parameter;

                /** Authcode wird decodiert in 'Username:Password' Format **/
                var decodeauthToken = System.Text.Encoding.UTF8.GetString(
                    Convert.FromBase64String(authToken));

                /** Split nach ":"  **/
                var arrUserNameandPassword = decodeauthToken.Split(':');

                if (IsAuthorizedUser(arrUserNameandPassword[0], arrUserNameandPassword[1]))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(
                           new GenericIdentity(arrUserNameandPassword[0]), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

        public static bool IsAuthorizedUser(string Username, string Password)
        {
            /** Daten werden in der Datenbank verglichen **/
            if(_restUserDB.GetRestUser(Username, Password))
            {
                return true;
            }

            return false;            
        }
    }
}