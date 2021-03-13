using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EASendMail;
using Homepage.GlobalDefinitions.Models;

namespace Homepage.Backend.BusinessLogic
{
    public class MailManager
    {
        /*  
         * Klasse versendet über einen Mail-Server, Emails.
         */

        private SMTPConfig _currentSMTPConfig;
        private Contact _currentContact;
        
        public MailManager()
        {
            _currentSMTPConfig = new SMTPConfig();
            _currentContact = new Contact();
        }

        public Contact CurrentContact
        {
            get
            {
                return _currentContact;
            }
            set
            {
                _currentContact = value;
            }
        }

        public SMTPConfig CurrentSMTPConfig
        {
            get
            {
                return _currentSMTPConfig;
            }
            set
            {
                _currentSMTPConfig = value;
            }
        }

        public bool SendMessageMail()
        {
            return sendMessageMail();
        }

        /** Methode um über das Kontakt-Formular eine E-Mail an mich persönlich zu versenden **/
        private bool sendMessageMail()
        {
            bool result = false;

            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");

                oMail.From = this.CurrentSMTPConfig.MailFrom;

                oMail.To = this.CurrentSMTPConfig.MailTo;

                oMail.Subject = this.CurrentSMTPConfig.Subject;

                oMail.TextBody = "E-Mail von : " + this.CurrentContact.MailAddress + " " + " Nachricht : " +  this.CurrentContact.Message;

                SmtpServer oServer = new SmtpServer(this.CurrentSMTPConfig.SMTPServer);

                oServer.User = this.CurrentSMTPConfig.MailFrom;

                oServer.Password = this.CurrentSMTPConfig.Password;

                oServer.Port = this.CurrentSMTPConfig.SMTPPort;

                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                SmtpClient oSmtp = new SmtpClient();

                oSmtp.SendMail(oServer, oMail);

                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
