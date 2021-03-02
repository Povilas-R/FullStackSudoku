using System;
using SudokuGenerator.Data.Enum;
using SudokuGenerator.Data.Internal;

namespace SudokuGenerator.Internal
{
	/// <summary>
	/// TODO: implement
	/// </summary>
	internal static class Generator
	{
		private static readonly Random Rand = new Random((int)DateTime.Now.Ticks);

		public static ReadonlySudoku GenerateSolved() => GenerateSolved(Rand.Next());
		public static ReadonlySudoku GenerateSolved(int seed)
		{
			short[,] cells = new short[9, 9];

			// TODO: generate here

			return new ReadonlySudoku(cells, seed);
		}

		public static Sudoku GenerateUnsolved(SudokuDifficulty difficulty) => GenerateUnsolved(Rand.Next(), difficulty);
		public static Sudoku GenerateUnsolved(int seed, SudokuDifficulty difficulty)
		{
			throw new NotImplementedException();
		}
	}
}
