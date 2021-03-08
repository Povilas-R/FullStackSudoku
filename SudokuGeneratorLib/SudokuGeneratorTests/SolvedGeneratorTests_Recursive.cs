using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuGenerator.Data.Enum;

namespace SudokuGeneratorTests
{
	[TestClass]
	public class SolvedGeneratorTests_Recursive : BaseSolvedGeneratorTests
	{
		protected override GenerationAlgorithm _algorithm => GenerationAlgorithm.Recursive;
	}
}
