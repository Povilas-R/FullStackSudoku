using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuGenerator.Data.Enum;

namespace SudokuGeneratorTests
{
	[TestClass]
	public class SolvedGeneratorTests_Backtracking : BaseSolvedGeneratorTests
	{
		protected override GenerationAlgorithm _algorithm => GenerationAlgorithm.Backtracking;
	}
}
