using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.GlobalDefinitions.Collections
{
    public class ListProjects
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Live { get; set; }

        public bool Active { get; set; }

        public List<ListProjectImages> ListProjectImages { get; set; }

        public DateTime CreationDate { get; set; }

        public ListProjects()
        {
            ListProjectImages = new List<ListProjectImages>();
        }
    }
}
