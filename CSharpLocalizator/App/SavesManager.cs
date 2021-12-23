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
		private readonly static string savePath = Environment.ExpandEnvironmentVariables("%localappdata%") + "/CSharpLocalizator/recentprojects";
		public static void Init()
		{
			if(File.Exists(Environment.ExpandEnvironmentVariables("%localappdata%") + "/CSharpLocalizator/recentprojects"))
			save = JsonConvert.DeserializeObject<Save>(File.ReadAllText(Environment.ExpandEnvironmentVariables("%localappdata%") + "/CSharpLocalizator/recentprojects"));
		}

		public static void SaveProject(SavedProject proj)
		{
			save.recentProjects.Remove(save.recentProjects.Find(x => x.path == proj.path));
			save.recentProjects.Add(proj);
			Save();
		}

		private static void Save()
		{
			//if (!Directory.Exists(Environment.ExpandEnvironmentVariables("%localappdata%") + "/CSharpLocalizator/"))
			//	Directory.CreateDirectory(Environment.ExpandEnvironmentVariables("%localappdata%") + "/CSharpLocalizator/");
			using (var sw = new StreamWriter(File.Create(savePath)))
				sw.Write(JsonConvert.SerializeObject(save));
		}

		public static List<SavedProject> GetProjects()
		{
			return new List<SavedProject>(save.recentProjects.OrderBy(x => x.date).Reverse());
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
