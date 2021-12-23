using System.Windows.Controls;

namespace CSharpLocalizer.Editor.CustomElements
{
	public class CloneableGrid : Grid
	{
		public Grid CloneGrid()
		{
			return MemberwiseClone() as Grid;
		}
	}

	public static class GridExtension
	{
		public static Grid Clone(this Grid grid)
		{
			return (grid as CloneableGrid).CloneGrid();
		}
	}
}
