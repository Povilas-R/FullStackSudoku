﻿using System;
using System.Collections.Generic;
using System.Linq;
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
			byte[,] cells = new byte[9, 9];

			// TODO: generate here
			Console.WriteLine("test");


			return new ReadonlySudoku(cells, seed);
		}

		public static Sudoku GenerateUnsolved(SudokuDifficulty difficulty) => GenerateUnsolved(Rand.Next(), difficulty);
		public static Sudoku GenerateUnsolved(int seed, SudokuDifficulty difficulty)
		{
			throw new NotImplementedException();
		}

		private static byte[,] GenerateSolvedRecursive(Random rand, byte[,] grid = null, int index = 0)
		{
			// Initial
			if (grid == null)
				grid = new byte[9, 9];

			// Reached end
			if (index == 81)
				return grid;

			int row = index / 9;
			int col = index % 9;
			foreach (byte value in GetPossibleValues(row, col, grid).OrderBy(x => rand.Next()))
			{
				var gridCopy = (byte[,])grid.Clone();
				gridCopy[row, col] = value;
				var resultGrid = GenerateSolvedRecursive(rand, gridCopy, index + 1);
				// Reached end
				if (resultGrid != null)
					return resultGrid;
			}
			// Return null in case of no possible values
			return null;
		}

		private static HashSet<int> GetPossibleValues(int targetRow, int targetColumn, byte[,] grid)
		{
			var values = new HashSet<int>(Enumerable.Range(1, 9));

			// Row
			for (int col = 0; col < 9; col++)
				values.Remove(grid[targetRow, col]);

			// Column
			for (int row = 0; row < 9; row++)
				values.Remove(grid[row, targetColumn]);

			// Square section
			int rowSquareCorner = targetRow / 3 * 3;
			int columnSquareCorner = targetColumn / 3 * 3;
			for (int rowOffset = 0; rowOffset < 3; rowOffset++)
				for (int colOffset = 0; colOffset < 3; colOffset++)
					values.Remove(grid[rowSquareCorner + rowOffset, columnSquareCorner + colOffset]);

			return values;
		}
	}
}
