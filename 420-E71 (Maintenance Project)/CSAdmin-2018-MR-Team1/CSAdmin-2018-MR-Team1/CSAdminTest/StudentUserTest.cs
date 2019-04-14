using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class StudentUserTest
	{
		[TestMethod]
		public void StudentUserTest_1()
		{
			StudentUser s = new StudentUser
			{
				IDEtudiant = 420,
				IDUser = 421
			};

			Assert.AreEqual(420, s.IDEtudiant, "Expected value of 420 from IDEtudiant. Got: " + s.IDEtudiant);
			Assert.AreEqual(421, s.IDUser, "Expected value of 421 from IDUser. Got: " + s.IDUser);
		}
	}
}