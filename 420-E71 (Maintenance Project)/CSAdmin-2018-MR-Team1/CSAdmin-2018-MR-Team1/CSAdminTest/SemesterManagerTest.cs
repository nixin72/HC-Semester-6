using System;
using CSAdmin2.Logic;
using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest
{
	[TestClass]
	class SemesterManagerTest
	{
		[TestMethod]
		public void SemesterManagerTest_TestConstructor()
		{
			Setting settings = new Setting();
			settings.SemesterEndDate = DateTime.Today.AddDays(3);
			settings.CurrentYearSemester = 20183;

			Assert.IsNotNull(settings, "Test failed. Null settings was returned.");
			Assert.AreEqual(settings.CurrentYearSemester, 20183,
				"Test failed. Incorrect semester was set. Expected value: 20182. Actual: " + settings.CurrentYearSemester);
			Assert.AreEqual(settings.SemesterEndDate, DateTime.Today.AddDays(3),
				"Test failed. Incorrect semester enddate was set. Expected value: " + DateTime.Today.AddDays(3) + ". Actual: " +
				settings.SemesterEndDate);
		}

		[TestMethod]
		public void SemesterManagerTest_TestSelectSettings()
		{
			Setting settings = SemesterManager.SelectSettings();
			Assert.IsNotNull(settings, "Test failed. Null settings was returned.");
			Assert.AreEqual(settings.CurrentYearSemester, 20182,
				"Test failed. Incorrect semester was returned. Expected value: 20182. Actual: " + settings.CurrentYearSemester);
			Assert.AreEqual(settings.SemesterEndDate, "",
				"Test failed. Incorrect semester enddate was returned. Expected value: ?????. Actual: " + settings.SemesterEndDate);
		}

		[TestMethod]
		public void SemesterManagerTest_UpdateCurrentSemester()
		{
			Setting settings = SemesterManager.SelectSettings();
			DateTime originalSemesterEndDate = settings.SemesterEndDate;
			int updated = SemesterManager.UpdateCurrentSemester(20182, DateTime.Now.AddDays(5));
			Assert.AreNotEqual(-1, updated, "Test failed. The semester failed to update and returned a value of -1");
			if (updated != -1)
			{
				SemesterManager.UpdateCurrentSemester(20182, originalSemesterEndDate);
			}
		}
	}
}