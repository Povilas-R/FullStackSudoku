using SudokuGenerator.Data.Enum;

namespace SudokuGenerator.Data
{
	public interface ISudokuGrid : IGrid
	{
		/// <summary>
		/// Returns the seed if sudoku was generated.
		/// </summary>
		int? Seed { get; }
	}
}
