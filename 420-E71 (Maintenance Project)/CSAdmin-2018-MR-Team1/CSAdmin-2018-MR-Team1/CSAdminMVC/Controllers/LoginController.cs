using System;
using System.Web.Mvc;
using System.Web.Security;
using CSAdmin2.Model;
using CSAdminMVC.Models;

namespace CSAdminMVC.Controllers
{
	public class LoginController : Controller
	{
		[AllowAnonymous]
		// GET: Login
		public ActionResult Index(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			ViewBag.auth = User.Identity.IsAuthenticated;


			return View();
		}

		// POST: Login
		[AllowAnonymous]
		[HttpPost]
		public ActionResult Index(LoginModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				User u = new LoginApiController().Login(model.Username, model.password, "CSA");
				FormsAuthentication.SetAuthCookie(model.Username, false);
			}
			catch (MemberAccessException e) //Authentication error
			{
				ViewBag.LoginFailed = true;
				return View(model);
			}
			catch (UnauthorizedAccessException e) //Authorization error
			{
				ViewBag.LoginFailed = true;
				return View(model);
			}

			return RedirectToAction("Index", "Home");

//            try
//            {
//#if !DEBUG
//                if (Membership.ValidateUser(model.Username, model.password))
//                {
//                    var user = Membership.GetUser(model.Username);
//#endif
//                    var context = new CSAdminContext();
//#if !DEBUG
//                    if (user != null)
//                    {
//#endif
//                        var csuser = Auth.Login(model.Username, "CSA", context);
//                        if (csuser != null)
//                        {
//                            FormsAuthentication.SetAuthCookie(model.Username, false);
//                        }
//#if !DEBUG
//                    }
//                }
//                else
//                {
//                    ViewBag.LoginFailed = true;
//                    return View(model);
//                }
//#endif
//            }
//            catch (Exception e)
//            {
//                ViewBag.LoginFailed = true;
//                return View(model);
//            }
//            return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public ActionResult Logout()
		{
			Session.Clear();
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Login");
		}
	}
}