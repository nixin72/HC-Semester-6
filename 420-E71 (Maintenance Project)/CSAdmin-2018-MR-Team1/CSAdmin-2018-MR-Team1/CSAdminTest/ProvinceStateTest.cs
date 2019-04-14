using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class ProvinceStateTest
	{
		/// <summary>
		///     Testing all the basic getters and setters, using valid data
		/// </summary>
		[TestMethod]
		public void ProvinceStateTest_1()
		{
			ProvinceState p = new ProvinceState
			{
				IDProvinceState = 420,
				Name = Constants.LongString.Substring(0, 49),
				IDCountry = 69
			};

			Assert.AreEqual(420, p.IDProvinceState,
				"Test failed. Expected value for IDProvinceState was 420. Actual Value: " + p.IDProvinceState);
			Assert.AreEqual(Constants.LongString.Substring(0, 49), p.Name,
				"Test failed. Expected value for " + Constants.LongString.Substring(0, 49) + ". Actual Value: " + p.Name);
			Assert.AreEqual(69, p.IDCountry, "Test failed. Expected value for IDCountry was 69. Actual Value: " + p.IDCountry);
		}

		/// <summary>
		///     Testing all the name being required
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Name' that has a value of null was inappropriately allowed")]
		public void ProvinceStateTest_2()
		{
			ProvinceState p = new ProvinceState
			{
				IDProvinceState = 420,
				Name = null,
				IDCountry = 69
			};
		}

		/// <summary>
		///     Testing name length of 50+ string length
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(CSAdminValidationException),
			"A 'Name' that has a length of 50+ was inappropriately allowed")]
		public void ProvinceStateTest_3()
		{
			ProvinceState p = new ProvinceState
			{
				IDProvinceState = 420,
				Name = Constants.LongString.Substring(0, 51),
				IDCountry = 69
			};
		}
	}
}