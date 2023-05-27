﻿using System;
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
        private string accountsDirPath;
        private string accountsFilePath;
        private List<Account>? accounts;
        public Account? sessionUser {get; set;}

        public LoginWindow()
        {
            InitializeComponent();
            this.accountsDirPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            // TODO: check if file exists
            this.accountsFilePath = System.IO.Path.Combine(this.accountsDirPath, "file.json");

            string jsonString = File.ReadAllText(this.accountsFilePath);
            this.accounts = JsonConvert.DeserializeObject<List<Account>>(jsonString);
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
            Account acc = new Account(LoginField.Text, PasswordField.Password, null);

            if (!acc.canLogIn(this.accounts))
            {
                ShowLogin(sender, e);
                MessageBox.Show("Login Failed");
                return;
            }

            this.Close();
            sessionUser = acc;
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {
            ShowLogin(sender, e);
            Account user = new(LoginFieldSignUp.Text, PasswordFieldSignUp.Password, EmailFieldSignUp.Text);
            if (!user.isValid())
            {
                ShowSignUp(sender, e);
                MessageBox.Show("Error", "Invalid Field");
                return;
            }
            if (!user.isAvailable(this.accounts))
            {
                MessageBox.Show("Error", "Account Taken");
                return;
            }

            if (this.accounts != null)
            {
                this.accounts.Add(user);
                string jsonString = JsonConvert.SerializeObject(this.accounts);
                MessageBox.Show("Signed in");
                File.WriteAllText(this.accountsFilePath, jsonString);
                return;
            }
            MessageBox.Show("Error", "Failed To Sign In");
        }
    }
}
