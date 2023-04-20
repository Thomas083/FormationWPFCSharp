using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace EvernoteClone.View
{
	/// <summary>
	/// Interaction logic for NotesWindow.xaml
	/// </summary>
	public partial class NotesWindow : Window
	{
	public NotesWindow()
		{
			InitializeComponent();
		}

		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
        }

        private async void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
			string region = "francecentral";
			string key = "748a880044b0469fb33b505aa39d5c36";

			var speechConfig = SpeechConfig.FromSubscription(key, region);
			using(var audioConfig = AudioConfig.FromDefaultSpeakerOutput())
			{
				using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
				{
					var result = await recognizer.RecognizeOnceAsync();
					contentRichTexBox.Document.Blocks.Add(new Paragraph(new Run(result.Text)));
				}
			}
        }

        private void contentRichTexBox_TextChanged(object sender, TextChangedEventArgs e)
        {
			int ammountCharacters = (new TextRange(contentRichTexBox.Document.ContentStart, contentRichTexBox.Document.ContentEnd)).Text.Length;
			statusTextblock.Text = $"Document length: {ammountCharacters} character";

		}

        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
			bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;
			if(isButtonChecked)
			{
				contentRichTexBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
			} else
			{
				contentRichTexBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
			}
        }

		private void contentRichTexBox_SelectionChanged(object sender, RoutedEventArgs e)
		{
			var selectedWeight = contentRichTexBox.Selection.GetPropertyValue(FontWeightProperty);
			boldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && (selectedWeight.Equals(FontWeights.Bold));
		}
	}
}
