using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuGenerator;
using SudokuGenerator.Data;

namespace SudokuGeneratorTests
{
	[TestClass]
	public class SolvedGeneratorTests
	{
		private readonly Dictionary<int, ISudokuGrid> GeneratedSolutions = new Dictionary<int, ISudokuGrid>();
		/// <summary>
		/// Gets generated sudoku from cache.
		/// Generates and caches if missing.
		/// </summary>
		private ISudokuGrid GetGeneratedSudoku(int seed)
		{
			if (!GeneratedSolutions.TryGetValue(seed, out ISudokuGrid grid))
				GeneratedSolutions[seed] = grid = SudokuFactory.GenerateSolved(seed);
			return grid;
		}

		[TestMethod]
		public void GenerateSolved_CheckSpecificSeed()
		{
			// TODO: check result against specific seed once solved solution generator is fully implemented
		}

		[TestMethod]
		public void GenerateSolved_CheckSameSeed()
		{
			var sudoku1 = SudokuFactory.GenerateSolved(0);
			var sudoku2 = SudokuFactory.GenerateSolved(0);
			for (int index = 0; index < 81; index++)
			{
				int row = index / 9;
				int col = index % 9;
				byte value1 = sudoku1[row, col];
				byte value2 = sudoku2[row, col];
				Assert.AreEqual(value1, value2, "Same seed generated different sudokus.");
			}
		}

		[TestMethod]
		public void GenerateSolved_CheckDimensions()
		{
			// Check 3 seeds
			foreach (int seed in Enumerable.Range(1, 3))
			{
				var grid = GetGeneratedSudoku(seed);
				Assert.IsTrue(grid.RowCount == 9 && grid.ColumnCount == 9, "Invalid sudoku dimensions.");
			}
		}

		[TestMethod]
		public void GenerateSolved_CheckIsSolved()
		{
			// Check 3 seeds
			foreach (int seed in Enumerable.Range(1, 3))
			{
				var grid = GetGeneratedSudoku(seed);
				bool isValid = true;
				for (int index = 0; index < 81 && isValid; index++)
				{
					int row = index / 9;
					int col = index % 9;
					byte value = grid[row, col];
					Assert.IsTrue(value >= 1 && value <= 9, "Sudoku is not solved.");
				}
			}
		}

		[TestMethod]
		public void GenerateSolved_CheckRulesForSolutions()
		{
			// Check 3 seeds
			foreach (int seed in Enumerable.Range(1, 3))
			{
				var grid = GetGeneratedSudoku(seed);
				bool isValid = true;
				// Row
				for (int row = 0; row < 9 && isValid; row++)
					assert(() => new HashSet<byte>(Enumerable.Range(0, 9).Select(col => grid[row, col])).Count == 9);
				// Column
				for (int col = 0; col < 9 && isValid; col++)
					assert(() => new HashSet<byte>(Enumerable.Range(0, 9).Select(row => grid[row, col])).Count == 9);
				// Square
				for (int squareRow = 0; squareRow <= 6; squareRow += 3)
					for (int squareCol = 0; squareCol <= 6; squareCol += 3)
						assert(() => new HashSet<byte>(Enumerable.Range(0, 9).Select(index => grid[squareRow + index / 3, squareCol + index % 3])).Count == 9);
			}

			static void assert(Func<bool> isValidDelegate)
			{
				Assert.IsTrue(isValidDelegate(), "Sudoku is invalid.");
			}
		}
	}
}
