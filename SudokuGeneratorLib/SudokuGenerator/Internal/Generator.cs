using System;
using System.Collections.Generic;
using System.Linq;
using SudokuGenerator.Data.Enum;
using SudokuGenerator.Data.Internal;
using System.Text;

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
			byte[,] grid = new byte[9, 9];
			var rand = new Random(seed);
			var invalidPaths = new Dictionary<string, List<byte>>();
			var currentPath = new StringBuilder(string.Empty, 82);

			for (int index = 0; index < 81; index++)
			{
				int row = index / 9;
				int col = index % 9;
				var possibleValues = GetPossibleValues(row, col, grid);
				// Remove invalid values for current path
				if (invalidPaths.ContainsKey(currentPath.ToString()))
				{
					foreach (byte value in invalidPaths[currentPath.ToString()])
					{
						possibleValues.Remove(value);
					}
				}

				// Set a random possible value
				if (possibleValues.Count > 0)
				{				
					grid[row, col] = possibleValues.ToArray()[rand.Next(possibleValues.Count)];
					currentPath.Append(grid[row, col]);
				}
				// Backtracks and updates invalid values
				else
				{
					int previousIndex = index - 1;
					int previousRow = previousIndex / 9;
					int previousCol = previousIndex % 9;
					currentPath = currentPath.Remove(currentPath.Length - 1, 1);
					if (!invalidPaths.ContainsKey(currentPath.ToString()))
						invalidPaths[currentPath.ToString()] = new List<byte>();
					invalidPaths[currentPath.ToString()].Add(grid[previousRow, previousCol]);
					grid[previousRow, previousCol] = 0;
					index -= 2;
				}
			}
			return new ReadonlySudoku(grid, seed);
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

		private static HashSet<byte> GetPossibleValues(int targetRow, int targetColumn, byte[,] grid)
		{
			var values = new HashSet<byte>(Enumerable.Range(1, 9).Select(x => (byte)x));

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
