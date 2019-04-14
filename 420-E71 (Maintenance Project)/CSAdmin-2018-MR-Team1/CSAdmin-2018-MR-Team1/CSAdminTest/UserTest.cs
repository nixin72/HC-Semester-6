using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	/// <summary>
	///     Tests the setters and getters and automatic validation of the <see cref="CSAdmin2.Model.User" />
	/// </summary>
	[TestClass]
	public class UserTest
	{
		#region good checks

		/// <summary>
		///     Testing all propreties all success
		/// </summary>
		/// <seealso cref="CSAdmin2.Model.User" />
		[TestMethod]
		public void UserTest_HappyPath()
		{
			int id = Constants.RandInt();
			string lastName = "LastName";
			string firstName = "FirstName";
			string username = "Username";
			bool isActive = Constants.RandBool();
			User user = new User
			{
				IDUser = id,
				LastName = lastName,
				FirstName = firstName,
				Username = username,
				IsActive = isActive
			};
			Assert.AreEqual(id, user.IDUser, $"Expectd ({id}), got ({user.IDUser})");
			Assert.AreEqual(lastName, user.LastName, $"Expectd \"{lastName}\", got \"{user.LastName}\"");
			Assert.AreEqual(firstName, user.FirstName, $"Expectd \"{firstName}\", got \"{user.FirstName}\"");
			Assert.AreEqual(username, user.Username, $"Expectd \"{username}\", got \"{user.Username}\"");
			Assert.AreEqual(isActive, user.IsActive, $"Expectd ({isActive}), got ({user.IsActive})");
		}

		#endregion

		#region string length

		/// <summary>
		///     Testing a last name being too long, expecting an exception
		/// </summary>
		/// <seealso cref="CSAdmin2.Model.User" />
		[TestMethod, ExpectedException(typeof(CSAdminValidationException))]
		public void UserTest_LastNameTooLong()
		{
			User user = new User
			{
				LastName = Constants.LongWhiteSpaceString
			};
			Assert.Fail("The validation of the last name should have failed. It should fail for strings that are too long.");
		}

		/// <summary>
		///     Testing a first name being too long, expecting an exception
		/// </summary>
		/// <seealso cref="CSAdmin2.Model.User" />
		[TestMethod, ExpectedException(typeof(CSAdminValidationException))]
		public void UserTest_FirstNameTooLong()
		{
			User user = new User
			{
				FirstName = Constants.LongWhiteSpaceString
			};
			Assert.Fail("The validation of the first name should have failed. It should fail for strings that are too long.");
		}

		/// <summary>
		///     Testing a username being too long, expecting an exception
		/// </summary>
		/// <seealso cref="CSAdmin2.Model.User" />
		[TestMethod, ExpectedException(typeof(CSAdminValidationException))]
		public void UserTest_UsernameTooLong()
		{
			User user = new User
			{
				Username = Constants.LongWhiteSpaceString
			};
			Assert.Fail("The validation of the username should have failed. It should fail for strings that are too long.");
		}

		#endregion

		#region null check

		/// <summary>
		///     Testing a username being too long, expecting an exception
		/// </summary>
		/// <seealso cref="CSAdmin2.Model.User" />
		[TestMethod, ExpectedException(typeof(CSAdminValidationException))]
		public void UserTest_NullUsername()
		{
			User user = new User
			{
				Username = null
			};
			Assert.Fail("The validation of the username should have failed. It should fail when setting a null.");
		}

		#endregion
	}
}