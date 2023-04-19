using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.Windows;
using System.Windows.Controls;
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
			contentRichTexBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
        }
    }
}
