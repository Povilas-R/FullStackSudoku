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
	}
}
