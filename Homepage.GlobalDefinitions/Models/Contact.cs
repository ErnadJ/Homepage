using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.GlobalDefinitions.Models
{
    public class Contact
    {
        [Required]
        [EmailAddress]
        public string MailAddress { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
