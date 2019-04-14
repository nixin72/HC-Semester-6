using System;
using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	public class SettingsTest
	{
		[TestMethod]
		public void SettingsTest_1()
		{
			Setting s = new Setting
			{
				CurrentYearSemester = 4200,
				SemesterEndDate = new DateTime(2018, 4, 20)
			};

			Assert.AreEqual(4200, s.CurrentYearSemester,
				"Expected CurrentYearSemester to be 4200. Got: " + s.CurrentYearSemester);
			Assert.AreEqual(new DateTime(2018, 4, 20), s.SemesterEndDate,
				"Expected EndDate to be 4/20/2018. Got: " + s.SemesterEndDate);
		}
	}
}