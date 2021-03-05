using SudokuGenerator;

namespace SudokuGeneratorConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var sudoku = SudokuFactory.GenerateSolved(0);
		}
	}
}
