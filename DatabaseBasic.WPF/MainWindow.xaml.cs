using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using DatabaseBasic.DataFramework;
using DatabaseBasic.DataFramework.Model;

namespace DatabaseBasic.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ContactDAL contactsDAL = new ContactDAL();
        Contact SelectedContact;


        public MainWindow()
        {
            InitializeComponent();

            RefreshContactsTable();

            SelectedContact = new Contact();
            SexComboBox.ItemsSource = new[]{
               "Male","Female"
            };

        }

        private void RefreshContactsTable()
        {
            MainTable.ItemsSource = contactsDAL.GetContacts();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshContactsTable();
        }

        private void MainTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedContact = (Contact)((ListView)sender).SelectedItem;

            if (SelectedContact != null)
            {

                NameTextBox.Text = SelectedContact.Name;
                SurnameTextBox.Text = SelectedContact.Surname;
                PhoneNumberTextBox.Text = SelectedContact.PhoneNumber;
                SexComboBox.SelectedIndex = (int)SelectedContact.Sex -1;
            }
            else
            {
                ClearContactData();
            }
        }

        private void NewContact_Button_Click(object sender, RoutedEventArgs e)
        {
            ClearContactData();
        }

        private void CreateUpdate_Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedContact.Name = NameTextBox.Text;
            SelectedContact.Surname = SurnameTextBox.Text;
            SelectedContact.PhoneNumber = PhoneNumberTextBox.Text;
            SelectedContact.Sex = (SexEnum)SexComboBox.SelectedIndex +1 ;

            if (SelectedContact.Id == 0)
            {
                var addedContact = contactsDAL.InsertContact(SelectedContact);
                MainTable.ItemsSource = MainTable.ItemsSource.Cast<Contact>().Concat(new List<Contact> { addedContact });
            }
            else
            {
                var updatedElement = contactsDAL.UpdateContact(SelectedContact);
                var actualMainTableItemsSource = MainTable.ItemsSource.Cast<Contact>().ToList();
                var elementInTable = actualMainTableItemsSource.FirstOrDefault(x => x.Id == updatedElement.Id);
                actualMainTableItemsSource.Remove(elementInTable);
                actualMainTableItemsSource.Add(updatedElement);
                MainTable.ItemsSource = actualMainTableItemsSource;
            }

            ClearContactData();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedContact.Id != 0)
            {
                if(MessageBox.Show($"Czy na pewno chcesz usunąć element {SelectedContact.Name} | {SelectedContact.Surname} ?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    if (contactsDAL.DeleteContact(SelectedContact.Id))
                    {
                        var items = MainTable.ItemsSource.Cast<Contact>().ToList();
                        var elementToDelete = items.First(x => x.Id == SelectedContact.Id);
                        items.Remove(elementToDelete);
                        MainTable.ItemsSource = items;

                        ClearContactData();
                        MessageBox.Show("Kontakt usunięto");
                    }
                    else
                    {
                        MessageBox.Show("Coś poszło nie tak", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Wybierz element z listy do usunięcia");
            }
        }

        private void ClearContactData()
        {
            SelectedContact = new Contact();
            NameTextBox.Text = string.Empty;
            SurnameTextBox.Text = string.Empty;
            PhoneNumberTextBox.Text = string.Empty;
            SexComboBox.SelectedIndex = 0;
        }
    }
}
