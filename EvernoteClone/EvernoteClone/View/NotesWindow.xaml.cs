using EvernoteClone.ViewModel;
using EvernoteClone.ViewModel.Helpers;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace EvernoteClone.View
{
	/// <summary>
	/// Interaction logic for NotesWindow.xaml
	/// </summary>
	public partial class NotesWindow : Window
	{
		NotesVM viewModel;
		public NotesWindow()
		{
			InitializeComponent();

			viewModel = Resources["vm"] as NotesVM;
			viewModel.SelectedNoteChanged += ViewModel_SelectedNoteChanged;

			var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
			fontFamilyComboBox.ItemsSource = fontFamilies;

			List<double> fontSizes = new List<double>()
			{
				8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 48, 72
			};

			fontSizeCombobox.ItemsSource = fontSizes;
		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			if(string.IsNullOrEmpty(App.UserId))
			{
				LoginWindow loginWindow = new LoginWindow();
				loginWindow.Show();

				viewModel.GetNotebooks();
			}
		}

		private void ViewModel_SelectedNoteChanged(object? sender, EventArgs e)
		{
			contentRichTexBox.Document.Blocks.Clear();
			if(viewModel.SelectedNote != null)
			{
				if(!string.IsNullOrEmpty(viewModel.SelectedNote.FileLocation))
				{
					FileStream fileStream = new FileStream(viewModel.SelectedNote.FileLocation, FileMode.Open);
					var contents = new TextRange(contentRichTexBox.Document.ContentStart, contentRichTexBox.Document.ContentEnd);
					contents.Load(fileStream, DataFormats.Rtf);
				}
			}
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


		private void italicButton_Click(object sender, RoutedEventArgs e)
		{
			bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;
			if (isButtonChecked)
			{
				contentRichTexBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
			}
			else
			{
				contentRichTexBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
			}
		}

		private void underlineButton_Click(object sender, RoutedEventArgs e)
		{
			bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;
			if (isButtonChecked)
			{
				contentRichTexBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
			}
			else
			{
				TextDecorationCollection textDecorations;
				(contentRichTexBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection)
					.TryRemove(TextDecorations.Underline, out textDecorations);
				contentRichTexBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
			}
		}

		private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(fontFamilyComboBox.SelectedItem!= null)
			{
				contentRichTexBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
			}
		}

		private void fontSizeCombobox_TextChanged(object sender, TextChangedEventArgs e)
		{
			contentRichTexBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeCombobox.Text);
		}

		private void contentRichTexBox_SelectionChanged(object sender, RoutedEventArgs e)
		{
			var selectedWeight = contentRichTexBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
			boldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && (selectedWeight.Equals(FontWeights.Bold));

			var selectedStyle = contentRichTexBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
			italicButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue) && (selectedStyle.Equals(FontStyles.Italic));

			var selectedDecoration = contentRichTexBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
			underlineButton.IsChecked = (selectedDecoration != DependencyProperty.UnsetValue) && (selectedDecoration.Equals(TextDecorations.Underline));

			fontFamilyComboBox.SelectedItem = contentRichTexBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
			fontSizeCombobox.Text = (contentRichTexBox.Selection.GetPropertyValue(Inline.FontSizeProperty)).ToString();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string rtfFile = Path.Combine(Environment.CurrentDirectory, $"{viewModel.SelectedNote.Id}.rtf");
			viewModel.SelectedNote.FileLocation = rtfFile;
			DatabaseHelper.Update(viewModel.SelectedNote);

			FileStream fileStream = new FileStream(rtfFile, FileMode.Create);
			var contents = new TextRange(contentRichTexBox.Document.ContentStart, contentRichTexBox.Document.ContentEnd);
			contents.Save(fileStream, DataFormats.Rtf);

		}
	}
}
