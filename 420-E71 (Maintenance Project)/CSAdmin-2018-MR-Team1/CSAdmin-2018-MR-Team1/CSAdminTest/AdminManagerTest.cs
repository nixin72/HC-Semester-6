using CSAdmin2.Logic;
using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	class AdminManagerTest
	{
		[TestMethod]
		public void AdminManagerTest_TestGetAdminByUsername()
		{
			User user = AdminManager.GetAdminByUsername("userad");
			Assert.IsNotNull(user, "Test failed. Null User was returned.");
			Assert.AreEqual(user.FirstName, "Allan",
				"Test failed. FirstName was incorrect. Expected value: Allan. Actual: " + user.FirstName);
			Assert.AreEqual(user.LastName, "McDonald",
				"Test failed. LastName was incorrect. Expected value: McDonald. Actual: " + user.LastName);
			Assert.AreEqual(user.IsActive, true,
				"Test failed. IsActive was incorrect. Expected value: true. Actual: " + user.IsActive);
			Assert.AreEqual(user.Username, "userad",
				"Test failed. Username was incorrect. Expected value: amcdonald@cegep-heritage.qc.ca. Actual: " + user.Username);
			Assert.AreEqual(user.IDUser, "75C18AAB-6B50-4EF3-B403-97B10CB0DADB",
				"Test failed. IDUser was incorrect. Expected value: 75C18AAB-6B50-4EF3-B403-97B10CB0DADB. Actual: " + user.IDUser);
		}
	}
}