using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class UserRoleTest
	{
		[TestMethod]
		public void UserRoleTest_1()
		{
			UserRole u = new UserRole
			{
				IDRole = 1,
				IDUserRole = 2,
				IDUser = 3,
				IsActive = false
			};

			Assert.AreEqual(1, u.IDRole, "Incorrect value for IDRole, Expected: 1. Actual: " + u.IDRole);
			Assert.AreEqual(2, u.IDUserRole, "Incorrect value for IDUserRole, Expected: 2. Actual: " + u.IDUserRole);
			Assert.AreEqual(3, u.IDUser, "Incorrect value for IDUser, Expected: 3. Actual: " + u.IDUser);
			Assert.AreEqual(false, u.IsActive, "Incorrect value for IsActive, Expected: 'false'. Actual: " + u.IsActive);
		}
	}
}