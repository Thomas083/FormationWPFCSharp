using DesktopContactApp.Classes;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DesktopContactApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		List<Contact> contacts;
		public MainWindow()
		{
			InitializeComponent();

			contacts = new List<Contact>();

			ReadDatabase();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			NewContactWindow newContactWindow = new NewContactWindow();
			newContactWindow.ShowDialog();

			ReadDatabase();
		}

		void ReadDatabase()
		{
			using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
			{
				conn.CreateTable<Contact>();
				contacts = conn.Table<Contact>().ToList().OrderBy(contact => contact.Name).ToList(); 
			}

			if (contacts != null)
			{
				contactsListView.ItemsSource = contacts;
			}
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox searchTextBox = sender as TextBox;

			var filteredList = contacts.Where(contact => contact.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();

			contactsListView.ItemsSource = filteredList;
		}

		private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Contact selectedContact = (Contact)contactsListView.SelectedItem;

			if (selectedContact != null)
			{
				ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(selectedContact);
				contactDetailsWindow.ShowDialog();

				ReadDatabase();
			}
        }
    }
}
