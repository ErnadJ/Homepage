using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.GlobalDefinitions.Collections
{
    public class ListSkills
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        public string CodeSnippet { get; set; }

        public bool Active { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
