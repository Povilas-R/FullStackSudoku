using SudokuGenerator.Data.Enum;

namespace SudokuGenerator.Data
{
	public interface ISudoku : ISudokuGrid
	{
		/// <summary>
		/// Initial sudoku grid.
		/// </summary>
		ISudokuGrid Initial { get; }
		/// <summary>
		/// Contains unique sudoku solutions up to the maximum.
		/// Max count is supplied when importing or parsing the sudoku.
		/// Generated sudoku solution count will always be 1.
		/// </summary>
		ISudokuGrid[] Solutions { get; }

		/// <summary>
		/// Sets cell digit if possible. Cannot change initial digits.
		/// </summary>
		void SetCell(int row, int column, Digit digit);
		/// <summary>
		/// Resets current user input.
		/// </summary>
		void Reset();
		/// <summary>
		/// Returns all invalid cells.
		/// Ignores source cells.
		/// </summary>
		Cell[] GetInvalidCells();
		/// <summary>
		/// Serializes the sudoku to string. Includes guess digits and seed.
		/// </summary>
		string ToString();
	}
}
