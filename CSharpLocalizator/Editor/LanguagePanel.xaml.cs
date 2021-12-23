using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CSharpLocalizer.Editor.CustomElements;

namespace CSharpLocalizer.Editor
{
	/// <summary>
	/// Логика взаимодействия для LanguagePanel.xaml
	/// </summary>
	public partial class LanguagePanel : UserControl
	{
		private string language;
		public Grid Main { get; set; } 
		public int LanguagePosition { get; set; } 
		new public string Language
		{
			get => language;
			set
			{
				languageName.Content = CultureInfo.GetCultureInfo(value).EnglishName;
				language = value;
			}
		}
		public LanguagePanel()
		{
			InitializeComponent();
		}

		private void ToMain(object sender, MouseButtonEventArgs e)
		{
			Main.Visibility = Visibility.Visible;
			Visibility = Visibility.Hidden;
		}

		private void AddKey(object sender, MouseButtonEventArgs e)
		{
			new ValueElement(languageGrid) 
			{ LanguagePosition = LanguagePosition, PairPos = Editor.Project.languages[LanguagePosition].keys.Count };
			Editor.Project.languages[LanguagePosition].keys.Add("");
			Editor.Project.languages[LanguagePosition].values.Add("");
		}
		public void AddKey(string key, string value, int pos)
		{
			new ValueElement(languageGrid) { LanguagePosition = LanguagePosition, PairPos = pos, Key = key, Value = value };
		}
	}
}
