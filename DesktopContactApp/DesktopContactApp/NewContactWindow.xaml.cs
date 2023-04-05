using DesktopContactApp.Classes;
using SQLite;
using System;
using System.Windows;

namespace DesktopContactApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public NewContactWindow()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            WindowStartupLocation= WindowStartupLocation.CenterOwner;
        }

		private void saveButton_Click(object sender, RoutedEventArgs e)
		{
            // Save contact
            Contact contact = new Contact()
            {
                Name = nameTextBox.Text,
                Email= emailTextBox.Text,
                Phone= phoneTextBox.Text,
            };
            
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                connection.Insert(contact);
            }

            Close();
        }
    }
}
