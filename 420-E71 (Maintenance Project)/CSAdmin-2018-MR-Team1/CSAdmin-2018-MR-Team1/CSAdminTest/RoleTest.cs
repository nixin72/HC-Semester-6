using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class RoleTest
	{
		/// <summary>
		///     Testing all the basic getters and setters, using valid data
		/// </summary>
		[TestMethod]
		public void RoleTest_1()
		{
			Role r = new Role
			{
				IDRole = 111,
				Code = Constants.LongString.Substring(0, 2),
				Description = Constants.LongString.Substring(0, 49)
			};

			Assert.AreEqual(111, r.IDRole, "Test failed. Expected value for IDRole was 111. Actual Value: " + r.IDRole);
			Assert.AreEqual(Constants.LongString.Substring(0, 2), r.Code,
				"Test failed. Expected value for Code: " + Constants.LongString.Substring(0, 2) + ". Actual Value: " + r.Code);
			Assert.AreEqual(Constants.LongString.Substring(0, 49), r.Description,
				"Test failed. Expected value for IDCountry was " + Constants.LongString.Substring(0, 49) + ". Actual Value: " +
				r.Description);
		}

		/// <summary>
		///     Testing code being required
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Code' that has a value of null was inappropriately allowed")]
		public void RoleTest_2()
		{
			Role r = new Role
			{
				Code = Constants.NullString
			};
		}

		/// <summary>
		///     Testing description being required
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Description' that has a value of null was inappropriately allowed")]
		public void RoleTest_3()
		{
			Role r = new Role
			{
				Description = Constants.NullString
			};
		}

		/// <summary>
		///     Testing code length of 3+ string length
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Code' that has a length of 3+ was inappropriately allowed")]
		public void RoleTest_4()
		{
			Role r = new Role
			{
				Code = Constants.LongString.Substring(0, 5)
			};
		}

		/// <summary>
		///     Testing description length of 3+ string length
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Code' that has a length of 50 was inappropriately allowed")]
		public void RoleTest_5()
		{
			Role r = new Role
			{
				Description = Constants.LongString.Substring(0, 51)
			};
		}
	}
}