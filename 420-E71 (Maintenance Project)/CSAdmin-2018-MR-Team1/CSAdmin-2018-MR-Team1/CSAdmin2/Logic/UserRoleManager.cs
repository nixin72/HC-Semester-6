using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using CSAdmin2.Model;

namespace CSAdmin2.Logic
{
	public class UserRoleManager
	{
		public static IEnumerable<Role> getRoles(CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return context.Roles;
		}

		public static IEnumerable<User> getUsers(CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return context.EmployeeUsers.Select(x => x.User).Where(x => x.IsActive).OrderBy(x => x.LastName + x.FirstName);
		}

		public static IEnumerable<UserRole> getUserRoles(CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return context.UserRoles;
		}

		public static IEnumerable<UserRole> getAllUserRoles(int roleId, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return context.UserRoles.Where(e => e.IDRole == roleId);
		}

		public static Role getRole(int rollID, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return context.Roles.FirstOrDefault(x => x.IDRole == rollID);
		}

		public static void editRole(UserRole newUserRole, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			context.UserRoles.AddOrUpdate(newUserRole);
			context.SaveChanges();
		}

		public static IEnumerable<User> getRoleUsers(int rollID, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return context.UserRoles.Where(x => x.IDRole == rollID).Select(x => x.User);
		}

		public static UserRole getUserRole(int userRoleId, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return context.UserRoles.FirstOrDefault(ur => ur.IDUserRole == userRoleId);
		}

		public static void addUsers(User newU, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			context.Users.Add(newU);
			context.SaveChanges();
		}

		public static void updateUser(Application updateU, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			context.Users.Find(updateU);
			context.SaveChanges();
		}

		public static void deleteUser(int userId, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			User user = context.Users.Find(userId);
			context.Users.Remove(user);
			context.SaveChanges();
		}

		public static IEnumerable<UserRole> GetAndUserRoles(string[] ids, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			IEnumerable<UserRole> listOfUserRoles = Enumerable.Empty<UserRole>();
			IEnumerable<User> ListOfUsers = Enumerable.Empty<User>();
			IEnumerable<User> TempListOfUsers = Enumerable.Empty<User>();
			
			int index = 0;
			foreach (string id in ids) {
				int intID = Int32.Parse(id);
				if (index == 0) {
					ListOfUsers = context.UserRoles.Where(x => x.IDRole == intID).Select(x => x.User);
				}
				else {
					ListOfUsers = ListOfUsers.Intersect(context.UserRoles.Where(x => x.IDRole == intID).Select(x => x.User));
				}
				index++;
			}

			int[] rolIDs = ids.Select(int.Parse).ToArray();
			int[] userIDs = ListOfUsers.Select(x => x.IDUser).ToArray();
			listOfUserRoles = context.UserRoles.Where(x => rolIDs.Contains(x.IDRole) && userIDs.Contains(x.User.IDUser));
				
			return listOfUserRoles;
		}
	}
}