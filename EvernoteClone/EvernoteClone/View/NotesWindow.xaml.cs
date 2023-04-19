﻿using System;
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
using System.Windows.Shapes;

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

        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {

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