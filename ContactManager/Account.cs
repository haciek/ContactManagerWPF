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
        public string password { get; set; }
        public string email { get; set; }
        public Account(string login, string password, string email)
        {
            this.login = login;
            this.password = password;
            this.email = email;
        }
        public bool isValid() => !(login.Equals(string.Empty) || password.Equals(string.Empty) || email.Equals(string.Empty));
        public bool Matches(string login, string password)
        {
            return (this.login != null && this.password != null)
                ? this.login.Equals(login) && this.password.Equals(password)
                : false;

        }

    }
}
