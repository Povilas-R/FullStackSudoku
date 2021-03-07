namespace SudokuGenerator.Data
{
	public interface IGrid
	{
		int RowCount { get; }
		int ColumnCount { get; }
		byte this[int row, int column] { get; }

		internal byte[,] ToMatrix()
		{
			var result = new byte[RowCount, ColumnCount];
			for (int row = 0; row < RowCount; row++)
				for (int col = 0; col < ColumnCount; col++)
					result[row, col] = this[row, col];
			return result;
		}
	}
}
