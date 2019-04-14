using System.Linq;
using CSAdmin2.Model;

namespace CSAdmin2.Logic
{
	public class ApplicationRolesManager
	{
		/// <summary>
		///     These classes are used for the application roles.
		///     The delete methods can be found in role Manager.
		/// </summary>
		/// <param name="roleCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static bool isRoleCodeUsed(string roleCode, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			if (context.Roles.Where(o => o.Code.Equals(roleCode)).Count() > 0)
			{
				return true;
			}

			return false;
		}

		public static bool isApplicationCodeUsed(string appCode, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			if (context.Applications.Where(o => o.Code.Equals(appCode)).Count() > 0)
			{
				return true;
			}

			return false;
		}
	}
}