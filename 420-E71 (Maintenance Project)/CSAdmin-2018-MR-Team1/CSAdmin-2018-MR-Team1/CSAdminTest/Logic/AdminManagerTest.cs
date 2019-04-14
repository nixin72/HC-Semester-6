using CSAdmin2.Logic;
using CSAdmin2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSAdminTest.Logic
{
	[TestClass]
	public class AdminManagerTest
	{
		[TestClass]
		public class GetAdminByUsername_Test
		{
			[TestMethod]
			public void GetAdminByUsername_HappyPath_AdminManager()
			{
				string user = "userad";

				User act = AdminManager.GetAdminByUsername(user);

				Assert.AreEqual(null, act);
			}

			[TestMethod]
			public void GetAdminByUsername_InvalidUsername_AdminManager()
			{
			}
		}
	}
}