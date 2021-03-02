using System;
using System.Collections.Generic;
using SudokuGenerator.Data;

namespace SudokuGenerator
{
	/// <summary>
	/// TODO: implement
	/// </summary>
	public static class SudokuSolver
	{
		public static IEnumerable<ISudokuGrid> Solve(ISudokuGrid sudokuGrid, int maxUniqueSolutions) => Solve(sudokuGrid.ToMatrix(), maxUniqueSolutions);
		internal static IEnumerable<ISudokuGrid> Solve(short[,] cells, int maxUniqueSolutions)
		{
			throw new NotImplementedException();
		}
	}
}
