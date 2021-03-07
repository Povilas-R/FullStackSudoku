using System;
using System.Text;

namespace SudokuGenerator.Data.Internal
{
	internal class ReadonlySudoku : Grid, ISudokuGrid
	{
		public ReadonlySudoku(byte[,] cells, int seed) : base(cells)
		{
			Seed = seed;
		}
		public ReadonlySudoku(ISudokuGrid sudokuGrid) : base(sudokuGrid) 
		{
			Seed = sudokuGrid.Seed;
		}

		public int? Seed { get; }

		public override string ToString()
		{
			var sb = new StringBuilder();
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					if (col > 0 && col < 9 && col % 3 == 0)
						sb.Append(' ');
					sb.Append(this[row, col]);
				}
				sb.Append(Environment.NewLine);
			}
			return sb.ToString();
		}
	}
}
