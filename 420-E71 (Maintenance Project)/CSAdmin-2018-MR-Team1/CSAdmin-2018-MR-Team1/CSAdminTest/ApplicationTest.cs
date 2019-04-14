using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class ApplicationTest
	{
		/// <summary>
		///     Testing all properties, all successes
		/// </summary>
		[TestMethod]
		public void ApplicationTest_GetAndSet()
		{
			Application a = new Application
			{
				Code = "123",
				Description = "Here is a description",
				IDApplication = 0
			};

			Assert.AreEqual("123", a.Code, "Expected 123 from a.Code instead got: " + a.Code);
			Assert.AreEqual("Here is a description", a.Description,
				"Expected 'Here is a description' from a.Description instead got: " + a.Description);
			Assert.AreEqual(0, a.IDApplication, "Expected 0 from a.IdApplication instead got: " + a.IDApplication);
		}

		/// <summary>
		///     Testing 'Code' with string length of 2
		/// </summary>
		[TestMethod]
		public void ApplicationTest_CodeLen2()
		{
			Application a = new Application
			{
				Code = "HA"
			};
		}

		/// <summary>
		///     Testing 'Code' with string length of 4
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Code' that has a string length of 3+ was inappropriately allowed.")]
		public void ApplicationTest_CodeLen4()
		{
			Application a = new Application
			{
				Code = "ANDREW-HA"
			};
		}

		/// <summary>
		///     Testing 'Code' with string length of 3+ (whitespace)
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Code' that has a string length of 3+ was inappropriately allowed.")]
		public void ApplicationTest_CodeLen3OrMore()
		{
			Application a = new Application
			{
				Code = Constants.LongWhiteSpaceString
			};
		}

		/// <summary>
		///     Testing empty 'Code'
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException), "A Code of null was inappropriately allowed.")]
		public void ApplicationTest_NullCode()
		{
			Application a = new Application
			{
				Code = null
			};
		}

		/// <summary>
		///     Testing 'Description' with string length of 20
		/// </summary>
		[TestMethod]
		public void ApplicationTest_DescLen20()
		{
			Application a = new Application
			{
				Description = "Inserting20Charactrs"
			};
		}

		/// <summary>
		///     Testing 'Description' with string length of 51
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Description' that has a string length of 50+ was inappropriately allowed.")]
		public void ApplicationTest_DescLen51()
		{
			Application a = new Application
			{
				Description = "HereIsAFiftyOneCharacterStringMyNameIsAndrewHa!!!!!"
			};
		}

		/// <summary>
		///     Testing 'Description' with string length of 51 (whitespace)
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Description' that has a string length of 50+ was inappropriately allowed.")]
		public void ApplicationTest_DescLen51AllWhitespace()
		{
			Application a = new Application
			{
				Description = Constants.LongWhiteSpaceString
			};
		}

		/// <summary>
		///     Testing empty 'Description'
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException), "A 'Description' of null was inappropriately allowed.")]
		public void ApplicationTest_NullDescription()
		{
			Application a = new Application
			{
				Description = null
			};
		}
	}
}