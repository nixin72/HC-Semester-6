using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CSAdminMVC.App_Code
{
	public class CSAdminAuthorizeAttribute : AuthorizeAttribute
	{
		private bool _authenticated;

		//Probably going to use later
		private bool _authorized;

		/// <summary>
		///     Specifies how to handle unauthorized requests.
		/// </summary>
		/// <param name="filterContext"></param>
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			base.HandleUnauthorizedRequest(filterContext);

			filterContext.Result = new RedirectResult("/Login/Index");
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			_authenticated = base.AuthorizeCore(httpContext);
			FormsAuthentication.SetAuthCookie(httpContext.User.Identity.Name, false);
			return _authenticated;
		}
	}
}