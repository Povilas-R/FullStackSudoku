namespace SudokuGenerator.Data.Internal
{
	internal class Grid : IGrid
	{
		protected short[,] _cells { get; }

		public Grid(IGrid grid) : this(grid.ToMatrix()) { }
		public Grid(short[,] cells) : this(cells, 0, cells.GetLength(0), 0, cells.GetLength(1)) { }
		public Grid(short[,] cells, int rowFrom, int rowTo, int columnFrom, int columnTo)
		{
			// Get counts
			RowCount = cells.GetLength(0);
			ColumnCount = cells.GetLength(1);

			// Copy cells
			_cells = new short[rowTo - rowFrom + 1, columnTo - columnFrom + 1];
			for (int row = rowFrom; row < rowTo; row++)
				for (int col = columnFrom; col < columnTo; col++)
					_cells[row - rowFrom, col - columnFrom] = cells[row, col];
		}

		public int RowCount { get; }
		public int ColumnCount { get; }
		public short this[int row, int column] => _cells[row, column];
	}
}
