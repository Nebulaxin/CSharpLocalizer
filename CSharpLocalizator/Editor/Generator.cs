using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLocalizer.Editor
{
	public static class Generator
	{
		public static string Generate(Project proj)
		{
			string output = "";

			output +=
@"using System.Globalization;
using System.Collections.Generic;

namespace CSharpLocalizer
{
	public static class Localizer
	{
		private static CultureInfo language;

		private static Dictionary<CultureInfo, Dictionary<string, string>> langDictionary = new Dictionary<CultureInfo, Dictionary<string, string>>
		{
";

			for(int i = 0; i < proj.languages.Count; i++)
			{
				var lang = proj.languages[i];
				output += $"			{{\n				\"CultureInfo.GetCultureInfo({lang.name})\", new Dictionary<string, string>\n";
				output += $"				{{\n";
				for (int j = 0; j < lang.keys.Count; j++)
				{
					output += $"					{{\"{lang.keys[j]}\", \"{lang.values[j]}\"}}{((j < lang.keys.Count - 1) ? "," : "")}\n";
				}
				output += $"				}}\n";
				output += $"			}}{(i < lang.keys.Count - 1 ? "," : "")}\n";
			}

			output +=
@"		};

		/// <summary>
		/// Current program language
		/// </summary>
		public static CultureInfo Language { get => language; set { Values = langDictionary[value]; language = value; } }

		/// <summary>
		/// Dictionary of keys and values
		/// </summary>
		public static Dictionary<string, string> Values { get; private set; }

	}
}";

			return output;
		}
	}
}
