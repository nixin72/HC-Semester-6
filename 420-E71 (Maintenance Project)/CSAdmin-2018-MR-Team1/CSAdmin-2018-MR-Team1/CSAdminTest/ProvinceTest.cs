using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class ProvinceTest
	{
		/// <summary>
		///     Testing basic getters and setters for province, using valid values
		/// </summary>
		[TestMethod]
		public void ProvinceTest_ConstructorHappyPath()
		{
			Province p = new Province
			{
				IDProvince = 12,
				Text = "Quebec",
				Abbreviation = "QC"
			};

			Assert.AreEqual(12, p.IDProvince,
				"Test failed. IdProvince was incorrect. Expected value: 12. Actual: " + p.IDProvince);
			Assert.AreEqual("Quebec", p.Text, "Test failed. Text was incorrect. Expected value: 12. Actual: " + p.Text);
			Assert.AreEqual("QC", p.Abbreviation,
				"Test failed. Abbreviation was incorrect. Expected value: 12. Actual: " + p.Abbreviation);
		}

		/// <summary>
		///     Testing the required validation for Text
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Text' that has a value of null was inappropriately allowed.")]
		public void ProvinceTest_NullText()
		{
			Province p = new Province
			{
				Text = Constants.NullString
			};
		}

		/// <summary>
		///     Testing the Text string length validation of 100+ characters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Text' that has a string length of 100+ was inappropriately allowed.")]
		public void ProvinceTest_TextTooLong()
		{
			Province p = new Province
			{
				Text = Constants.LongString.Substring(0, 102)
			};
		}

		/// <summary>
		///     Testing the Text whitespace string length validation of 100+ characters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Text' that has a whitespace string length of 100+ was inappropriately allowed.")]
		public void ProvinceTest_TextTooLongWhitespace()
		{
			Province p = new Province
			{
				Text = Constants.LongWhiteSpaceString.Substring(0, 102)
			};
		}

		/// <summary>
		///     Testing the required validation for Abbreviation
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"An 'Abbreviation' that has a value of null was inappropriately allowed.")]
		public void ProvinceTest_NullAbbreviation()
		{
			Province p = new Province
			{
				Abbreviation = Constants.NullString
			};
		}

		/// <summary>
		///     Testing the Abbreviation string length validation of 2+ characters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"An 'Abbreviation' that has a string length of 2+ was inappropriately allowed.")]
		public void ProvinceTest_AbreviationTooLong()
		{
			Province p = new Province
			{
				Abbreviation = Constants.LongString.Substring(0, 3)
			};
		}

		/// <summary>
		///     Testing the Abbreviation whitespace string length validation of 2+ characters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"An 'Abbreviation' that has a whitespace string length of 2+ was inappropriately allowed.")]
		public void ProvinceTest_AbreviationTooLongWhitespace()
		{
			Province p = new Province
			{
				Abbreviation = Constants.LongWhiteSpaceString.Substring(0, 3)
			};
		}
	}
}