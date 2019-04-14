using System.Web.Security;
using CSAdmin2.Logic;
using CSAdmin2.Model;
using CSAdminMVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	class LoginTest
	{
		[TestMethod]
		public void LoginTest_ConstructorHappyPath()
		{
			LoginModel l = new LoginModel
			{
				Username = "userad",
				password = "userad"
			};

			Assert.AreEqual("userad", l.Username,
				"Test failed. Expected value for username was userad. Actual Value: " + l.Username);
			Assert.AreEqual("userad", l.password,
				"Test failed. Expected value for password was userad. Actual Value: " + l.password);
		}

		[TestMethod]
		public void LoginTest_CorrectLogin()
		{
			LoginModel l = new LoginModel();
			l.Username = "userad";
			l.password = "userad";

			Assert.IsTrue(Membership.ValidateUser(l.Username, l.password),
				"Test failed. Expected validation for username " + l.Username + ", and password " + l.password + " to pass");
		}

		[TestMethod]
		public void LoginTest_IncorrectLogin()
		{
			LoginModel l = new LoginModel();
			l.Username = "userWrong";
			l.password = "userWrong";
			Assert.IsFalse(Membership.ValidateUser(l.Username, l.password),
				"Test failed. Expected validation for username " + l.Username + ", and password " + l.password + " to fail");
		}

		[TestMethod]
		public void LoginTest_AuthorizedLogin()
		{
			LoginModel l = new LoginModel
			{
				Username = "userad",
				password = "userad"
			};

			var context = new CSAdminContext();

			Assert.AreEqual(l, Auth.Login(l.Username, "CSA", context),
				"Test failed. Expected authorization for username " + l.Username + " to pass");
		}

		[TestMethod]
		public void LoginTest_UnauthorizedLogin()
		{
			LoginModel l = new LoginModel
			{
				Username = "1111111",
				password = "1111111"
			};

			var context = new CSAdminContext();

			Assert.AreEqual(null, Auth.Login(l.Username, "CSA", context),
				"Test failed. Expected authorization for username " + l.Username + " to fail");
		}
	}
}