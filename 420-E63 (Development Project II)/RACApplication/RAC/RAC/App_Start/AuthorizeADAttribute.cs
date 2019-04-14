using System;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RAC.CSAdminModel;

namespace RAC
{
    /* This custom class retrieves a user's role, and see if it matches one of the roles allowed to access the page*/
    //http://www.benramey.com/2014/10/20/active-directory-authentication-in-asp-net-mvc-5-with-forms-authentication-and-group-based-authorization/
    //http://stackoverflow.com/questions/4342271/asp-net-mvc-forms-authorization-with-active-directory-groups
    // ReSharper disable once InconsistentNaming
    public class AuthorizeADAttribute : AuthorizeAttribute
    {
        private bool _authenticated;
        private bool _authorized;

        //Refers to the names of roles in Active Directory that can log into that portion of the site
        public string Groups { get; set; }

        //How to handle a request for an unauthorized page
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            if (_authenticated && !_authorized)
            {
                filterContext.Result = new RedirectResult("/error/notauthorized");
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            _authenticated = base.AuthorizeCore(httpContext);

            if (_authenticated)
            {
                //If the "Groups" parameter is empty, allow access to all users
                if (string.IsNullOrEmpty(Groups))
                {
                    _authorized = true;
                    return _authorized;
                }

                //Otherwise, check to see if user is in the role specified
                string username = httpContext.User.Identity.Name;

                return IsUserInRole(username);
            }

            _authorized = false;
            return _authorized;
        }

        //for authorization
        public bool IsUserInRole(string username)
        {
            try
            {
                //Refer to the database to get the user's role
                var csAdmin = new CSAdminUsers();
                ObjectResult<uspLogin_Result> result = csAdmin.uspLogin(username, "RAC");
                //See if that role is included in the list of roles, separated into an array of role names for permitted users
                string[] groups = Groups.Split(',');
                bool authorized = groups.Contains(result.First().Role);
                return authorized;
            }
            catch (Exception ex)
            {
                try
                {
                    using (var file = new StreamWriter(HttpContext.Current.Server.MapPath("err.log"), true))
                    {
                        file.WriteLine(ex.ToString());
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(@"Error could not be logged: " + e);
                }

                return false;
            }
        }
    }
}