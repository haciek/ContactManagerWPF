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

namespace ContactManager {
   
    public partial class MainWindow : Window
    {
        LoginWindow loginWin = new LoginWindow();
        public MainWindow()
        {
            InitializeComponent();
            loginWin.ShowDialog();
            Account? user = loginWin.sessionUser;
            
            if (user == null)
                this.Close();

            //List<Contact> contacts = new List<Contact>();
            //contacts.Add(new Contact() { name = "John" } );
            
            //ContactList.ItemsSource = contacts;
            List<ListBoxItem> listItems = new List<ListBoxItem>();
            List<Contact> contacts = new List<Contact>();
            contacts.Add(new Contact() { name = "John" });
            contacts.Add(new Contact() { name = "Mark" });
            foreach (Contact contact in contacts)
            {
                listItems.Add(new ListBoxItem() { Content = contact.name });
            }

            ContactList.ItemsSource = listItems;

            //listItems.Add(new ListBoxItem() { })


        }

        private void DisplayContact(object sender, RoutedEventArgs e)
        {

        }

        private void ContactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             
        }
    }
}
