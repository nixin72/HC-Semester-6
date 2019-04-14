using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class EmployeeUserTest
	{
		/// <summary>
		///     Testing getters and setters for employee user, all valid values
		/// </summary>
		[TestMethod]
		public void EmployeeUserTest_ConstructorHappyPath()
		{
			EmployeeUser eu = new EmployeeUser
			{
				IDEmploye = 12,
				IDUser = 21
			};

			Assert.AreEqual(12, eu.IDEmploye, "Expected 12 from IDEmploye, Actual: " + eu.IDEmploye);
			Assert.AreEqual(21, eu.IDUser, "Expected 21 from IDUser, Actual: " + eu.IDUser);
		}
	}
}