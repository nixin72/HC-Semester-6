using CSAdmin2.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class RoleManagerTest
	{
		[TestMethod]
		public void RoleManager_CreatUsername()
		{
			string fName = "Jhon";
			string lName = "Handcock";
			Assert.AreEqual("jhandcock", RoleManager.CreateUserName(fName, lName),
				"Test failed. Expected value for IDRole was jhandcock. Actual Value: " + RoleManager.CreateUserName(fName, lName));

			fName = "jHoN";
			lName = "hANdCoCK";
			Assert.AreEqual("jhandcock", RoleManager.CreateUserName(fName, lName),
				"Test failed. Expected value for IDRole was jhandcock. Actual Value: " + RoleManager.CreateUserName(fName, lName));

			fName = "jHoN";
			lName = "hANdCoCK";
			Assert.AreEqual("jhandcock", RoleManager.CreateUserName(fName, lName),
				"Test failed. Expected value for IDRole was jhandcock. Actual Value: " + RoleManager.CreateUserName(fName, lName));

			fName = "jhon";
			lName = "1hand2cock3";
			Assert.AreEqual("jhandcock", RoleManager.CreateUserName(fName, lName),
				"Test failed. Expected value for IDRole was jhandcock. Actual Value: " + RoleManager.CreateUserName(fName, lName));

			fName = "1jh2on3";
			lName = "handcock";
			Assert.AreEqual("jhandcock", RoleManager.CreateUserName(fName, lName),
				"Test failed. Expected value for IDRole was jhandcock. Actual Value: " + RoleManager.CreateUserName(fName, lName));

			fName = "/jh/on/";
			lName = "handcock";
			Assert.AreEqual("jhandcock", RoleManager.CreateUserName(fName, lName),
				"Test failed. Expected value for IDRole was jhandcock. Actual Value: " + RoleManager.CreateUserName(fName, lName));

			fName = "jhon";
			lName = "/hand/cock/";
			Assert.AreEqual("jhandcock", RoleManager.CreateUserName(fName, lName),
				"Test failed. Expected value for IDRole was jhandcock. Actual Value: " + RoleManager.CreateUserName(fName, lName));
		}

		[TestMethod]
		public void RoleManager_isRoleCodeUsed()
		{
			string roleID = "ST";
			Assert.AreEqual(true, ApplicationRolesManager.isRoleCodeUsed(roleID),
				"Test failed. Expected value for IDRole was true. Actual Value: " + ApplicationRolesManager.isRoleCodeUsed(roleID));
		}

		[TestMethod]
		public void RoleManager_isApplicationCodeUsed()
		{
			string appID = "CSA";
			Assert.AreEqual(true, ApplicationRolesManager.isApplicationCodeUsed(appID),
				"Test failed. Expected value for IDRole was true. Actual Value: " + ApplicationRolesManager.isRoleCodeUsed(appID));
		}
	}
}