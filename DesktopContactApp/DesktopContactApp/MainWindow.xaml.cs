using DesktopContactApp.Classes;
using SQLite;
using System.Collections.Generic;
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
		public MainWindow()
		{
			InitializeComponent();

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
			List<Contact> contacts;
			using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
			{
				conn.CreateTable<Contact>();
				contacts = conn.Table<Contact>().ToList(); 
			}

			if (contacts != null)
			{
				contactsListView.ItemsSource = contacts;
			}
		}
	}
}
