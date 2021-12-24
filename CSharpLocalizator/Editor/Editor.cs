using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace CSharpLocalizer.Editor
{
	public static class Editor
	{
		public static string ProjectExtension => ".cslp";
		public static string ProjectPath { get; set; }

		public static Project Project { get; private set; }

		public static string CurrentLanguage { get; set; }

		public static void Load()
		{
			Project = JsonConvert.DeserializeObject<Project>(File.ReadAllText(ProjectPath));
			SavesManager.SaveProject(new SavedProject() { date = DateTime.UtcNow.Ticks, name = Project.name, path = ProjectPath });
		}

		public static void Save()
		{
			File.WriteAllText(ProjectPath, JsonConvert.SerializeObject(Project));
			using (var fs = new StreamWriter(new FileStream(new FileInfo(ProjectPath).DirectoryName + "\\Localizer.cs", FileMode.Create)))
				fs.Write(Generator.Generate(Project));
		}
	}

	public class Project
	{
		public string name;
		public List<Language> languages = new List<Language>();
	}
	public class Language
	{
		public string name;
		public List<string> keys = new List<string>();
		public List<string> values = new List<string>();
	}

}
