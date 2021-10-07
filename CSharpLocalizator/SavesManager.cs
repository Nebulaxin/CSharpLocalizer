using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace CSharpLocalizator
{
	public class SavesManager
	{
		public static Save save = new Save();

		public static void Init()
		{
			if(File.Exists(Environment.ExpandEnvironmentVariables("%localappdata%") + "/CSharpLocalizator/recentprojects"))
			save = JsonConvert.DeserializeObject<Save>(File.ReadAllText(Environment.ExpandEnvironmentVariables("%localappdata%") + "/CSharpLocalizator/recentprojects"));
		}

		public static void SaveProject(SavedProject proj)
		{
			save.recentProjects[save.recentProjects.FindIndex(x => x == proj)] = proj;
			Save();
		}

		public static void SaveNewProject(SavedProject proj)
		{
			save.recentProjects.Add(proj);
			Save();
		}

		private static void Save()
		{
			if (!Directory.Exists(Environment.ExpandEnvironmentVariables("%localappdata%") + "/CSharpLocalizator/"))
				Directory.CreateDirectory(Environment.ExpandEnvironmentVariables("%localappdata%") + "/CSharpLocalizator/");
			using (var sw = new StreamWriter(File.Create(Environment.ExpandEnvironmentVariables("%localappdata%") + "/CSharpLocalizator/recentprojects")))
			{
				sw.Write(JsonConvert.SerializeObject(save));
			}
		}

		public static List<SavedProject> GetProjects()
		{
			return save.recentProjects;
		}
	}

	public class Save
	{
		public List<SavedProject> recentProjects = new List<SavedProject>();
	}

	public class SavedProject
	{
		public string path;
		public string name;
		public long date;
	}
}
