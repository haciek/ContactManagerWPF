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
        private string email { get; set; }
        public Account(string login, string password, string? email)
        {
            this.login = login;
            this.password = password;
            this.email = (email == null) ? string.Empty : email;
        }
        public bool isValid() 
        {
            return !(login.Equals(string.Empty)
                || password.Equals(string.Empty)
                || email.Equals(string.Empty));
        }
        public bool Matches(string login, string password)
        {
            return (this.login != null && this.password != null)
                ? this.login.Equals(login) && this.password.Equals(password)
                : false;
        }
        public bool isAvailable(List<Account>? accounts) 
        {
            if (accounts == null)
                return true;

            foreach (var account in accounts)
                if (account.login == this.login)
                    return true;

            return false;
        }
        public bool canLogIn(List<Account>? accounts)
        {
            if (!this.isValid())
                return false;
            if (accounts == null)
                return false;

            foreach (var account in accounts)
                if (account.login == this.login && account.password == this.password)
                    return true;
            return false;
        }
    }
}
