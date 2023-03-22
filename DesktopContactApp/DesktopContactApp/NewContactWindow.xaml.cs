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
            string databaseName = "Contacts.db";
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string databasePath = System.IO.Path.Combine(folderPath, databaseName);

            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.CreateTable<Contact>();
                connection.Insert(contact);
            }

            Close();
        }
    }
}
