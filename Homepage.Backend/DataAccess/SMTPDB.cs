using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homepage.GlobalDefinitions.Models;

namespace Homepage.Backend.DataAccess
{
    public class SMTPDB
    {
        /*  
         * Klasse gibt die Daten aus der Tabelle 'dbo.SMTPConfig' zurück.
         */

        public event OnErrorEventHandler OnError;
        public delegate void OnErrorEventHandler(string message);

        private SMTPConfig _currentSmtpConfig;
        private SQL _sqlManager;

        public SMTPDB()
        {
            _currentSmtpConfig = new SMTPConfig();
            _sqlManager = new SQL();
        }

        /** Methode gibt die SMTP_Config Tabelle zurück **/
        public SMTPConfig GetSMTPConfig()
        {
            return getSmtpConfig();
        }

        /** Methode gibt die SMTP_Config Tabelle zurück **/
        private SMTPConfig getSmtpConfig()
        {
            SMTPConfig currentSMTPConfig = new SMTPConfig();

            DataTable smtpConfigTable = new DataTable();

            string sqlQuery = "";

            try
            {
                sqlQuery = "GetSMTPConfig";

                /** SQL Select Statement gibt die Tabelle "SMTP_Config" zurück **/
                smtpConfigTable = _sqlManager.ExecuteSelect(sqlQuery, 
                    new string[] { }, 
                    new object[] { });

                /** Tabelle wird in einer Schleife durchlaufen und ins Modell geladen **/
                if (smtpConfigTable.Rows.Count > 0)
                {
                    for(int i = 0; i < smtpConfigTable.Rows.Count; i++)
                    {
                        if (!Convert.IsDBNull(smtpConfigTable.Rows[i]["ID"]))
                        {
                            currentSMTPConfig.Id = Convert.ToInt64(smtpConfigTable.Rows[i]["ID"]);
                        }
                        if (!Convert.IsDBNull(smtpConfigTable.Rows[i]["MailFrom"]))
                        {
                            currentSMTPConfig.MailFrom = (smtpConfigTable.Rows[i]["MailFrom"].ToString());
                        }
                        if (!Convert.IsDBNull(smtpConfigTable.Rows[i]["MailTo"]))
                        {
                            currentSMTPConfig.MailTo = (smtpConfigTable.Rows[i]["MailTo"].ToString());
                        }
                        if (!Convert.IsDBNull(smtpConfigTable.Rows[i]["Password"]))
                        {
                            currentSMTPConfig.Password = (smtpConfigTable.Rows[i]["Password"].ToString());
                        }
                        if (!Convert.IsDBNull(smtpConfigTable.Rows[i]["Subject"]))
                        {
                            currentSMTPConfig.Subject = (smtpConfigTable.Rows[i]["Subject"].ToString());
                        }
                        if (!Convert.IsDBNull(smtpConfigTable.Rows[i]["SMTPServer"]))
                        {
                            currentSMTPConfig.SMTPServer = (smtpConfigTable.Rows[i]["SMTPServer"].ToString());
                        }
                        if (!Convert.IsDBNull(smtpConfigTable.Rows[i]["SMTPPort"]))
                        {
                            currentSMTPConfig.SMTPPort = Convert.ToInt32(smtpConfigTable.Rows[i]["SMTPPort"]);
                        }
                        if (!Convert.IsDBNull(smtpConfigTable.Rows[i]["CreationDate"]))
                        {
                            currentSMTPConfig.CreationDate = (DateTime)smtpConfigTable.Rows[i]["CreationDate"];
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                OnError("[GETSMTPCONFIG-ERROR] " + ex.Message);
            } 

            return currentSMTPConfig;
        }
    }
}
