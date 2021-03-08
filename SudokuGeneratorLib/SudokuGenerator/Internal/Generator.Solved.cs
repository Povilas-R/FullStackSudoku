using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuGenerator.Data.Enum;
using SudokuGenerator.Data.Internal;

namespace SudokuGenerator.Internal
{
	internal static partial class Generator
	{
		public static ReadonlySudoku GenerateSolved(GenerationAlgorithm algorithm, int seed)
		{
			var rand = new Random(seed);
			byte[,] grid = algorithm switch
			{
				GenerationAlgorithm.Backtracking => GenerateSolvedBacktrack(rand),
				GenerationAlgorithm.Recursive => GenerateSolvedRecursive(rand),
				_ => throw new NotImplementedException(),
			};
			return new ReadonlySudoku(grid, seed);
		}

		private static byte[,] GenerateSolvedBacktrack(Random rand)
		{
			byte[,] grid = new byte[9, 9];
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

			return grid;
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
	}
}
