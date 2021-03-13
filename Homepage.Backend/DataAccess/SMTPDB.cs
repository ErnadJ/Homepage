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

        private SMTPConfig _currentSmtpConfig;
        private SQL _sqlManager;

        public SMTPDB()
        {
            _currentSmtpConfig = new SMTPConfig();
            _sqlManager = new SQL();
        }

        public SMTPConfig CurrentSMTPConfig
        {
            get
            {
                return _currentSmtpConfig;
            }
            set
            {
                _currentSmtpConfig = value;
            }
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
                smtpConfigTable = _sqlManager.ExecuteSelect(sqlQuery, new string[] { }, new object[] { });

                /** Tabelle wird in einer Schleife durchlaufen und ins Modell geladen **/
                if (smtpConfigTable.Rows.Count > 0)
                {
                    for(int i = 0; i < smtpConfigTable.Rows.Count; i++)
                    {
                        currentSMTPConfig.Id = Convert.ToInt64(smtpConfigTable.Rows[i]["ID"]);
                        currentSMTPConfig.MailFrom = (smtpConfigTable.Rows[i]["MailFrom"].ToString());
                        currentSMTPConfig.MailTo = (smtpConfigTable.Rows[i]["MailTo"].ToString());
                        currentSMTPConfig.Password = (smtpConfigTable.Rows[i]["Password"].ToString());
                        currentSMTPConfig.Subject = (smtpConfigTable.Rows[i]["Subject"].ToString());
                        currentSMTPConfig.SMTPServer = (smtpConfigTable.Rows[i]["SMTPServer"].ToString());
                        currentSMTPConfig.SMTPPort = Convert.ToInt32((smtpConfigTable.Rows[i]["SMTPPort"].ToString()));
                        currentSMTPConfig.CreationDate = (DateTime)smtpConfigTable.Rows[i]["CreationDate"];
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return currentSMTPConfig;
        }
    }
}
