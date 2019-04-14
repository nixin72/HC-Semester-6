using System;
using System.Web;

namespace CSAdminMVC.Models
{
	public static class SessionInformation
	{
		private static string LOGGED_IN_KEY = "IsLoggedIn";

		public static bool IsLoggedIn()
		{
			return GetLoggedIn();
		}

		public static void SetLoggedIn(bool status)
		{
			HttpContext.Current.Session[LOGGED_IN_KEY] = status;
		}

		public static bool GetLoggedIn()
		{
			var LoggedIn = true;
			if (HttpContext.Current.Session[LOGGED_IN_KEY] == null ||
			    Convert.ToBoolean(HttpContext.Current.Session[LOGGED_IN_KEY]) == false)
			{
				LoggedIn = false;
			}

			return LoggedIn;
		}
	}
}