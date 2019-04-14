using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class ProgramVersionTest
	{
		[TestMethod]
		public void ProgramVersionTest_ConstructorHappyPath()
		{
			ProgramVersion p = new ProgramVersion
			{
				IDProgramVersion = 123,
				IDProgram = 321,
				IDProgramClara = 111
			};

			Assert.AreEqual(123, p.IDProgramVersion,
				"Test Failed. IdProgramVersion was incorrect. Expected value: 123. Actual: " + p.IDProgramVersion);
			Assert.AreEqual(321, p.IDProgram,
				"Test Failed. IdProgramVersion was incorrect. Expected value: 321. Actual: " + p.IDProgram);
			Assert.AreEqual(111, p.IDProgramClara,
				"Test Failed. IdProgramVersion was incorrect. Expected value: 11. Actual: " + p.IDProgramClara);
		}
	}
}