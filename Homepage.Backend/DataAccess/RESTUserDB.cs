using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.Backend.DataAccess
{
    public class RESTUserDB
    {
        /*  
         * Klasse prüft ob der User in der Tabelle 'dbo.Users' exestiert.
         */

        private SQL _sqlManager;

        public RESTUserDB()
        {
            _sqlManager = new SQL();
        }

        /** Methode vergleicht Username und Passwort in der Tabelle "Users" **/
        public bool GetRestUser(string username, string password)
        {
            return getRestUser(username, password);
        }

        /** Methode vergleicht Username und Passwort in der Tabelle "Users" **/
        private bool getRestUser(string username, string password)
        {
            DataTable userTable = new DataTable();

            string sqlQuery = "";

            try
            {
                sqlQuery = "GetRestUser";

                /** SQL Select Statement gibt die Tabelle "Users" zurück **/
                userTable = _sqlManager.ExecuteSelect(sqlQuery, new string[] { "@Username", "@Password"}, new object[] { username, password });

                /** Wenn ein Eintrag gefunden wurde , dann exestiert der User **/
                if (userTable.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }
    }
}
