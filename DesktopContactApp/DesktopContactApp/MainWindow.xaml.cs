using DesktopContactApp.Classes;
using SQLite;
using System.Windows;

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
			using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
			{
				conn.CreateTable<Contact>();
				var contacts = conn.Table<Contact>().ToList(); 
			}
		}
	}
}
