using RAC.CSAdminModel;
using RAC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace RAC.App_Start
{
    /* This custom class retrieves a user's role, and see if it matches one of the roles allowed to access the page*/
    //http://www.benramey.com/2014/10/20/active-directory-authentication-in-asp-net-mvc-5-with-forms-authentication-and-group-based-authorization/
    //http://stackoverflow.com/questions/4342271/asp-net-mvc-forms-authorization-with-active-directory-groups
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

                return isUserInRole(username);
            }

            _authorized = false;
            return _authorized;
        }

        //for authorization
        public bool isUserInRole(String username)
        {
            bool _authorized;
            try
            {
                //Refer to the database to get the user's role
                CSAdminUsers CSAdmin = new CSAdminUsers();
                ObjectResult<uspLogin_Result> result = CSAdmin.uspLogin(username, "RAC");
                //See if that role is included in the list of roles, separated into an array of role names for permitted users
                var groups = Groups.Split(',');
                _authorized = groups.Contains(result.First().Role);
                return _authorized;
            }
            catch (Exception ex)
            {
                try
                {
                    using (StreamWriter file = new StreamWriter(HttpContext.Current.Server.MapPath("err.log"), true))
                    {
                        file.WriteLine(ex.ToString());
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Error could not be logged: " + e.ToString());
                }

                _authorized = false;
                return _authorized;
            }
        }

        //to get user's full name in the format firstname lastname
        public String getUserFullName(String username)
        {
            try
            {
                //Refer to the database to get the user's role
                CSAdminUsers CSAdmin = new CSAdminUsers();
                RAC.CSAdminModel.User user = CSAdmin.Users.Where(u => u.Username == username).First();
                return user.FirstName + " " + user.LastName;
            }
            catch (Exception ex)
            {
                try
                {
                    using (StreamWriter file = new StreamWriter(HttpContext.Current.Server.MapPath("err.log"), true))
                    {
                        file.WriteLine(ex.ToString());
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Error could not be logged: " + e.ToString());
                }

                return "";
            }
        }
    }
}