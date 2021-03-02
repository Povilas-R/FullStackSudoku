using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuGenerator.Data.Enum;

namespace SudokuGenerator.Data.Internal
{
	internal class Sudoku : ISudoku
	{
		private readonly short[,] Cells = new short[9, 9];

		public int? Seed { get; set; }
		public int RowCount => 9;
		public int ColumnCount => 9;
		public short this[int row, int column]
		{
			get => Cells[row, column];
			set => Cells[row, column] = value;
		}

		public ISudokuGrid Initial { get; set; }
		public ISudokuGrid[] Solutions { get; set; }

		public void SetCell(int row, int column, Digit digit)
		{
			if (digit < Digit.None || digit > Digit.Nine)
				throw new ArgumentException(nameof(digit)); // TODO: better exception throwing
			else if (Initial[row, column] != (short)Digit.None)
				throw new ArgumentException($"{nameof(row)}, {nameof(column)}"); // TODO: better exception throwing

			Cells[row, column] = (short)digit;
		}
		public void Reset()
		{
			for (int row = 0; row < 9; row++)
				for (int col = 0; col < 9; col++)
					this[row, col] = Initial[row, col];
		}
		public Cell[] GetInvalidCells()
		{
			var invalidCells = new List<Cell>();
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					// Skip initial cells
					if (Initial[row, col] != (short)Digit.None)
						continue;

					short value = this[row, col];
					// Mark as invalid if the value is not available
					if (value != (short)Digit.None && !getAvailableValues(row, col).Contains(value))
						invalidCells.Add(new Cell(row, col, (Digit)value));
				}
			}
			return invalidCells.ToArray();

			HashSet<short> getAvailableValues(int cellRow, int cellColumn)
			{
				var values = new HashSet<short>(Enumerable.Range(1, 9).Select(x => (short)x));
				for (int row = 0; row < 9; row++)
					for (int col = 0; col < 9; col++)
						if (row != cellRow && col != cellColumn)
							values.Remove(this[row, col]);
				return values;
			}
		}

		#region PARSING

		public override string ToString()
		{
			var initialBuilder = new StringBuilder();
			var guessBuilder = new StringBuilder();
			for (int row = 0; row < 9; row++)
			{
				for (int col = 0; col < 9; col++)
				{
					short initialDigit = Initial[row, col];
					short guessDigit = this[row, col];
					initialBuilder.Append(initialDigit == 0 ? "*" : initialDigit);
					guessBuilder.Append(initialDigit > 0 ? "#" : guessDigit == 0 ? "-" : guessDigit);
				}
			}
			return $"{initialBuilder}|{guessBuilder}|{Seed}";
		}

		public static Sudoku FromString(string input)
		{
			var parts = input?.Split('|');
			int seed = 0;
			// Validate input
			if (parts == null
				|| parts.Length != 3 
				|| parts[0].Length != 81 
				|| !string.IsNullOrEmpty(parts[1]) && parts[1].Length != 81 
				|| !string.IsNullOrEmpty(parts[2]) && !int.TryParse(parts[2], out seed))
				throw new ArgumentException();

			var result = new Sudoku();
			// Set seed
			if (seed != 0) result.Seed = seed;
			// Fill initial
			for (int i = 0; i < parts[0].Length; i++)
			{
				short digit = (short)(parts[0][i] - '0');
				if (digit >= 1 && digit <= 9)
					result[i / 9, i % 9] = digit;
			}
			result.Initial = new ReadonlySudoku(result);
			// Fill guesses
			for (int i = 0; i < parts[1].Length; i++)
			{
				short digit = (short)(parts[1][i] - '0');
				if (digit >= 1 && digit <= 9)
					result[i / 9, i % 9] = digit;
			}

			return result;
		}

		#endregion PARSING
	}
}
