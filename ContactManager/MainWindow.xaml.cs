using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactManager {

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
        
    public partial class MainWindow : Window
    {
        private string accountsDirPath;
        private string accountsFilePath;
        public MainWindow()
        {
            InitializeComponent();
            this.accountsDirPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            this.accountsFilePath = System.IO.Path.Combine(this.accountsDirPath, "file.json");
        }

        private void ShowSignUp(object sender, RoutedEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            SingUpPanel.Visibility = Visibility.Visible;
            LoginFieldSignUp.Text = string.Empty;
            PasswordFieldSignUp.Password = string.Empty;
            EmailFieldSignUp.Text = string.Empty;
        }
        private void ShowLogin(object sender, RoutedEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Visible;
            SingUpPanel.Visibility = Visibility.Collapsed;
            LoginField.Text = string.Empty;
            PasswordField.Password = string.Empty;
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            using FileStream file = File.OpenRead(this.accountsFilePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Account? user = JsonSerializer.Deserialize<Account>(file, options);
            if (user == null)
                return;
            //if (users != null)
            //    return;
            //bool userExists = false;
            //foreach (var user in users)
            //{
            //    if (user == null) continue;
            //
            //    if (user.Matches(LoginField.Text, PasswordField.Password)) 
            //    {
            //        userExists = true;
            //        break;
            //    }                 
            //}
            //if (!userExists)
            if (!user.Matches(LoginField.Text.ToString(), PasswordField.Password.ToString()))
            {   
                ShowLogin(sender, e);
                MessageBox.Show("Login Failed");
                return;
            }

            MessageBox.Show("Login Succesfull");
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {   
            ShowLogin(sender, e);
            Account user = new(LoginFieldSignUp.Text, PasswordFieldSignUp.Password, EmailFieldSignUp.Text);
            if (!user.isValid()) {
                ShowSignUp(sender, e);
                MessageBox.Show("Error", "Invalid Field");
                return;
            }
            MessageBox.Show("Signed in");
            string jsonString = JsonSerializer.Serialize<Account>(user);
            File.WriteAllText(this.accountsFilePath, jsonString);

        }
    }
}
