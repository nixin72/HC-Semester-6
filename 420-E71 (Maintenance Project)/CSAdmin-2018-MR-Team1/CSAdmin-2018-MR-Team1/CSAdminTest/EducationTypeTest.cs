using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class EducationTypeTest
	{
		/// <summary>
		///     Testing the getters and setters for education type. Using all valid fields
		/// </summary>
		[TestMethod]
		public void EducationTypeTest_ConstructorHappyPath()
		{
			EducationType e = new EducationType
			{
				IDEducationType = 0,
				Name = "HereIsAName"
			};

			Assert.AreEqual(0, e.IDEducationType, "Expected result from IdEducationType was 0, Actual: " + e.IDEducationType);
			Assert.AreEqual("HereIsAName", e.Name, "Expected result from Name was 'HereIsAName', Actual: " + e.Name);
		}

		/// <summary>
		///     Testing the validation for string length for Name. Testing for String length of 50+
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Name' that has a string length of 50+ was inappropriately allowed.")]
		public void EducationTypeTest_StringTooLong()
		{
			EducationType e = new EducationType
			{
				Name = Constants.LongString
			};
		}


		/// <summary>
		///     Testing the validation for string length for Name. Testing for String length of 50+ (whitespace)
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Name' that has a string length of 50+ was inappropriately allowed.")]
		public void EducationTypeTest_StringTooLongWhitespace()
		{
			EducationType e = new EducationType
			{
				Name = Constants.LongWhiteSpaceString
			};
		}

		/// <summary>
		///     Testing the validation for required field for Name
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException), "A 'Name' that is null was inappropriately allowed.")]
		public void EducationTypeTest_NullName()
		{
			EducationType e = new EducationType
			{
				Name = Constants.NullString
			};
		}

		[TestMethod]
		public void EducationTypeTest_toString()
		{
			EducationType e = new EducationType
			{
				IDEducationType = 0,
				Name = "HereIsAName"
			};

			Assert.AreEqual("{ \"IDEducationType\" : 0, \"Name\" : \"HereIsAName\" }", e.ToString());
		}
	}
}