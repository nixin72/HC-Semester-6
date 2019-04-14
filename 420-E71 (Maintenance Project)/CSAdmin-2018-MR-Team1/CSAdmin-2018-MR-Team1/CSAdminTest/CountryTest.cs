using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class CountryTest
	{
		/// <summary>
		///     Test the getter and setters of Country.cs, using valid data
		/// </summary>
		[TestMethod]
		public void CountryTest_Constructor()
		{
			Country c = new Country
			{
				IDCountry = 20,
				Name = "China"
			};
			Assert.AreEqual(20, c.IDCountry, "Expected from IdCountry was 20, Actual: " + c.IDCountry);
			Assert.AreEqual("China", c.Name, "Expected from Name was China, Actual: " + c.Name);
		}

		/// <summary>
		///     Testing Required on Name
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException), "A null 'Name' was inappropriately allowed.")]
		public void CountryTest_NullName()
		{
			Country c = new Country
			{
				Name = Constants.NullString
			};
		}

		/// <summary>
		///     Testing String length of 100+ on Name
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Name' with a String length of 100+ was inappropriately allowed.")]
		public void CountryTest_NameTooLong()
		{
			Country c = new Country
			{
				Name = Constants.LongString
			};
		}

		/// <summary>
		///     Testing String length of 100+ on Name (whitespace)
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Name' with a String length of 100+ was inappropriately allowed.")]
		public void CountryTest_NameTooLongWhitespaces()
		{
			Country c = new Country
			{
				Name = Constants.LongWhiteSpaceString
			};
		}

		/// <summary>
		///     Testing to String
		/// </summary>
		[TestMethod]
		public void CountryTest_toString()
		{
			Country c = new Country
			{
				IDCountry = 20,
				Name = "China"
			};

			Assert.AreEqual("{ \"IDCountry\" : 20, \"Name\" : \"China\" }", c.ToString());
		}
	}
}