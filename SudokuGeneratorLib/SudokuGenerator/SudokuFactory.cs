using System;
using System.Linq;
using SudokuGenerator.Data;
using SudokuGenerator.Data.Enum;
using SudokuGenerator.Data.Internal;
using SudokuGenerator.Internal;

namespace SudokuGenerator
{
	public static class SudokuFactory
	{
		private static readonly Random Rand = new Random((int)DateTime.Now.Ticks);

		public static ISudokuGrid GenerateSolved() => GenerateSolved(Rand.Next());
		public static ISudokuGrid GenerateSolved(int seed) => GenerateSolved(GenerationAlgorithm.Backtracking, seed);
		public static ISudokuGrid GenerateSolved(GenerationAlgorithm algorithm, int seed)
		{
			return Generator.GenerateSolved(algorithm, seed);
		}

		public static ISudoku Generate(SudokuDifficulty difficulty) => Generate(Rand.Next(), difficulty);
		public static ISudoku Generate(int seed, SudokuDifficulty difficulty)
		{
			return Generator.GenerateUnsolved(seed, difficulty);
		}

		/// <param name="maxUniqueSolutions">0 to skip solution resolution.</param>
		public static ISudoku FromString(string input, int maxUniqueSolutions = 10)
		{
			if (maxUniqueSolutions < 0)
				throw new ArgumentException($"{nameof(maxUniqueSolutions)} must be at least 0.");

			var sudoku = Sudoku.FromString(input);
			if (maxUniqueSolutions > 0)
				sudoku.Solutions = SudokuSolver.Solve(sudoku, maxUniqueSolutions).ToArray();
			return sudoku;
		}
	}
}
