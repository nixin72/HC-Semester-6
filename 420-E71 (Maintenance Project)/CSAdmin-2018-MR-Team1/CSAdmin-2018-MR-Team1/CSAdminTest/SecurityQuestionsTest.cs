using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class SecurityQuestionsTest
	{
		/// <summary>
		///     Testing basic setters and getters for Program. Using valid values
		/// </summary>
		[TestMethod]
		public void SecurityQuestionsTest_1()
		{
			SecurityQuestion s = new SecurityQuestion
			{
				IDSecurityQuestion = 420,
				Text = Constants.LongString.Substring(0, 255)
			};
			Assert.AreEqual(420, s.IDSecurityQuestion,
				"Test failed. Expected value for IDSecurityQuestion was 420. Actual Value: " + s.IDSecurityQuestion);
			Assert.AreEqual(Constants.LongString.Substring(0, 255), s.Text,
				"Test failed. Expected value for Text was " + Constants.LongString.Substring(0, 255) + " Actual Value: " + s.Text);
		}

		/// <summary>
		///     Testing null attribute
		/// </summary>
		[TestMethod]
		public void SecurityQuestionsTest_2()
		{
			SecurityQuestion s = new SecurityQuestion
			{
				IDSecurityQuestion = 69,
				Text = Constants.NullString
			};
			Assert.AreEqual(69, s.IDSecurityQuestion,
				"Test failed. Expected value for IDSecurityQuestion was 69. Actual Value: " + s.IDSecurityQuestion);
			Assert.AreEqual(Constants.NullString, s.Text,
				"Test failed. Expected value for Text was 'null' Actual Value: " + s.Text);
		}

		/// <summary>
		///     Testing Name length validation of more than 50 characters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Text' that has a string length of 256+ was inappropriately allowed")]
		public void SecurityQuestionsTest_3()
		{
			SecurityQuestion s = new SecurityQuestion
			{
				Text = Constants.LongString.Substring(0, 257)
			};
		}
	}
}