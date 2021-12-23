using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CSharpLocalizer.Editor.CustomElements
{
	public class ValueElement : ListElement
	{
		public string Key { get => key.Text; set => key.Text = value; }
		public string Value { get => value.Text.Replace("\n", "\\n"); set => this.value.Text = value; }
		public string Language { get; set; }
		public int LanguagePosition { get; set; }
		public int PairPos { get; set; }

		private readonly Label index;
		private readonly TextBox value, key;


		public ValueElement(Grid parent) : base(parent, new List<Control> { new Label(), new TextBox(), new TextBox()})
		{
			parent.RowDefinitions[position].MaxHeight = 400;

			index = controls[0] as Label;
			key = controls[1] as TextBox;
			value = controls[2] as TextBox;

			index.Content = position;

			value.Background = new SolidColorBrush(Color.FromRgb(0x20, 0x20, 0x20));
			value.BorderBrush = new SolidColorBrush(Color.FromRgb(0x40, 0x40, 0x40));
			key.Background = new SolidColorBrush(Color.FromRgb(0x20, 0x20, 0x20));
			key.BorderBrush = new SolidColorBrush(Color.FromRgb(0x40, 0x40, 0x40));

			key.AcceptsReturn = false;
			value.AcceptsReturn = true;

			splitter.IsEnabled = true;

			value.TextChanged += (object sender, TextChangedEventArgs e) =>
			{
				Editor.Project.languages[LanguagePosition].values[PairPos] = value.Text;
			};

			key.TextChanged += (object sender, TextChangedEventArgs e) =>
			{
				Editor.Project.languages[LanguagePosition].keys[PairPos] = key.Text;
			};
		}
	}
}
