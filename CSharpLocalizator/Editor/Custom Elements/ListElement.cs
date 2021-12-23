using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CSharpLocalizer.Editor.CustomElements
{
	public class ListElement
	{
		protected List<Control> controls = new List<Control>();
		protected GridSplitter splitter = new GridSplitter();
		protected int position;

		protected ListElement(Grid parent, List<Control> ctrls)
		{
			position = parent.RowDefinitions.Count - 1;

			parent.RowDefinitions.Insert(position, new RowDefinition { MinHeight = 48, Height = new GridLength(48, GridUnitType.Pixel), MaxHeight = 48 });

			for (int i = 0; i < ctrls.Count; i++)
			{
				var ctrl = ctrls[i];

				ctrl.Foreground = new SolidColorBrush(Color.FromRgb(0xee, 0xee, 0xee));

				ctrl.Margin = new Thickness(8, 8, 8, 8);

				if (ctrl is Label)
				{
					ctrl.HorizontalContentAlignment = HorizontalAlignment.Center;
					ctrl.VerticalContentAlignment = VerticalAlignment.Center;
				}

				ctrl.HorizontalAlignment = HorizontalAlignment.Stretch;
				ctrl.VerticalAlignment = VerticalAlignment.Stretch;

				Grid.SetRow(ctrl, position);
				Grid.SetColumn(ctrl, i);

				parent.Children.Add(ctrl);
				controls.Add(ctrl);
			}

			splitter.Background = new SolidColorBrush(Color.FromRgb(0x40, 0x40, 0x40));

			splitter.Height = 4;

			Grid.SetRow(splitter, position);
			Grid.SetColumnSpan(splitter, parent.ColumnDefinitions.Count);
			Panel.SetZIndex(splitter, 0);

			splitter.HorizontalAlignment = HorizontalAlignment.Stretch;
			splitter.VerticalAlignment = VerticalAlignment.Bottom;

			splitter.IsEnabled = false;

			parent.Children.Add(splitter);
		}

		public void SetRow(int pos)
		{
			foreach (var ctrl in controls)
				Grid.SetRow(ctrl, pos);
		}
	}
}