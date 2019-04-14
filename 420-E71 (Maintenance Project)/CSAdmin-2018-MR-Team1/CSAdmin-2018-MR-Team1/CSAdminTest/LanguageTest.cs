using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class LanguageTest
	{
		/// <summary>
		///     Testing getters and setters for language with valid values
		/// </summary>
		[TestMethod]
		public void LanguageTest_ConstructorHappyPath()
		{
			Language l = new Language
			{
				LanguageID = 1,
				Name = "TestName",
				IsDefault = true
			};

			Assert.AreEqual(1, l.LanguageID, "Expected value for LanguageId was 1, Actual value was: " + l.LanguageID);
			Assert.AreEqual("TestName", l.Name, "Expected value for Name was 'TestName', Actual value was: " + l.Name);
			Assert.AreEqual(true, l.IsDefault, "Expected value for IsDefault was true, Actual value was: " + l.IsDefault);
		}

		/// <summary>
		///     Testing getters and setters for language with null values
		/// </summary>
		[TestMethod]
		public void LanguageTest_NullData()
		{
			Language l = new Language
			{
				Name = null,
				IsDefault = null
			};

			Assert.AreEqual(null, l.Name, "Expected value for Name was 'null, Actual value was: " + l.Name);
			Assert.AreEqual(null, l.IsDefault, "Expected value for IsDefault was null, Actual value was: " + l.IsDefault);
		}

		/// <summary>
		///     Testing string length check for name of 255+ characters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Name' that has a string length of 255+ was inappropriately allowed.")]
		public void LanguageTest_NameTooLong()
		{
			Language l = new Language
			{
				Name = Constants.LongString.Substring(0, 256)
			};
		}

		/// <summary>
		///     Testing string length check for name of 255+ whitespace characters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Name' that has a whitespace string length of 255+ was inappropriately allowed.")]
		public void LanguageTest_NameTooLongWhitespace()
		{
			Language l = new Language
			{
				Name = Constants.LongWhiteSpaceString.Substring(0, 256)
			};
		}
	}
}