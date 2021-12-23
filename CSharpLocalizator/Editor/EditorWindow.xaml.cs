using CSharpLocalizator.Editor.CustomElements;

using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.Globalization;

namespace CSharpLocalizator.Editor
{
	/// <summary>
	/// Логика взаимодействия для EditorWindow.xaml
	/// </summary>
	public partial class EditorWindow : Window
	{

		private Dictionary<string, LanguagePanel> langPanels = new Dictionary<string, LanguagePanel>();

		private DateTime lastSaveDate;

		public EditorWindow()
		{
			InitializeComponent();
			Editor.Load();
			lastSaveDate = DateTime.UtcNow;
			KeyDown += Save;
			Title = $"C# Localizator - Editor - {Editor.Project.name}";
			foreach(var lang in Editor.Project.languages)
			{
				var l = new LanguageElement(languagesGrid) { Language = CultureInfo.GetCultureInfo(lang.name) };
				l.EditClicked += EditLanguage;
				var p = new LanguagePanel
				{
					Visibility = Visibility.Hidden,
					Language = lang.name,
					Main = mainPanel,
					LanguagePosition = Editor.Project.languages.IndexOf(lang)
				};

				main.Children.Add(p);
				langPanels.Add(lang.name, p);
				for (int i = 0; i < lang.keys.Count; i++)
				{
					p.AddKey(lang.keys[i], lang.values[i], i);
				}
			}
		}

		private void Save(object o, KeyEventArgs e)
		{
			if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.S) && (DateTime.UtcNow - lastSaveDate).TotalSeconds > 1)
			{
				lastSaveDate = DateTime.UtcNow;
				Editor.Save();
				Debug.WriteLine("saved");
			}
		}

		private void AddLanguage(object sender, MouseButtonEventArgs e)
		{
			var lang = new LanguageElement(languagesGrid);
			lang.EditClicked += EditLanguage;
		}

		private void EditLanguage(LanguageElement sender)
		{
			Editor.CurrentLanguage = sender.Language.Name;
			if(!langPanels.ContainsKey(Editor.CurrentLanguage))
			{
				Editor.Project.languages.Add(new Language() { name = Editor.CurrentLanguage });
				var p = new LanguagePanel { Visibility = Visibility.Hidden, Language = Editor.CurrentLanguage, 
					Main = mainPanel, LanguagePosition = Editor.Project.languages.Count - 1 };

				main.Children.Add(p);
				langPanels.Add(Editor.CurrentLanguage, p);
			}
			mainPanel.Visibility = Visibility.Hidden;
			langPanels[Editor.CurrentLanguage].Visibility = Visibility.Visible;
		}
	}
}
