using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharpLocalizator
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			SavesManager.Init();
			var rp = SavesManager.GetProjects();
			foreach (var proj in rp)
			{
				var l = new Label();
				var l2 = new Label();
				var l3 = new Label();
				var c = new Canvas();
				c.Height = 80;
				c.Children.Add(l);
				c.Children.Add(l2);
				c.Children.Add(l3);
				c.Tag = proj.path;
				c.MouseDown += OpenExistingProjet;
				c.MouseEnter += (object sender, MouseEventArgs e) =>
					c.Background = new SolidColorBrush(Color.FromArgb(0x20, 0xdd, 0xdd, 0xdd));
				c.MouseLeave += (object sender, MouseEventArgs e) =>
					c.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
				l.Content = proj.name;
				l2.Content = proj.path;
				l3.Content = DateTime.FromBinary(proj.date).ToString("g");
				l.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xee, 0xee, 0xee));
				l2.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xdd, 0xdd, 0xdd));
				l3.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xdd, 0xdd, 0xdd));
				l.FontSize = 32;
				l2.FontSize = 16;
				l3.FontSize = 16;
				l2.Margin = new Thickness(8, 40, 0, 0);
				Canvas.SetRight(l3, 8);
				recentProjectss.Children.Add(c);
			}
		}

		private void OpenExistingProjet(object sender, MouseButtonEventArgs e)
		{
			Editor.Editor.ProjectPath = (string)(sender as Canvas).Tag;
			Application.Current.MainWindow = new EditorWindow();
			Application.Current.MainWindow.Show();
			Application.Current.Windows[0].Close();
			Close();
		}

		private void NewProject(object sender, MouseButtonEventArgs e)
		{
			var np = new NewProjectWindow();
			np.ShowDialog();
		}

		private void OpenProject(object sender, MouseButtonEventArgs e)
		{
			var fd = new VistaOpenFileDialog();
			fd.DefaultExt = Editor.Editor.ProjectExtension;
			fd.Multiselect = false;
			fd.ShowDialog();
			Editor.Editor.ProjectPath = fd.FileName;
			Application.Current.MainWindow = new EditorWindow();
			Application.Current.MainWindow.Show();
			Application.Current.Windows[0].Close();
			Close();
		}

		private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (e.NewSize.Width > scrollView.MaxWidth + 456)
				scrollView.Margin = new Thickness(14, 146, (e.NewSize.Width - (scrollView.MaxWidth + 14)) / 1, 48);
			else scrollView.Margin = new Thickness(14, 146, 442, 48);

		}
	}
}


