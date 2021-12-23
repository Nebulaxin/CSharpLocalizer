using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CSharpLocalizer
{
	public class ProjectElement : Canvas
	{
		public string ProjectName { get => (string)name.Content; set => name.Content = value; }
		public string ProjectPath { get => (string)path.Content; set => path.Content = value; }
		public string ProjectDate { get => (string)date.Content; set => date.Content = value; }

		private readonly Label name = new Label();
		private readonly Label path = new Label();
		private readonly Label date = new Label();
		public ProjectElement()
		{
			Children.Add(name);
			Children.Add(path);
			Children.Add(date);
			MouseEnter += (object sender, MouseEventArgs e) =>
				Background = new SolidColorBrush(Color.FromArgb(0x20, 0xdd, 0xdd, 0xdd));
			MouseLeave += (object sender, MouseEventArgs e) =>
				Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
			name.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xee, 0xee, 0xee));
			path.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xdd, 0xdd, 0xdd));
			date.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xdd, 0xdd, 0xdd));
			name.FontSize = 32;
			path.FontSize = 16;
			date.FontSize = 16;
			path.Margin = new Thickness(8, 40, 0, 0);
			Height = 80;
			SetRight(date, 8);
		}
	}
}
