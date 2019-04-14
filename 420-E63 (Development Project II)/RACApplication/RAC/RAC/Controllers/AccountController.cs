using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using RAC.BusinessLogic;
using RAC.CSAdminModel;
using RAC.Models;
using RAC.RACModels;

namespace RAC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public ApplicationSignInManager _signInManager;
        public ApplicationUserManager _userManager;

        public AccountController() { }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        //
        // GET: /Account/Login
        [PageHistory]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser membershipUser = UserManager.FindByEmail(model.Email);

            if (membershipUser != null)
            {

            if (await UserManager.IsEmailConfirmedAsync(membershipUser.Id))
            {
                RACUser user = CandidateBLL.MapUserInformation(membershipUser);

                // Candidate sign in
                SignInStatus result = await SignInManager.PasswordSignInAsync(membershipUser.UserName, model.Password,
                    model.RememberMe, false);
                switch (result)
                {
                    case SignInStatus.Success:

                        if ((userType) user.UserType == userType.Candidate)
                        {
                            if (!user.IsDeleted)
                            {
                                Session.Add("User", user);
                                Session.Timeout = 60;
                                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                                var authTicket = new FormsAuthenticationTicket(1, model.Email, DateTime.Now,
                                    DateTime.Now.AddHours(8), false, user.UserType.ToString());
                                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                                HttpContext.Response.Cookies.Add(authCookie);
                                return RedirectToAction("Home", "Candidate");
                            }

                            // account is deleted
                            ModelState.AddModelError("", "Invalid login attempt.");
                            return View(model);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid login attempt.");
                            return View(model);
                        }

                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new {ReturnUrl = returnUrl, model.RememberMe});
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }
                // email not confirmed
                ModelState.AddModelError("", "Your email has not yet been confirmed.");
                return View(model);
            }


            if (WebConfigurationManager.AppSettings.Get("localDebug") == "true" && model.Email.ToLower() == "userad" &&
                model.Password == "userad")
            {
                var theRacAdvisor = new RACUser()
                {
                    FirstName = "Local",
                    LastName = "Test",
                    UserType = (int)userType.RACAdvisor
                };
                Session.Add("User", theRacAdvisor);
                Session.Timeout = 60;
                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }

            if (Membership.ValidateUser(model.Email, model.Password))
            {
                var csAdmin = new CSAdminUsers();
                ObjectResult<uspLogin_Result> result = csAdmin.uspLogin(model.Email, "RAC");

                if (!result.GetEnumerator().MoveNext())
                {
                    ModelState.AddModelError(string.Empty,
                        "The user name provided does not have access to this system.");
                }

                var auth = new AuthorizeADAttribute {Groups = "RAC Advisor"};
                bool isRacAdvisor = auth.IsUserInRole(model.Email);
                if (csAdmin.Users.Any(c => c.Username == model.Email))
                {
                    User racAdvisor = csAdmin.Users.First(c => c.Username.Equals(model.Email));
                    if (isRacAdvisor)
                    {
                        var theRacAdvisor = new RACUser()
                        {
                            FirstName = racAdvisor.FirstName,
                            LastName = racAdvisor.LastName,
                            UserType = (int) userType.RACAdvisor
                        };
                        Session.Add("User", theRacAdvisor);
                        Session.Timeout = 60;
                        FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                        return RedirectToAction("RacAdvisorHome", "RACAdvisor");
                    }

                    ModelState.AddModelError(string.Empty,
                        "The user name provided does not have access to this system.");
                }

                ModelState.AddModelError(string.Empty, "The email address or password entered is invalid.");
            }

            ModelState.AddModelError(string.Empty, "The email address or password entered is invalid.");

            return View(model);
        }

        //
        // GET: /Account/VerifyCode
        [PageHistory]
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }

            return View(new VerifyCodeViewModel {Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe});
        }

        [PageHistory]
        [AllowAnonymous]
        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            SignInStatus result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe,
                model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [PageHistory]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            /*
             * The method will take in the candidate's information, including the CAPTCHA on the register page. It will check to make sure the CAPTCHA
             * was checked off, that the information entered was valid. If it is valid, then the user will be redirected to the proper page. If not, the page
             * is redisplayed with proper information.  
             *
             * See also:
             * None
             *
             * Parameters:
             * CandidateVal val = Candidate information following the validation model. 
             * 
             * Returns:
             * An ActionResult that sends the user to the proper page.
             * 
             */

            string response = Request["g-recaptcha-response"];
            var secretKey = "6Ld1_EEUAAAAAJDtQJlXEEW0AZU5mdEprthIox74";
            var client = new WebClient();
            string result = client.DownloadString(string.Format(
                "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            JObject obj = JObject.Parse(result);
            var status = (bool) obj.SelectToken("success");
            ViewBag.CaptchaMessage = "";

            //Logging the IP Address
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            RegisterViewModel val = model;

            if (status)
            {
                if (ModelState.IsValid)
                {
                    //EF create user
                    var user = new ApplicationUser
                    {
                        UserName = val.Email,
                        Email = val.Email,
                        Street = val.Street,
                        City = val.City,
                        Province = val.Province,
                        Country = val.Country,
                        PreferredMethodOfContact = (int) val.PreferredMethodOfContact,
                        IsDeleted = false,
                        IsArchived = false,
                        IsPrivacyPolicyAccepted = true,
                        DateRegistered = DateTime.Now,
                        RegistrationIP = ipAddress,
                        //RACRequest_Id = val. ,
                        FirstName = val.FirstName.ElementAt(0).ToString().ToUpper() + val.FirstName.Substring(1).ToLower(),
                        LastName = val.LastName.ElementAt(0).ToString().ToUpper() + val.LastName.Substring(1).ToLower(),
                        HomePhone = val.HomePhone,
                        WorkPhone = val.WorkPhone,
                        UserType = (int) userType.Candidate
                    };

                    // make username unique
                    int r = new Random().Next(100, 1000);
                    user.UserName = val.FirstName + val.LastName + r;
                    IdentityResult res = UserManager.Create(user, model.Password);
                    if (res.Succeeded)
                    {
                        // await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        string callbackUrl = Url.Action("ConfirmEmail", "Account",
                            // ReSharper disable once PossibleNullReferenceException
                            new {userId = user.Id, code}, Request.Url.Scheme);

                        Notification.sendEmailWithMessage(model.Email,
                            "Please confirm your account by clicking <a href=\"" +
                            callbackUrl + "\">here</a>", "Confirm Your Account");

                        var newCandidate = new RACUser()
                        {
                            CSAdminId = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            HomePhone = user.HomePhone,
                            WorkPhone = user.WorkPhone,
                            City = user.City,
                            Street = user.Street,
                            Province = user.Province,
                            Country = user.Country,
                            IsDeleted = user.IsDeleted,
                            IsArchived = user.IsArchived,
                            IsPrivacyPolicyAccepted = user.IsPrivacyPolicyAccepted,
                            DateRegistered = user.DateRegistered,
                            RegistrationIP = user.RegistrationIP,
                            UserType = (int)userType.Candidate,
                            PreferredMethodOfContact = user.PreferredMethodOfContact
                        };

                        string url = Request.Url.AbsoluteUri;
                        List<ERRORS> registrationErrors = CandidateBLL.Register(newCandidate, val.ProgramId,
                            // ReSharper disable once StringIndexOfIsCultureSpecific.1
                            url.Substring(0, url.IndexOf("Account")));

                        if (registrationErrors.Count() != 0 && registrationErrors[0] == ERRORS.SUCCESS)
                        {
                            return RedirectToAction("Login", "Account");
                        }

                        return View(val);
                    }

                    AddErrors(res);
                }
            }
            else
            {
                ViewBag.CaptchaMessage = "Please verify that you are human";
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [PageHistory]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                Notification.CreateNotification(CandidateBLL.getCandidateIdByEmail(UserManager.GetEmail(userId)),
                    Messages.NEW_RAC_STARTED, (int)userType.RACAdvisor);
            }

            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [PageHistory]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindByEmail(model.Email);
                if (user == null || !await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // ReSharper disable once PossibleNullReferenceException
                string callbackUrl = Url.Action("ResetPassword", "Account", new {userId = user.Id, code},
                    Request.Url.Scheme);
                try
                {
                    await UserManager.SendEmailAsync(user.Id, "Reset Password",
                        "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                }
                catch
                {
                    Notification.saveEmailToFile(" TO: " + model.Email +
                                                 "\r\n From: POSTMASTER \r\n Please reset your password by clicking < a href =\"" +
                                                 callbackUrl +
                                                 "\">here</a>\r\n ---------------------------------------");
                }

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [PageHistory]
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [PageHistory]
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }

            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [PageHistory]
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }

            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider,
                Url.Action("ExternalLoginCallback", "Account", new {ReturnUrl = returnUrl}));
        }

        //
        // GET: /Account/SendCode
        [PageHistory]
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            string userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }

            IList<string> userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            List<SelectListItem> factorOptions =
                userFactors.Select(purpose => new SelectListItem {Text = purpose, Value = purpose}).ToList();
            return View(new SendCodeViewModel
            {
                Providers = factorOptions,
                ReturnUrl = returnUrl,
                RememberMe = rememberMe
            });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }

            return RedirectToAction("VerifyCode",
                new {Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe});
        }

        //
        // GET: /Account/ExternalLoginCallback
        [PageHistory]
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            SignInStatus result = await SignInManager.ExternalSignInAsync(loginInfo, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new {ReturnUrl = returnUrl, RememberMe = false});
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation",
                        new ExternalLoginConfirmationViewModel {Email = loginInfo.Email});
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model,
            string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Candidate");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                ExternalLoginInfo info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, false, false);
                        return RedirectToLocal(returnUrl);
                    }
                }

                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Clear();
            Session["Candidate"] = null;
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        public bool CheckIsRACAdvisor()
        {
            return RACAdvisorBLL.CheckIsRACAdvisor();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        [PageHistory]
        [AllowAnonymous]
        public ActionResult ResendConfirmationEmail()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<String> ResendConfirmationEmail(String email)
        {
            try
            {
                ApplicationUser user = UserManager.FindByEmailAsync(email).Result;
                if (user.EmailConfirmed)
                {
                    return "-2";
                }
                string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "Account",
                    // ReSharper disable once PossibleNullReferenceException
                    new {userId = user.Id, code}, protocol: Request.Url.Scheme);

                Notification.sendEmailWithMessage(email, "Please confirm your account by clicking <a href=\"" +
                                                         callbackUrl + "\">here</a>", "Confirm Your Account");
            }
            catch (Exception)
            {
                return "-1";
            }

            return "1";
        }

        [HttpPost]
        [AllowAnonymous]
        public String IsExistingEmail(String email)
        {
            String value = "false";
            if (CandidateBLL.IsExistingEmail(email))
            {
                value = "true";
            }

            return value;
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XSRF_KEY = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null) { }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
                if (UserId != null)
                {
                    properties.Dictionary[XSRF_KEY] = UserId;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}