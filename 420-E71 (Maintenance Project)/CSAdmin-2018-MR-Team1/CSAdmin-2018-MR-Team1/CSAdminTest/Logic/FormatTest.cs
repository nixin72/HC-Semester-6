using System;
using CSAdmin2.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest.Logic
{
	[TestClass]
	public class FormatTest
	{
		[TestClass]
		public class FormatDate_Test
		{
			[TestMethod]
			public void FormatDate_HappyPath_FormatTest()
			{
				var date = DateTime.Parse("3/19/2018");
				var exp = "2018-Mar-19";
				var act = Format.FormatDate(date);

				Assert.AreEqual(exp, act);
			}
		}

		[TestClass]
		public class FormatSemester_Test
		{
			[TestMethod]
			public void FormatSemester_HappyPathWinter_FormatTest()
			{
				short semester = 20181;
				var exp = "Winter 2018";
				string act = Format.FormatSemester(semester);

				Assert.AreEqual(exp, act);
			}

			[TestMethod]
			public void FormatSemester_HappyPathSummer_FormatTest()
			{
				short semester = 20182;
				var exp = "Summer 2018";
				string act = Format.FormatSemester(semester);

				Assert.AreEqual(exp, act);
			}

			[TestMethod]
			public void FormatSemester_HappyPathFall_FormatTest()
			{
				short semester = 20183;
				var exp = "Fall 2018";
				string act = Format.FormatSemester(semester);

				Assert.AreEqual(exp, act);
			}

			[TestMethod]
			public void FormatSemester_InvalidFormat_FormatTest()
			{
				//This probably *should* throw an error, but currently does not. 
				short semester = 281;
				var exp = "Winter 28";
				string act = Format.FormatSemester(semester);

				Assert.AreEqual(exp, act);
			}

			[TestMethod]
			public void FormatSemester_InvalidSemester_FormatTest()
			{
				short semester = 20184;
				string exp = null;
				string act = Format.FormatSemester(semester);

				Assert.AreEqual(exp, act);
			}
		}

		[TestClass]
		public class FormatLastFirst_Test
		{
			[TestMethod]
			public void FormatLastFirst_HappyPath_FormatTest()
			{
				var first = "Allan";
				var last = "McDonald";
				var exp = "McDonald, Allan";
				var act = Format.FormatLastFirst(first, last);

				Assert.AreEqual(exp, act);
			}

			[TestMethod]
			public void FormatLastFirst_EmptyStrings_FormatTest()
			{
				var first = "";
				var last = "";
				var exp = ", ";
				var act = Format.FormatLastFirst(first, last);

				Assert.AreEqual(exp, act);
			}
		}

		[TestClass]
		public class FormatFirstLast_Test
		{
			[TestMethod]
			public void FormatFirstLast_HappyPath_FormatTest()
			{
				var first = "Allan";
				var last = "McDonald";
				var exp = "Allan McDonald";
				var act = Format.FormatFirstLast(first, last);

				Assert.AreEqual(exp, act);
			}

			[TestMethod]
			public void FormatFirstLast_EmptyStrings_FormatTest()
			{
				var first = "";
				var last = "";
				var exp = " ";
				var act = Format.FormatFirstLast(first, last);

				Assert.AreEqual(exp, act);
			}
		}
	}
}