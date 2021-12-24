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
"using System.Globalization;\n" +
"using System.Collections.Generic;\n" +
"\n" +
"namespace CSharpLocalizer\n" +
"{\n" +
"\tpublic static class Localizer\n" +
"\t{\n" +
"\t\tprivate static CultureInfo language;\n" +
"\t\t\n" +
"\t\tprivate static Dictionary<CultureInfo, Dictionary<string, string>> langDictionary = new Dictionary<CultureInfo, Dictionary<string, string>>\n" +
"\t\t{\n";

			for(int i = 0; i < proj.languages.Count; i++)
			{
				var lang = proj.languages[i];
				output += $"\t\t\t{{\n\t\t\t\tCultureInfo.GetCultureInfo(\"{lang.name}\"), new Dictionary<string, string>\n";
				output += $"\t\t\t\t{{\n";
				for (int j = 0; j < lang.keys.Count; j++)
				{
					output += $"\t\t\t\t\t{{\"{lang.keys[j]}\", \"{lang.values[j]}\"}}{((j < lang.keys.Count - 1) ? "," : "")}\n";
				}
				output += $"\t\t\t\t}}\n";
				output += $"\t\t\t}}{(i < lang.keys.Count - 1 ? "," : "")}\n";
			}

			output +=
"\t\t};\n" +
"\t\t\n" +
"\t\t/// <summary>\n" +
"\t\t/// Current program language\n" +
"\t\t/// </summary>\n" +
"\t\tpublic static CultureInfo Language { get => language; set { Values = langDictionary[value]; language = value; } }\n" +
"\t\t\n" +
"\t\t/// <summary>\n" +
"\t\t/// Dictionary of keys and values\n" +
"\t\t/// </summary>\n" +
"\t\tpublic static Dictionary<string, string> Values { get; private set; }\n" +
"\t}\n" +
"}\n";

			return output;
		}
	}
}
