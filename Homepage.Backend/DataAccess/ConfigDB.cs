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
        public event OnErrorEventHandler OnError;
        public delegate void OnErrorEventHandler(string message);

        private Config _currentConfig;
        private SQL _sqlManager;

        public ConfigDB()
        {
            _currentConfig = new Config();
            _sqlManager = new SQL();
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
                        if (!Convert.IsDBNull(configTable.Rows[i]["ID"]))
                        {
                            currentConfig.Id = Convert.ToInt64(configTable.Rows[i]["ID"]);
                        }
                        if (!Convert.IsDBNull(configTable.Rows[i]["WebsiteName"]))
                        {
                            currentConfig.WebsiteName = (configTable.Rows[i]["WebsiteName"].ToString());
                        }
                        if (!Convert.IsDBNull(configTable.Rows[i]["Author"]))
                        {
                            currentConfig.Author = (configTable.Rows[i]["Author"].ToString());
                        }
                        if (!Convert.IsDBNull(configTable.Rows[i]["ContactMailAddress"]))
                        {
                            currentConfig.ContactMailAddress = (configTable.Rows[i]["ContactMailAddress"].ToString());
                        }
                        if (!Convert.IsDBNull(configTable.Rows[i]["AuthCode"]))
                        {
                            currentConfig.AuthCode = (configTable.Rows[i]["AuthCode"].ToString());
                        }
                        if (!Convert.IsDBNull(configTable.Rows[i]["MaintenanceMode"]))
                        {
                            currentConfig.MaintenanceMode = (bool)(configTable.Rows[i]["MaintenanceMode"]);
                        }
                        if (!Convert.IsDBNull(configTable.Rows[i]["CreationDate"]))
                        {
                            currentConfig.CreationDate = (DateTime)configTable.Rows[i]["CreationDate"];
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                OnError("[GETCONFIG-ERROR] " + ex.Message);
            }

            return currentConfig;
        }
    }
}
