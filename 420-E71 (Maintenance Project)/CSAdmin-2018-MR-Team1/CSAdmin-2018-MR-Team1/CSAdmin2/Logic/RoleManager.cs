using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CSAdmin2.Clara;
using CSAdmin2.Model;

namespace CSAdmin2.Logic
{
	public class RoleManager
	{
		private static readonly string[] SPECIAL_ROLE_CODES = {"CEC", "AD", "CSA", "ASM", "ASA"};

		/// <summary>
		///     Takes in a first name and last name of a user, and returns a username created from both concatenated and encoded.
		/// </summary>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <returns></returns>
		public static string CreateUserName(string firstName, string lastName)
		{
			string userName = null;

			if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
			{
				firstName = Constants.NormaliseName(firstName);
				lastName = Constants.NormaliseName(lastName);
				userName = firstName.ToLower()[0] + lastName.ToLower();
				byte[] tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(userName);
				userName = Encoding.UTF8.GetString(tempBytes);
				userName = userName.Replace("dr.", "");
				userName = userName.Replace("-", "");
				userName = userName.Trim();
			}

			return userName;
		}

		/// <summary>
		///     Inserts OR updates a user role object using a given employee id, and role code.
		/// </summary>
		/// <param name="_IDUser"></param>
		/// <param name="_RoleCode"></param>
		/// <param name="context"></param>
		public static void InsertUserRole(int _IDUser, int _IDRole, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);

			//List<User> userList = new List<User>();

			//The method below gets a list of users using the EmployeeId.
			//User user = context.EmployeeUsers.FirstOrDefault(e => e.IDUser == _IDUser).User; //Commenting out guillaume's non functioning code

			IQueryable<EmployeeUser> userList =
				from EmployeeUser eu in context.EmployeeUsers
				where eu.IDUser == _IDUser
				select eu;

			User user = new User();

			if (userList.Any())
			{
				user = userList.FirstOrDefault().User;
			}


			//The method below gets a list of roles using the RoleCode.
			//Role role = context.Roles.FirstOrDefault(r => r.IDRole == _IDRole); //Commenting out guillaume's non functioning code

			IQueryable<Role> roleList =
				from Role r in context.Roles
				where r.IDRole == _IDRole
				select r;

			Role role = new Role();

			if (roleList.Any())
			{
				role = roleList.FirstOrDefault();
			}

			// use explicite types
			//List<int> IDRolesList;

			if (user == null)
			{
				//INSERT

				//Grabs user's firstname, lastname, username, isActive and inserts into respective tables.
				IQueryable<Employe> usersInfo =
					from Employe u in context.Employes
					where u.IDEmploye == _IDUser
					select u;


				User newUser = new User
				{
					LastName = usersInfo.First().Nom,
					FirstName = usersInfo.First().NomPrenom,
					IsActive = true
				};

				newUser.Username = CreateUserName(newUser.FirstName, newUser.LastName);

				context.Users.Add(newUser);

				context.SaveChanges();

				//TODO: Scope_identity()
				_IDUser = newUser.IDUser;

				EmployeeUser employeeUser = new EmployeeUser
				{
					IDUser = _IDUser,
					IDEmploye = _IDUser
				};
				context.EmployeeUsers.Add(employeeUser);

				context.SaveChanges();

				UserRole userRole = new UserRole
				{
					IDUser = _IDUser,
					IDRole = role.IDRole,
					IsActive = true
				};
				context.UserRoles.Add(userRole);

				IQueryable<User> users2 =
					from User u in context.EmployeeUsers
					where u.EmployeeUser.IDEmploye == _IDUser
					select u;

				_IDUser = users2.First().IDUser;

				context.SaveChanges();
			}
			else
			{
				//UPDATE

				user.IsActive = true;
				UserRole oldUserRole = context.UserRoles.FirstOrDefault(ur => ur.IDRole == role.IDRole && ur.IDUser == user.IDUser);
				if (oldUserRole == null)
				{
					UserRole userRole = new UserRole
					{
						IDUser = _IDUser,
						IDRole = _IDRole,
						IsActive = true
					};
					context.UserRoles.AddOrUpdate(userRole);
				}
				else
				{
					oldUserRole.IsActive = true;
				}

				context.SaveChanges();
			}
		}

		/// <summary>
		///     sets a user role found from the given employee id and role code to inactive
		/// </summary>
		/// <param name="_IDEmploye"></param>
		/// <param name="_RoleCode"></param>
		/// <param name="context"></param>
		public static void DeleteUserRole(int _IDUser, string _RoleCode, CSAdminContext context = null)
		{
			//    RoleDB.DeleteUserRole(IDEmploye, RoleCode)
			Constants.Initialise(ref context);

			int _IDRole;

			IQueryable<EmployeeUser> userId =
				from EmployeeUser eu in context.EmployeeUsers
				where eu.IDUser == _IDUser
				select eu;

			IQueryable<Role> idRole =
				from Role r in context.Roles
				where r.Code == _RoleCode
				select r;

			if (userId.Count() > 0 && idRole.Count() > 0)
			{
				_IDUser = userId.First().IDUser;
				_IDRole = idRole.First().IDRole;

				IQueryable<UserRole> updateUserRoles =
					from UserRole ur in context.UserRoles
					where ur.IDUser == _IDUser && ur.IDRole == _IDRole
					select ur;

				foreach (UserRole ur in updateUserRoles)
				{
					ur.IsActive = false;
				}
			}

			context.SaveChanges();
		}

		public static void DeleteRole(int _IDRole, CSAdminContext context = null)
		{
			//    RoleDB.deleteRole(IDRole)
			Constants.Initialise(ref context);

			IQueryable<ApplicationRole> ApplicationRoles =
				from ApplicationRole ar in context.ApplicationRoles
				where ar.IDRole == _IDRole
				select ar;

			IQueryable<UserRole> UserRoles =
				from UserRole ur in context.UserRoles
				where ur.IDRole == _IDRole
				select ur;

			IQueryable<Role> Roles =
				from Role r in context.Roles
				where r.IDRole == _IDRole
				select r;

			foreach (var ApplicationRole in ApplicationRoles)
			{
				context.ApplicationRoles.Remove(ApplicationRole);
			}

			foreach (var UserRole in UserRoles)
			{
				context.UserRoles.Remove(UserRole);
			}

			foreach (var Role in Roles)
			{
				context.Roles.Remove(Role);
			}

			context.SaveChanges();
		}

		#region Applications

		/// <summary>
		///     Returns a complete list of all Applications
		/// </summary>
		/// <param name="context"></param>
		public static List<Application> SelectAllApplications(CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return context.Applications.ToList();
		}

		/// <summary>
		///     Returns a complete list of all application roles
		/// </summary>
		/// <param name="IDRole"></param>
		/// <param name="context"></param>
		public static List<ApplicationRole> SelectAllApplicationRoles(int IDRole, CSAdminContext context = null)
		{
			Constants.Initialise(ref context);

			IQueryable<Application> Applications =
				from Application a in context.Applications
				orderby a.Description
				select a;

			IQueryable<ApplicationRole> ApplicationRoles =
				from ApplicationRole ar in context.ApplicationRoles
				join a in Applications on ar.IDApplication equals a.IDApplication
				where ar.IDRole == IDRole
				select ar;

			if (ApplicationRoles.Any())
			{
				return ApplicationRoles.ToList();
			}

			return null;
		}

		/// <summary>
		///     Returns a list of roles given an _IDApplication
		/// </summary>
		/// <param name="IDApplication"></param>
		/// <param name="context"></param>
		public static List<Role> SelectRolesByApplication(int _IDApplication, CSAdminContext context = null)
		{
			//    Return RoleDB.SelectRolesByApplication(IDApplication)
			Constants.Initialise(ref context);

			IQueryable<ApplicationRole> applicationRoles =
				from ApplicationRole ar in context.ApplicationRoles
				select ar;

			IQueryable<Role> roles =
				from Role r in context.Roles
				join ar in applicationRoles on r.IDRole equals ar.IDRole
				where ar.IDApplication == _IDApplication
				orderby r.Description
				select r;

			if (roles.Any())
			{
				return roles.ToList();
			}

			return null;
		}

		/// <summary>
		///     Returns a list of roles given an _IDRole
		/// </summary>
		/// <param name="IDRole"></param>
		/// <param name="context"></param>
		public static List<Application> SelectApplicationsByRole(int _IDRole, CSAdminContext context = null)
		{
			//    Return RoleDB.SelectApplicationsByRole(IDRole)
			Constants.Initialise(ref context);

			IQueryable<ApplicationRole> applicationRoles =
				from ApplicationRole ar in context.ApplicationRoles
				select ar;

			IQueryable<Application> applications =
				from Application a in context.Applications
				join ar in applicationRoles on a.IDApplication equals ar.IDApplication
				where ar.IDRole == _IDRole && ar.IsActive
				select a;

			if (applications.Any())
			{
				return applications.ToList();
			}

			return null;
		}

		/// <summary>
		///     Returns a complete list of all roles
		/// </summary>
		/// <param name="context"></param>
		public static List<Role> SelectAllRoles(CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			return context.Roles.ToList();
		}

		/// <summary>
		///     Updates applications given the IDApplication with a new code and description
		/// </summary>
		/// <param name="newApp"></param>
		public static bool UpdateApplication(Application newApp, CSAdminContext context = null)
		{
			//    Return RoleDB.updateApplication(IDApplication, code, description)
			Constants.Initialise(ref context);

			try
			{
				context.Applications.AddOrUpdate(newApp);
				context.SaveChanges();
			}
			catch (SqlException ex)
			{
				if (ex.Number == 2601)
				{
					return false;
				}

				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return true;
		}

		/// <summary>
		///     Adds a new application with the given code and description
		/// </summary>
		/// <param name="app"></param>
		public static bool AddApplication(Application app, CSAdminContext context = null)
		{
			//    Return RoleDB.addApplication(code, description)
			Constants.Initialise(ref context);

			try
			{
				Constants.Initialise(ref context);
				context.Applications.Add(app);
				context.SaveChanges();
			}
			catch (SqlException ex)
			{
				if (ex.Number == 2601)
				{
					return false;
				}

				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return true;
		}

		/// <summary>
		///     Updates a role given the IDRole with a new code and description
		/// </summary>
		/// <param name="newRole"></param>
		public static bool UpdateRole(Role newRole, CSAdminContext context = null)
		{
			//    Return RoleDB.updateRole(IDRole, code, description)
			Constants.Initialise(ref context);

			try
			{
				context.Roles.AddOrUpdate(newRole);
				context.SaveChanges();
			}
			catch (SqlException ex)
			{
				if (ex.Number == 2601)
				{
					return false;
				}

				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return true;
		}

		/// <summary>
		///     Updates an Application Role given the IDApplicationRole and sets the isActive property
		/// </summary>
		/// <param name="IDApplicationRole"></param>
		/// <param name="isActive"></param>
		/// <param name="context"></param>
		public static void UpdateApplicationRole(int IDApplicationRole, bool isActive, CSAdminContext context = null)
		{
			//    RoleDB.updateApplicationRole(IDApplicationRole, isActive)
			Constants.Initialise(ref context);

			IQueryable<ApplicationRole> applicationRoles =
				from ApplicationRole ar in context.ApplicationRoles
				where ar.IDApplicationRole == IDApplicationRole
				select ar;

			foreach (ApplicationRole applicationRole in applicationRoles)
			{
				applicationRole.IsActive = isActive;
			}

			context.SaveChanges();
		}

		/// <summary>
		///     Adds a new role with the code and description
		/// </summary>
		/// <param name="_role"></param>
		public static bool AddRole(Role _role, CSAdminContext context = null)
		{
			//    Return RoleDB.addRole(code, description)
			Constants.Initialise(ref context);

			try
			{
				context.Roles.Add(_role);
				context.SaveChanges();
			}
			catch (SqlException ex)
			{
				if (ex.Number == 2601)
				{
					return false;
				}

				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return true;
		}

		/// <summary>
		///     Adds a new application role with the given IDApplication and IDRole, and sets the IsActive property to true
		/// </summary>
		/// <param name="IDApplication"></param>
		/// <param name="IDRole"></param>
		/// <param name="context"></param>
		public static void AddApplicationRole(int IDApplication, int IDRole, CSAdminContext context = null)
		{
			//    RoleDB.addApplicationRole(IDApplication, IDRole)
			Constants.Initialise(ref context);

			ApplicationRole applicationRole = new ApplicationRole
			{
				IDApplication = IDApplication,
				IDRole = IDRole,
				IsActive = true
			};

			context.ApplicationRoles.Add(applicationRole);
			context.SaveChanges();
		}

		/// <summary>
		///     Removes an application role given the IDApplicationRole
		/// </summary>
		/// <param name="IDApplicationRole"></param>
		/// <param name="context"></param>
		public static void DeleteApplicationRole(int IDApplicationRole, CSAdminContext context = null)
		{
			//    RoleDB.deleteApplicationRole(IDApplicationRole)
			Constants.Initialise(ref context);

			IQueryable<ApplicationRole> applicationRoles =
				from ApplicationRole ar in context.ApplicationRoles
				where ar.IDApplicationRole == IDApplicationRole
				select ar;

			foreach (var applicationRole in applicationRoles)
			{
				context.ApplicationRoles.Remove(applicationRole);
			}

			context.SaveChanges();
		}

		/// <summary>
		///     Removes an application given the IDApplication
		/// </summary>
		/// <param name="IDApplication"></param>
		/// <param name="context"></param>
		public static void DeleteApplication(int IDApplication, CSAdminContext context = null)
		{
			//    RoleDB.deleteApplication(IDApplication)
			Constants.Initialise(ref context);

			IQueryable<ApplicationRole> applicationRoles =
				from ApplicationRole ar in context.ApplicationRoles
				where ar.IDApplication == IDApplication
				select ar;

			foreach (var ApplicationRole in applicationRoles)
			{
				context.ApplicationRoles.Remove(ApplicationRole);
			}

			IQueryable<Application> applications =
				from Application a in context.Applications
				where a.IDApplication == IDApplication
				select a;

			foreach (var Application in applications)
			{
				context.Applications.Remove(Application);
			}

			context.SaveChanges();
		}

		#endregion

		/// <summary>
		///     Updates a userName given the IDUser and sets the username to the Username parameter
		/// </summary>
		/// <param name="IDUser"></param>
		/// <param name="UserName"></param>
		/// <param name="context"></param>
		public static void UpdateUsername(int IDUser, string Username, CSAdminContext context = null)
		{
			//UserManager.UpdateUsername(IDUser, Username, Username, true);
			Constants.Initialise(ref context);
			UserManager.UpdateUsername(IDUser, Username);
		}
	}
}