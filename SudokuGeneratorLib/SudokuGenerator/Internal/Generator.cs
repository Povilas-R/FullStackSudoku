using System.Collections.Generic;
using System.Linq;

namespace SudokuGenerator.Internal
{
	internal static partial class Generator
	{
		private static HashSet<byte> GetPossibleValues(int targetRow, int targetColumn, byte[,] grid)
		{
			var values = new HashSet<byte>(Enumerable.Range(1, 9).Select(x => (byte)x));

			// Row
			for (int col = 0; col < 9; col++)
				values.Remove(grid[targetRow, col]);

			// Column
			for (int row = 0; row < 9; row++)
				values.Remove(grid[row, targetColumn]);

			// Square section
			int rowSquareCorner = targetRow / 3 * 3;
			int columnSquareCorner = targetColumn / 3 * 3;
			for (int rowOffset = 0; rowOffset < 3; rowOffset++)
				for (int colOffset = 0; colOffset < 3; colOffset++)
					values.Remove(grid[rowSquareCorner + rowOffset, columnSquareCorner + colOffset]);

			return values;
		}
	}
}
