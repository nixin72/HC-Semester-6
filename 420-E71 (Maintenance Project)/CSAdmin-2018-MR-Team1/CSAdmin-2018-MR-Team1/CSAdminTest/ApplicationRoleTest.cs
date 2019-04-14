using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class ApplicationRoleTest
	{
		/// <summary>
		///     Tests the getters and setters of ApplicationRoles
		/// </summary>
		[TestMethod]
		public void ApplicationRoleTest_GetAndSet()
		{
			int testAppRoleID = 420;
			int testAppID = 69;
			int testRoleID = 100;
			bool testState = false;

			ApplicationRole ar = new ApplicationRole
			{
				IDApplicationRole = testAppRoleID,
				IDApplication = testAppID,
				IDRole = testRoleID,
				IsActive = testState
			};

			Assert.AreEqual(testAppRoleID, ar.IDApplicationRole,
				"Expected IdApplicationRole is 420, Actually got: " + ar.IDApplicationRole);
			Assert.AreEqual(testAppID, ar.IDApplication, "Expected IdApplication is 69, Actually got: " + ar.IDApplication);
			Assert.AreEqual(testRoleID, ar.IDRole, "Expected IdRole is 100, Actually got: " + ar.IDRole);
			Assert.AreEqual(testState, ar.IsActive, "Expected IsActive: false, Actually got: " + ar.IsActive);
		}
	}
}