using LandmarkAI.Classes;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace LandmarkAI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Image files (*.png; *.jpg)|*.png;*.jpg;*.jpeg|All files(*.*)|*.*";
			dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			if (dialog.ShowDialog() == true)
			{
				string fileName = dialog.FileName;
				selectedImage.Source = new BitmapImage(new Uri(fileName));

				MakePredictionAsync(fileName);
			}
		}

		private async void MakePredictionAsync(string fileName)
		{
			string url = "https://westeurope.api.cognitive.microsoft.com/customvision/v3.0/Prediction/a3bcaa8f-019c-489d-8cd9-b1a640ed481b/classify/iterations/Iteration4/image";
			string predictionKey = "47563b52804d4ea39bfc3e2d113cc9a1";
			string contentType = "application/octet-stream";
			var file = File.ReadAllBytes(fileName);

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Prediction-Key", predictionKey);

				using(var content =  new ByteArrayContent(file))
				{
					content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
					var response = await client.PostAsync(url, content);

					var responseString = await response.Content.ReadAsStringAsync();

					List<Prediction> predictions = (JsonConvert.DeserializeObject<CustomVision>(responseString)).Predictions;
					predictionsListView.ItemsSource = predictions;
				}
			}
		}
	}
}
