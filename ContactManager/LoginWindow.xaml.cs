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
using Newtonsoft.Json;

namespace ContactManager
{

    public partial class LoginWindow : Window
    {
        public Account? sessionUser { get; set; }
        private AccountDataManager AccountManager;
        public LoginWindow()
        {
            InitializeComponent();
            AccountManager = new AccountDataManager();
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
            Account account = new Account(LoginField.Text, PasswordField.Password);

            if (!AccountManager.isAvailable(account))
            {
                ShowLogin(sender, e);
                MessageBox.Show("Login Failed");
                return;
            }
            AccountManager.SetSessionAccount(account);
            this.Close();
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {
            ShowLogin(sender, e);
            Account account = new(LoginFieldSignUp.Text, PasswordFieldSignUp.Password);
            if (!account.isValid())
            {
                ShowSignUp(sender, e);
                MessageBox.Show("Error", "Invalid Field");
                return;
            }
            if (AccountManager.isAvailable(account))
            {
                MessageBox.Show("Error", "Account Taken");
                return;
            }

            AccountManager.AddAccount(account);
            MessageBox.Show("Success", "Signed in");
        }
        private void OnPasswordFieldChange(object sender, RoutedEventArgs args)
        {
            PasswordBox passwordField = (PasswordBox)sender;
            int len = passwordField.Password.Length;
            if (len < 6)
                passwordField.BorderBrush = Brushes.Red;
            else if (len < 8)
                passwordField.BorderBrush = Brushes.Yellow;
            else
                passwordField.BorderBrush = Brushes.Green;

        }
    }
}
