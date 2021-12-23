using Ookii.Dialogs.Wpf;

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CSharpLocalizer
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class StartWindow : Window
	{
		public StartWindow()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			SavesManager.Init();

			foreach (var proj in SavesManager.GetProjects())
			{
				var pe = new ProjectElement()
				{
					ProjectName = proj.name,
					ProjectDate = DateTime.FromBinary(proj.date).ToString("g"),
					ProjectPath = proj.path
				};
				pe.MouseDown += OpenExistingProjet;
				recentProjects.Children.Add(pe);
			}
		}
		private void OpenExistingProjet(object sender, MouseButtonEventArgs e)
		{
			var pe = sender as ProjectElement;
			Editor.Editor.ProjectPath = pe.ProjectPath;
			Application.Current.MainWindow = new Editor.EditorWindow();
			Application.Current.MainWindow.Show();
			Close();
		}

		private void NewProject(object sender, MouseButtonEventArgs e)
		{
			new NewProjectWindow().ShowDialog();
		}

		private void OpenProject(object sender, MouseButtonEventArgs e)
		{
			var fd = new VistaOpenFileDialog();
			fd.DefaultExt = Editor.Editor.ProjectExtension;
			fd.Multiselect = false;
			fd.ShowDialog();
			Editor.Editor.ProjectPath = fd.FileName;
			Application.Current.MainWindow = new Editor.EditorWindow();
			Application.Current.MainWindow.Show();
			Close();
		}

	}
}


