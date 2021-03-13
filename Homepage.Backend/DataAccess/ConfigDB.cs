using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homepage.GlobalDefinitions.Models;

namespace Homepage.Backend.DataAccess
{
    public class ConfigDB
    {
        /*  
         * Klasse gibt die Daten aus der Tabelle 'dbo.Config' zurück.
         */

        private Config _currentConfig;
        private SQL _sqlManager;

        public ConfigDB()
        {
            _currentConfig = new Config();
            _sqlManager = new SQL();
        }

        public Config CurrentConfig
        {
            get
            {
                return _currentConfig;
            }
            set
            {
                _currentConfig = value;
            }
        }

        /** Methode gibt die Config Tabelle aus der Datenbank zurück **/
        public Config GetConfig()
        {
            return getConfig();
        }

        private Config getConfig()
        {
            Config currentConfig = new Config();

            DataTable configTable = new DataTable();

            string sqlQuery = "";

            try
            {
                sqlQuery = "GetConfig";

                /** SQL Select Statement gibt die Tabelle "Config" zurück **/
                configTable = _sqlManager.ExecuteSelect(sqlQuery, new string[] { }, new object[] { });

                /** Tabelle wird in einer Schleife durchlaufen und ins Modell geladen **/
                if(configTable.Rows.Count > 0)
                {
                    for(int i = 0; i < configTable.Rows.Count; i++)
                    {
                        currentConfig.Id = Convert.ToInt64(configTable.Rows[i]["ID"]);
                        currentConfig.WebsiteName = (configTable.Rows[i]["WebsiteName"].ToString());
                        currentConfig.Author = (configTable.Rows[i]["Author"].ToString());
                        currentConfig.ContactMailAddress = (configTable.Rows[i]["ContactMailAddress"].ToString());
                        currentConfig.AuthCode = (configTable.Rows[i]["AuthCode"].ToString());
                        currentConfig.MaintenanceMode = (bool)(configTable.Rows[i]["MaintenanceMode"]);
                        currentConfig.CreationDate = (DateTime)configTable.Rows[i]["CreationDate"];
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return currentConfig;
        }
    }
}
