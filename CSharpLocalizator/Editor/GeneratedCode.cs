using System.Globalization;
using System.Collections.Generic;

namespace CSharpLocalizer
{
	public static class Localizator
	{
		private static CultureInfo language;

		private static Dictionary<CultureInfo, Dictionary<string, string>> langDictionary = new Dictionary<CultureInfo, Dictionary<string, string>>
		{

		};

		/// <summary>
		/// Current program language
		/// </summary>
		public static CultureInfo Language { get => language; set { Values = langDictionary[value]; language = value; } }

		/// <summary>
		/// Dictionary of keys and values
		/// </summary>
		public static Dictionary<string, string> Values { get; private set; }

	}
}
