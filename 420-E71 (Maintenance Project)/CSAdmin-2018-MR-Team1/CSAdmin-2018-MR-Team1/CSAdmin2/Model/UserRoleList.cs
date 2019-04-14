using System.Collections.Generic;
using System.Linq;
using CSAdmin2.Logic;

namespace CSAdmin2.Model
{
	public class UserRoleList
	{
		public UserRoleList(int rollID, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			selectedRole = UserRoleManager.getRole(rollID);
			Users = UserRoleManager.getRoleUsers(rollID);
			UserRoles = UserRoleManager.getAllUserRoles(rollID);
		}
		public UserRoleList(IEnumerable<UserRole> urs, CSAdminContext context = null)
		{

			UserRoles = urs.OrderBy(x => x.User.LastName).ThenBy(x => x.User.FirstName);
			Users = Enumerable.Empty<User>();
			foreach (UserRole Uri in UserRoles) {
				Users = urs.Select(x => x.User);
			}
			Constants.Initialise(ref context);

		}

		public Role selectedRole { get; set; }

		public IEnumerable<User> Users { get; set; }
		public IEnumerable<UserRole> UserRoles { get; set; }
	}
}