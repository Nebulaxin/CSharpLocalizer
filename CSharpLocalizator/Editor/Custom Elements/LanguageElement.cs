using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CSharpLocalizer.Editor.CustomElements
{
	public class LanguageElement : ListElement
	{
		public delegate void EditEventHandler(LanguageElement sender);
		public event EditEventHandler EditClicked;
		public delegate void ChangeEventHandler(LanguageElement sender, LanguageEventArgs e);
		public event ChangeEventHandler LanguageChanged;

		public CultureInfo Language
		{
			get => language.SelectedCulture;
			set
			{
				language.SelectedCulture = value;
				name.Content = value.EnglishName;
			}
		}

		private readonly Label index, name, edit;
		private readonly LanguageDropdown language = new LanguageDropdown();
		public LanguageElement(Grid parent) : base(parent, new List<Control> { new Label(), new Label(), new Control(), new Label()})
		{
			index = controls[0] as Label;
			name = controls[1] as Label;
			edit = controls[3] as Label;

			parent.Children.Add(language);

			Grid.SetColumn(language, 2);
			Grid.SetRow(language, position);
			Grid.SetRowSpan(language, 5);
			Panel.SetZIndex(language, 1000 - position);

			Language = CultureInfo.GetCultureInfo("en");

			language.Margin = new Thickness(8, 8, 8, 8);
			language.HorizontalAlignment = HorizontalAlignment.Center;
			language.VerticalAlignment = VerticalAlignment.Top;

			index.Content = position;
			name.Content = Language.EnglishName;
			edit.Content = "Edit";

			edit.Background = new SolidColorBrush(Color.FromRgb(0x32, 0x32, 0x32));

			language.LanguageSelected += (object o, LanguageEventArgs e) =>
			{
				Language = e.Culture;
				//LanguageChanged.
			};

			edit.MouseLeftButtonDown += (object _, MouseButtonEventArgs e) =>
				EditClicked.Invoke(this);


		}
	}

}
