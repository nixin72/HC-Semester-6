using CSAdmin2.Model;

namespace CSAdmin2.Logic
{
	// Note: forgot to include the admin manager
	public static class AdminManager
	{
		/// <summary>
		///     Gets an administrator within the system using it's username
		/// </summary>
		/// <param name="username"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static User GetAdminByUsername(string username, CSAdminContext context = null)
		{
			return Auth.Login(username, Constants.CSADMIN_APP_CODE);
		}
	}
}