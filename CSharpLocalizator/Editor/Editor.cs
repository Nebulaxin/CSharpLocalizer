using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLocalizator.Editor
{
	public class Editor
	{
		public static string ProjectExtension { get => ".cslp"; }
		public static string LanguageExtension { get => ".cslang"; }
		public static string ProjectPath { get; set; }

		public static Project Project { get; set; }
		public static List<Language> Languages { get; set; } = new List<Language>();


	}

	public class Project
	{
		public string name;
		public List<string> languages = new List<string>();
	}

	public class Language
	{
		public string language;
		public List<string> keys = new List<string>();
		public List<string> values = new List<string>();
	}
}
