namespace SudokuGenerator.Data
{
	public interface ISolvingMethod
	{
		Cell Cell { get; }
		string Description { get; }
	}
}
