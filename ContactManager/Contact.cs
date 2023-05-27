using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    public class Contact
    {
        public string name { get; set; }
        public string? label { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? adress { get; set; }

        public Contact() { }

    }
}
