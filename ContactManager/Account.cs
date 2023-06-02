using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    public class Account
    {
        public string login { get; set; }
        private string password { get; set; }
        public Account(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
        public bool isValid() 
        {
            return !(login.Equals(string.Empty)
                || password.Equals(string.Empty));
        }
    }
}
