//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Shapes;

//namespace CSharpLocalizator.Editor.CustomElements
//{
//	public class KeyElement : ListElement
//	{
//		public string Key { get => key.Text; set => key.Text = value; }
//		public int Id { get; set; }

//		private readonly Label index;
//		private readonly TextBox key;
//		public KeyElement(Grid parent) : base(parent, new List<Control> { new Label(), new TextBox()})
//		{
//			index = controls[0] as Label;
//			key = controls[1] as TextBox;

//			index.Content = position;

//			key.Background = new SolidColorBrush(Color.FromRgb(0x20, 0x20, 0x20));
//			key.BorderBrush = new SolidColorBrush(Color.FromRgb(0x40, 0x40, 0x40));
//			key.AcceptsReturn = false;

//			key.TextChanged += (object sender, TextChangedEventArgs e) =>
//			{
//				Editor.EditKey(key.Text, Id);
//			};
//		}
//	}
//}
