using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web.Http;
using System.Web.Security;
using CSAdmin2.Model;
using System.Configuration;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using CSAdminMVC.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;


namespace CSAdminMVC.Controllers
{
    [System.Web.Http.Route("api/[controller]")]
    public class LoginApiController : ApiController
    {
        public ApplicationSignInManager _signInManager;
        public ApplicationUserManager _userManager;

        public LoginApiController() { }

        public LoginApiController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        [System.Web.Http.HttpPost]
        public User Login(string username, string password, string appCode)
        {
            //Checks local users(?) and Clara
            if (Membership.ValidateUser(username, password))
            {
                var MemUser = Membership.GetUser(username);
                var auth = Authorize(MemUser.UserName, appCode);

                if (MemUser != null && auth != null)
                {
                    return auth;
                }

                throw new UnauthorizedAccessException("This user does not have access to the system.");
            }
            else if (IsADAuthenticated(username, password))
            {
                var auth = Authorize(username, appCode);
                if (auth != null)
                {
                    return auth;
                }

                throw new UnauthorizedAccessException("this user does not have access to the system.");
            }

            throw new MemberAccessException("The username or password is incorrect.");
        }

        [System.Web.Http.HttpPost] //TODO
        public ApplicationUser Register(RegisterViewModel user, string registationIp, string appCode)
        {
            ApplicationUser newUser = user.ToUser();
            newUser.IsDeleted = false;
            newUser.IsPrivacyPolicyAccepted = true;
            newUser.DateRegistered = DateTime.Now;
            newUser.UserName = user.FirstName + user.LastName + (new Random().Next(100, 1000));

            IdentityResult res = UserManager.Create(user.ToUser(), user.Password);
            if (res.Succeeded)
            {
                string code = UserManager.GenerateEmailConfirmationToken(newUser.Id);
                string callbackUrl = new UrlHelper().Action("ConfirmEmail", "Account",
                    // ReSharper disable once PossibleNullReferenceException
                    new { userId = newUser.Id, code }, Request.RequestUri.Scheme); 

                Notification.sendEmailWithMessage(newUser.Email,
                    "Please confirm your account by clicking <a href=\"" +
                    callbackUrl + "\">here</a>", "Confirm Your Account");
            }

            return newUser;
        }

        [System.Web.Http.HttpPost]
        public bool ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return false;
            }

            IdentityResult result = UserManager.ConfirmEmail(userId, code);
            return result.Succeeded;
        }

        private User Authorize(string username, string appCode, CSAdminContext context = null)
        {
            try
            {
                CSAdmin2.Logic.Constants.Initialise(ref context);
            }
            catch (Exception e)
            {
                context = new CSAdminContext();
            }

            return
                (from User u in context.Users
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
                     a.Code == appCode &&
                     u.Username == username
                 select u)
                .Distinct().FirstOrDefault();
        }

        public bool IsADAuthenticated(string username, string password)
        {
            using (var adCon = new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings.Get("LDAPServer")))
            {
                return adCon.ValidateCredentials(username, password);
            }
        }
    }
}
