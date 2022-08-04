using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.GlobalDefinitions.Models
{
    public class SMTPConfig
    {
        public long Id { get; set; }

        public string MailFrom { get; set; }

        public string MailTo { get; set; }

        public string Subject { get; set; }

        public string Password { get; set; }

        public string SMTPServer { get; set; }

        public int SMTPPort { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
