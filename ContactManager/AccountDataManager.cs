using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    internal class AccountDataManager
    {
        private string MainDataDir;
        private string AccountDataPath;
        private string AccountDataJson;
        private List<Account>? AccountList = new List<Account>();
        private Account? SessionAccount { get; set; }
        public AccountDataManager() {
            string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            MainDataDir = Path.Combine(AppData, "ContactManager");
            if (!Directory.Exists(MainDataDir))
                Directory.CreateDirectory(MainDataDir);

            AccountDataPath = Path.Combine(MainDataDir, "Accounts.json");
            if (!File.Exists(AccountDataPath))
                File.Create(AccountDataPath).Close();

            AccountDataJson = File.ReadAllText(AccountDataPath);
            
            AccountList = JsonConvert.DeserializeObject<List<Account>>(AccountDataJson);

        }
        public void AddAccount(Account account)
        {
            if (AccountList == null)
                AccountList = new List<Account>();
            AccountList.Add(account);

            string AccountDir = Path.Combine(MainDataDir, account.login);
            Directory.CreateDirectory(AccountDir);

            string AccountContactPath = Path.Combine(AccountDir, "Contacts.json");
            File.Create(AccountContactPath).Close();
            SaveAccounts();
        }

        public bool isAvailable(Account account)
        {
            return AccountList == null || AccountList.Contains(account);
        }

        public void SetSessionAccount(Account account)
        {
            SessionAccount = account;
        }
        private void SaveAccounts()
        {
            AccountDataJson = JsonConvert.SerializeObject(AccountList);
            File.WriteAllText(AccountDataPath, AccountDataJson);
        }
    }
}
