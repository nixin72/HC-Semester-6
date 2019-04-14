using System.Collections.Generic;
using System.Linq;
using CSAdmin2.Logic;
using CSAdmin2.Model;

namespace CSAdminMVC.Views.ViewModels
{
	public class ApplicationRolesVM
	{
		public ApplicationRolesVM(CSAdminContext context = null)
		{
			Constants.Initialise(ref context);
			// don't let those things create 2 different connections at the same time
			Roles = RoleManager.SelectAllRoles(context);
			Roles = Roles.OrderBy(s => s.Description);
			
            var AppList = RoleManager.SelectAllApplications(context).ToList();
            for (int i=0; i< AppList.Count; i++) {
                AppList[i].ApplicationRoles = AppList[i].ApplicationRoles.OrderBy(x => x.Role.Description).ToList();
            }
            
            Applications = AppList.OrderBy(s => s.Description);
        }

		public IEnumerable<Application> Applications { get; set; }
		public IEnumerable<Role> Roles { get; set; }
	}
}