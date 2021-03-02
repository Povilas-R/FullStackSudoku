using SudokuGenerator;

namespace SudokuGeneratorConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var sudoku = SudokuFactory.FromString(
				"*9**16*5***6*2**9****8****45**6****7****5****9****1**68****7****5**3*2***3*48**1*|1#--##-#---#-#--#----#----##--#----#----#----#----#--##----#----#--#-#---#-##--#-|", 
				0);
			var invalidCells = sudoku.GetInvalidCells();
			sudoku.Reset();
			invalidCells = sudoku.GetInvalidCells();
		}
	}
}
