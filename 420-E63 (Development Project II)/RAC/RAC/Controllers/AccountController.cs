using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RAC.Models;
using RAC.RACModels;
using Newtonsoft.Json.Linq;
using System.Net;
using RAC.BusinessLogic;
using System.Collections.Generic;
using System.Web.Security;
using RAC.CSAdminModel;
using System.Data.Entity.Core.Objects;


namespace RAC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
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
            
            RACModels.User user = CandidateBLL.GetUser(model.Email);
            if (user != null)
            {
                
                ApplicationUser membershipUser = UserManager.FindByEmail(model.Email);
                if (await UserManager.IsEmailConfirmedAsync(membershipUser.Id))
                {
                    // Candidate sign in
                    SignInStatus result = await SignInManager.PasswordSignInAsync(membershipUser.UserName, model.Password, model.RememberMe, false);
                    switch (result)
                    {
                        case SignInStatus.Success:


                            if ((userType)user.UserType == userType.Candidate)
                            {
                                Candidate can = CandidateBLL.GetCandidateById(user.Id);
                                if (!can.IsDeleted)
                                {
                                    var candidateUser = (Candidate)user;
                                    candidateUser.RACRequest = RACRequestBLL.GetRACByCandidate(candidateUser.Id);

                                    Session.Add("User", candidateUser);
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
                            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, model.RememberMe });
                        default:
                            ModelState.AddModelError("", "Invalid login attempt.");
                            return View(model);
                    }
                }
                
                // email not confirmed
                ModelState.AddModelError("", "Your email has not yet been confirmed.");
                return View(model);
                
                

            }
            else if (Membership.ValidateUser(model.Email, model.Password))
            {

                var csAdmin = new CSAdminUsers();
                ObjectResult<uspLogin_Result> result = csAdmin.uspLogin(model.Email, "RAC");

                if (!result.GetEnumerator().MoveNext())
                {
                    ModelState.AddModelError(string.Empty, "The user name provided does not have access to this system.");
                }

                var auth = new App_Start.AuthorizeADAttribute { Groups = "RAC Advisor" };
                bool isRacAdvisor = auth.isUserInRole(model.Email);
                if (csAdmin.Users.Any(c => c.Username == model.Email))
                {
                    CSAdminModel.User racAdvisor = csAdmin.Users.First(c => c.Username.Equals(model.Email));
                    if (isRacAdvisor)
                    {
                        var theRacAdvisor = new RACModels.User { FirstName = racAdvisor.FirstName, LastName = racAdvisor.LastName, UserType = (int)userType.RACAdvisor };
                        Session.Add("User", theRacAdvisor);
                        Session.Timeout = 60;
                        FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                        return RedirectToAction("RacAdvisorHome", "RACAdvisor");
                    }
                    
                    ModelState.AddModelError(string.Empty, "The user name provided does not have access to this system.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The email address or password entered is invalid.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "The email address or password entered is invalid.");
            }

            
            return View(model);


        }
         
        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

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
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
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
            var status = (bool)obj.SelectToken("success");
            ViewBag.CaptchaMessage = "";
            var newCandidate = new Candidate();

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
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                    // make username unique
                    int r = (new Random()).Next(100, 1000);
                    user.UserName = val.FirstName + val.LastName + r.ToString();
                    IdentityResult res = await UserManager.CreateAsync(user, model.Password);
                    if (res.Succeeded)
                    {
                        // await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account",
                           new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        
                        Notification.sendEmailWithMessage(model.Email, "Please confirm your account by clicking <a href='" +
                            callbackUrl + "\">here</a>", "Confirm Your Account");

                        
                        newCandidate = new Candidate
                        {
                            FirstName = val.FirstName,
                            LastName = val.LastName,
                            City = val.City,
                            Country = val.Country,
                            DateRegistered = DateTime.Now,
                            Email = val.Email,
                            HomePhone = val.HomePhone,
                            WorkPhone = val.WorkPhone,
                            Province = val.Province,
                            UserType = (int)userType.Candidate,
                            Street = val.Street,
                            PreferredMethodOfContact = (int)val.PreferredMethodOfContact,
                            RegistrationIP = ipAddress,
                            IsArchived = false,
                            IsDeleted = false,
                            IsPrivacyPolicyAccepted = true
                        };

                        string url = Request.Url.AbsoluteUri;
                        List<ERRORS> registrationErrors = CandidateBLL.Register(newCandidate, val.ProgramId,
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
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded) {
                Notification.CreateNotification(CandidateBLL.getCandidateIdByEmail(UserManager.GetEmail(userId)), Messages.NEW_RAC_STARTED);
            }
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
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
                var user = UserManager.FindByEmail(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }



                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, Request.Url.Scheme);
                try
                {
                    await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                }
                catch
                {
                    Notification.saveEmailToFile(" TO: " + model.Email + "\r\n From: POSTMASTER \r\n Please reset your password by clicking < a href =\"" +
                        callbackUrl + "\">here</a>\r\n ---------------------------------------");
                }

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }
            else
            {
                return code == null ? View("Error") : View();
            }
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
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                AddErrors(result);
                return View();
            }
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }
            else
            {
                return View();
            }
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
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
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Candidate");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser {
                    UserName = model.Email,
                    Email = model.Email
                };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
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

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XSRF_KEY = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
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
                : this(provider, redirectUri, null)
            {
            }

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
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
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