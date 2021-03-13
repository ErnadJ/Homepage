using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.GlobalDefinitions.Models
{
    public class Config
    {
        public long Id { get; set; }

        public string WebsiteName { get; set; }

        public string Author { get; set; }

        public string ContactMailAddress { get; set; }

        public string AuthCode { get; set; }

        public bool MaintenanceMode { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
