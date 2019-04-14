using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class PageRoleSecurityTest
	{
		/// <summary>
		///     Testing the getters and setters for PageRoleSecurity, using valid values
		/// </summary>
		[TestMethod]
		public void PageRoleSecurityTest_ConstructorHappyPath()
		{
			PageRoleSecurity p = new PageRoleSecurity
			{
				IDApplication = 120,
				PageName = "PageName",
				Role = "RoleHere"
			};

			Assert.AreEqual(120, p.IDApplication, "Test failed. Expected value: 120, Actual value: " + p.IDApplication);
			Assert.AreEqual("PageName", p.PageName, "Test failed. Expected value: 'PageName', Actual value: " + p.PageName);
			Assert.AreEqual("RoleHere", p.Role, "Test failed. Expected value: 'RoleHere', Actual value: " + p.Role);
		}

		/// <summary>
		///     Testing the Role length validation 50+ characters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Role' that has a string length of 50+ was inappropriately allowed.")]
		public void PageRoleSecurityTest_RoleTooLong()
		{
			PageRoleSecurity p = new PageRoleSecurity
			{
				Role = Constants.LongString.Substring(0, 52)
			};
		}

		/// <summary>
		///     Testing the Role length validation 50+ whitespace characters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Role' that has a string length of 50+ was inappropriately allowed.")]
		public void PageRoleSecurityTest_RoleTooLongWhitespace()
		{
			PageRoleSecurity p = new PageRoleSecurity
			{
				Role = Constants.LongString.Substring(0, 52)
			};
		}

		/// <summary>
		///     Testing the Role with null value
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Role' that has a string of null was inappropriately allowed.")]
		public void PageRoleSecurityTest_NullRole()
		{
			PageRoleSecurity p = new PageRoleSecurity
			{
				Role = Constants.NullString
			};
		}

		/// <summary>
		///     Testing the PageName length validation 200+ characters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'PageName' that has a string length of 200+ was inappropriately allowed.")]
		public void PageRoleSecurityTest_PageNameTooLong()
		{
			PageRoleSecurity p = new PageRoleSecurity
			{
				PageName = Constants.LongString.Substring(0, 202)
			};
		}

		/// <summary>
		///     Testing the PageName length validation 200+ whitespace characters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'PageName' that has a string length of 200+ was inappropriately allowed.")]
		public void PageRoleSecurityTest_PageNameTooLongWhitespace()
		{
			PageRoleSecurity p = new PageRoleSecurity
			{
				PageName = Constants.LongString.Substring(0, 202)
			};
		}

		/// <summary>
		///     Testing the PageName with null value
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'PageName' that has a string of null was inappropriately allowed.")]
		public void PageRoleSecurityTest_NullPageName()
		{
			PageRoleSecurity p = new PageRoleSecurity
			{
				PageName = Constants.NullString
			};
		}
	}
}