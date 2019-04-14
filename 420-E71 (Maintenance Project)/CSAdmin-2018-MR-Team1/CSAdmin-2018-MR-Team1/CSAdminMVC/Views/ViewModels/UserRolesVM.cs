using System.Collections.Generic;
using System.Linq;
using CSAdmin2.Logic;
using CSAdmin2.Model;

namespace CSAdminMVC.Views.ViewModels
{
	public class UserRolesVM
	{
		public UserRolesVM(CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			// don't let those things create 2 different connections at the same time
			Roles = UserRoleManager.getRoles(context).OrderBy(r => r.Description);
			Users = UserRoleManager.getUsers(context).OrderBy(u => u.LastName).ThenBy(u => u.LastName);
			UserRoles = UserRoleManager.getUserRoles(context);
		}

		public IEnumerable<User> Users { get; set; }
		public IEnumerable<Role> Roles { get; set; }
		public IEnumerable<UserRole> UserRoles { get; set; }
	}
}