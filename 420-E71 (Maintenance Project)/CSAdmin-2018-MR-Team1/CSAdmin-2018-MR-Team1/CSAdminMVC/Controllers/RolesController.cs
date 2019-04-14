using System.Collections.Generic;
using System.Web.Mvc;
using CSAdmin2.Logic;
using CSAdmin2.Model;
using CSAdminMVC.App_Code;
using CSAdminMVC.Views.ViewModels;

namespace CSAdminMVC.Controllers
{
	[CSAdminAuthorize]
	public class RolesController : Controller
	{
		public void EditApplication(Application app)
		{
			RoleManager.UpdateApplication(app);
		}

		public ActionResult DeleteApplication(short id)
		{
			RoleManager.DeleteApplication(id);
			return Redirect("../ApplicationRoles");
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public void AddApplication(Application app)
		{
			RoleManager.AddApplication(app);
		}

		public void EditRole(Role role)
		{
			RoleManager.UpdateRole(role);
		}

		public ActionResult DeleteRole(short id)
		{
			RoleManager.DeleteRole(id);
			return Redirect("../ApplicationRoles");
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public void AddRole(Role role)
		{
			RoleManager.AddRole(role);
		}

		public ActionResult ApplicationRoles()
		{
			return View(new ApplicationRolesVM());
		}

		//----------------------User Roles methods--------------------------
		public ActionResult ManageUserRoles()
		{
			return View(new UserRolesVM());
		}

		public bool isRoleCodeUsed(string roleId)
		{
			bool returned = ApplicationRolesManager.isRoleCodeUsed(roleId);
			return returned;
		}

		public bool isApplicationCodeUsed(string appId)
		{
			bool returned = ApplicationRolesManager.isApplicationCodeUsed(appId);
			return returned;
		}

        
    
		[HttpGet]
		public PartialViewResult ViewUserRoles(int id)
		{
			return PartialView(new UserRoleList(id));
		}

		[HttpGet]
		public PartialViewResult ViewUserRolesAnd(string id)
		{
			string[] ids = id.Split('-');
			IEnumerable<UserRole> UserRL = UserRoleManager.GetAndUserRoles(ids);
			return PartialView(new UserRoleList(UserRL));
		}
		[HttpPost]
		public void AddUserRole(int userID, int roleID)
		{
			RoleManager.InsertUserRole(userID, roleID);
		}

		[HttpGet]
		public PartialViewResult EditUserRole(int userRoleID)
		{
			return PartialView(UserRoleManager.getUserRole(userRoleID));
		}

		[HttpPost]
		public void SaveUserRole(int UserId, string newUserName)
		{
			UserManager.UpdateUsername(UserId, newUserName);
			//UserManager.UpdateUsername(UserId, newUserName, newUserName, true);
		}

		[HttpPost]
		public void deactivateUserRole(string roleID, int userID)
		{
			RoleManager.DeleteUserRole(userID, roleID);
		}

		[HttpGet]
		public PartialViewResult deleteUserRole(int userRoleID)
		{
			return PartialView(UserRoleManager.getUserRole(userRoleID));
		}

        [HttpPost]
        public void DeleteApplicationRole(int IDApplicationRole)
        {
            RoleManager.DeleteApplicationRole(IDApplicationRole);
        }

        [HttpPost]
        public void UpdateApplicationRole(int IDApplicationRole, bool isActive)
        {
            RoleManager.UpdateApplicationRole(IDApplicationRole, isActive);
        }

        [HttpPost]
        public void AddApplicationRole(int appId, int roleId)
        {
            RoleManager.AddApplicationRole(appId, roleId);
        }

    }
}