using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using CSAdmin2.Model;

namespace CSAdmin2.Logic
{
	/// <summary>
	///     A static handler used to handle all sorts of authentification related tasks.
	/// </summary>
	public static class Auth
	{
		/// <summary>
		///     Used to make sure a user can access a system,
		///     This is a prototype replacement for the dbo.uspLogin stored procedure
		/// </summary>
		/// <param name="username"></param>
		/// <param name="applicationCode"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static User Login(string username, string applicationCode, CSAdminContext context = null)
		{
			try
			{
				Constants.Initialise(ref context);
			}
			catch (Exception e)
			{
				context = new CSAdminContext();
			}

			IQueryable<User> user =
				from User u in context.Users
				join UserRole ur in context.UserRoles
					on u.IDUser equals ur.IDUser
				join ApplicationRole ar in context.ApplicationRoles
					on ur.IDRole equals ar.IDRole
				join Application a in context.Applications
					on ar.IDApplication equals a.IDApplication
				join Role r in context.Roles
					on ur.IDRole equals r.IDRole
				where
					ur.IsActive &&
					ar.IsActive &&
					u.IsActive &&
					a.Code == applicationCode &&
					u.Username == username
				select
					u;

			return user.Distinct().First();
		}

		public static bool LDAPAuth(string username, string password)
		{
			PrincipalContext pc = GetADContext();
			bool authenticated = pc.ValidateCredentials(username, password);
			pc.Dispose();
			return authenticated;
		}

		internal static PrincipalContext GetADContext()
		{
			return new PrincipalContext(ContextType.Domain, "DC1.cegep-heritage.qc.ca");
		}

		internal static PrincipalSearcher getPrincipalSearcher()
		{
			return new PrincipalSearcher(new UserPrincipal(GetADContext()));
		}
	}
}