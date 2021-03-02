namespace SudokuGenerator.Data
{
	public interface IGrid
	{
		int RowCount { get; }
		int ColumnCount { get; }
		short this[int row, int column] { get; }

		internal short[,] ToMatrix()
		{
			var result = new short[RowCount, ColumnCount];
			for (int row = 0; row < RowCount; row++)
				for (int col = 0; col < ColumnCount; col++)
					result[row, col] = this[row, col];
			return result;
		}
	}
}
