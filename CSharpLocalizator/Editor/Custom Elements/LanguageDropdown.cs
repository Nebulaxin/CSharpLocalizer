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

namespace CSharpLocalizator.Editor.CustomElements
{
	public class LanguageDropdown : Grid
	{
		public delegate void LanguageEventHandler(object sender, LanguageEventArgs e);
		public event LanguageEventHandler LanguageSelected;


		private Border selectedItemBorder = new Border();
		private Label selectedItem = new Label();
		private StackPanel elements = new StackPanel();
		private CultureInfo selectedCulture = CultureInfo.GetCultureInfo("en");

		public CultureInfo SelectedCulture
		{
			get => selectedCulture;

			set
			{
				selectedCulture = value;
				selectedItem.Content = value.EnglishName;
			}
		}

		public LanguageDropdown() : base()
		{
			Focusable = true;

			var sv = new ScrollViewer();
			sv.Content = elements;
			Children.Add(selectedItemBorder);
			Children.Add(selectedItem);
			Children.Add(sv);
			sv.Visibility = Visibility.Hidden;

			RowDefinitions.Add(new RowDefinition() { Height = new GridLength(32) });
			RowDefinitions.Add(new RowDefinition() { Height = new GridLength(160) });

			SetRow(sv, 1);

			selectedItemBorder.Background = new SolidColorBrush(Color.FromRgb(0x40, 0x40, 0x40));
			selectedItem.Foreground = new SolidColorBrush(Color.FromRgb(0xee, 0xee, 0xee));
			elements.Background = new SolidColorBrush(Color.FromRgb(0x30, 0x30, 0x30));

			selectedItemBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(0x16, 0x16, 0x16));
			selectedItemBorder.BorderThickness = new Thickness(0, 0, 0, 1);

			foreach(var lang in CultureInfo.GetCultures(CultureTypes.NeutralCultures).OrderBy(x => x.EnglishName))
			{
				var langLabel = new Label()
				{
					Foreground = new SolidColorBrush(Color.FromRgb(0xee, 0xee, 0xee)),
					Content = lang.EnglishName,
					Tag = lang.Name
				};
				langLabel.MouseEnter += (object sender, MouseEventArgs e) =>
					langLabel.Background = new SolidColorBrush(Color.FromRgb(0x40, 0x40, 0x40));
				langLabel.MouseLeave += (object sender, MouseEventArgs e) =>
					langLabel.Background = new SolidColorBrush(Colors.Transparent);
				langLabel.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) =>
				{
					selectedCulture = CultureInfo.GetCultureInfo((string)(sender as Label).Tag);
					selectedItem.Content = selectedCulture.EnglishName;
					LanguageSelected.Invoke(this, new LanguageEventArgs(selectedCulture));
					sv.Visibility = Visibility.Hidden;
				};

				elements.Children.Add(langLabel);
			}

			selectedItem.MouseLeftButtonDown += (object o, MouseButtonEventArgs e) =>
				sv.Visibility = sv.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;

			LostFocus += (object o, RoutedEventArgs e) => sv.Visibility = Visibility.Hidden;
		}
	}

	public class LanguageEventArgs
	{
		public CultureInfo Culture { get; }

		public LanguageEventArgs(CultureInfo ci) => Culture = ci;
	}
}