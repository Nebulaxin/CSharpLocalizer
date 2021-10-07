using Newtonsoft.Json;

using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

using Ookii.Dialogs.Wpf;

namespace CSharpLocalizator
{
	/// <summary>
	/// Логика взаимодействия для NewProjectWindow.xaml
	/// </summary>
	public partial class NewProjectWindow : Window
	{
		public NewProjectWindow()
		{
			InitializeComponent();
		}

		private void OpenEditor(object sender, MouseButtonEventArgs e)
		{
			
			if (!Directory.Exists(projPath.Text))
				return;
			try
			{
				using (var sw = new StreamWriter(File.Create(projPath.Text + "/" + projName.Text + Editor.Editor.ProjectExtension)))
				{
					var project = new Editor.Project { name = projName.Text };
					Editor.Editor.Project = project;
					sw.Write(JsonConvert.SerializeObject(project));
				}
			}
			catch (Exception)
			{
				return;
			}
			SavesManager.SaveNewProject(new SavedProject { name = projName.Text, path = projPath.Text, date = DateTime.Now.ToBinary() });
			Editor.Editor.ProjectPath = projPath.Text + projName.Text + Editor.Editor.ProjectExtension;
			Application.Current.MainWindow = new EditorWindow();
			Application.Current.MainWindow.Show();
			Application.Current.Windows[0].Close();
			Close();
		}

		private void Chancel(object sender, MouseButtonEventArgs e)
		{
			Close();
		}

		private void SelectFolder(object sender, MouseButtonEventArgs e)
		{
			var fd = new VistaFolderBrowserDialog();
			fd.Description = "Select Folder";
			fd.ShowDialog();
			projPath.Text = fd.SelectedPath;
		}
	}
}
