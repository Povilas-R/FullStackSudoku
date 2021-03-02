using SudokuGenerator.Data.Enum;

namespace SudokuGenerator.Data
{
	public record Cell(int Row, int Column, Digit Digit);
}
