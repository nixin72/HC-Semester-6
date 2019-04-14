using System.Web.Mvc;
using RAC.BusinessLogic;
using RAC.Models;
using RAC.RACModels;
using WebGrease.Css.Extensions;
using System;
using System.Web.Security;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace RAC.Controllers
{
    public enum ManageMessageId
    {
        AddPhoneSuccess,
        ChangePasswordSuccess,
        SetTwoFactorSuccess,
        SetPasswordSuccess,
        RemoveLoginSuccess,
        RemovePhoneSuccess,
        Error
    }

    public class CandidateController : Controller
    {
        // JSON_BOOLEAN_[TRUE, FALSE] represent a boolean value that would be given back to an AJAX request.
        private const string JSON_BOOLEAN_TRUE = "{ \"isSuccess\": true }";
        private const string JSON_BOOLEAN_FALSE = "{ \"isSuccess\": false}";
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        // GET: Candidate
        public ActionResult Fallback()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SubmitRACRequest(Candidate user)
        {
            /*
             * The method will extract the answers provided by the candidate and save the information. The RAC request will be submitted and be read only 
             * the next time the page is loaded.
             *
             * See also:
             * SaveRACRequest()
             * ChangeRACStatusSubmitted()
             *
             * Parameters:
             * string selfEvaluation = A JSON string representing the self evaluation's information that was entered by the candidate.
             * 
             * Returns:
             * An ActionResult that sends the user to the proper page
             * 
             */

            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return Json(JSON_BOOLEAN_FALSE);
            }

            if (RACRequestBLL.IsManditoryCommentsFilled(user) &&
                RACRequestBLL.IsAllAnswersFilled(user) &&
                UploadedDocumentBll.IsAtLeastOneFileUploaded(user))
            {
                user.RACRequest = RACRequestBLL.SubmitRACRequest(user);
                //Getting the updated user session afterwards
                Session["User"] = user;
                return Json(JSON_BOOLEAN_TRUE);
            }

            return Json(JSON_BOOLEAN_FALSE);
        }

        public ActionResult Home()
        {

            if (Session["User"] != null)
            {
                User user = (User) Session["User"];
                if (user.UserType == (int)userType.Candidate)
                {
                    var c = new CandidateHomeViewModel();
                    c = CandidateBLL.MapToHomeViewModel(user, c);
                    c.Notifications = CandidateBLL.GetCandidateNotifications(user.Id);
                    return View(c);
                }

                if (user.UserType == (int)userType.RACAdvisor)
                {
                    return RedirectToAction("RacAdvisorHome", "RACAdvisor");
                }
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public PartialViewResult DeleteNotification(int id)
        {
            var n = new Notification();
            Notification.DeleteNotification(id);
            var user = (User)Session["User"];
            var c= new CandidateHomeViewModel();
            c = CandidateBLL.MapToHomeViewModel(user, c);
            c.Notifications = CandidateBLL.GetCandidateNotifications(user.Id);
            return PartialView("Notification", c);
        }

        public ActionResult Index()
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }
            //default return home
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult GetCompleteRacRequest(int id)
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                Candidate searchCan = CandidateBLL.GetCandidateById(id);
                Session["Candidate"] = searchCan;
                return View(searchCan);
            }

            Candidate can = CandidateBLL.GetCandidateByUserType();
            if (can == null)
            {
                LogOff();
                return RedirectToAction("Index", "Home");
            }
            if (can.Id == id)
            {
                return View(can);
            }

            LogOff();
            return RedirectToAction("Index","Home");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public void HomeAutosave(Candidate user)
        {
            /*
             * The method is going to take in a candidate object from the user, it is going to update their RAC Request inside the User object.
             * This is used for the autosave functionality of the form
                            * See also:
             * SaveRACRequest()
             *              * Parameters:
             * string user = Candidate object that is passed from the form back to the controller
             * 
             * Returns:
             * void
             * 
             */

            user.RACRequest = RACRequestBLL.SaveRACRequest(user);
        }
       
        public ActionResult SelfEvaluation()
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }
            try
            {
                Candidate can = CandidateBLL.GetCandidateByUserType();
                if (can == null)
                {
                    LogOff();
                    return RedirectToAction("Index", "Home");
                }
                if (can.RACRequest.Status == 1 || can.IsArchived)
                {
                    return RedirectToAction("GetCompleteRacRequest", "Candidate", new {id = can.Id});
                }

                return View(can);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelfEvaluation(Candidate user)
        {
            /*
             * The method is going to take in a candidate object from the user, it is going to update their RAC Request inside the User object
             *
             * See also:
             * SaveRACRequest()
             *
             * Parameters:
             * string user = Candidate object that is passed from the form back to the controller
             * 
             * Returns:
             * An ActionResult that sends the user to the proper page
             * 
             */
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }
            //do the save function here
            user.RACRequest = RACRequestBLL.SaveRACRequest(user);
            //Getting the updated user session afterwards
            Session["User"] = user;
            return View("SelfEvaluation", (Candidate)Session["User"]);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult HasGenEds(int id)
        {
            /*
             * The method is called by JavaScript. 
             *
             * See also:
             * register.js
             *
             * Parameters:
             * string email = email that from the input field
             * 
             * Returns:
             * True or false of whether or not the email is already taken
             * 
             */
            ProgramBLL pbll = new ProgramBLL(); ;
            return Json(ProgramBLL.CheckProgramHasGenEds(id));
        }
        [AllowAnonymous]
        public ActionResult ViewAccount(int Id, ManageMessageId? message)
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";
            try
            {
                Candidate can = CandidateBLL.GetCandidateById(Id);
                if (((Candidate)(Session["user"])).Id == can.Id)
                {
                    RegisterViewModel val = RegisterViewModel.ToCandidateVal(can);
                    return View(val);
                }
                else {
                    LogOff();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                LogOff();
                return View();
            }
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
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(Models.ChangePasswordViewModel model)
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("ViewAccount", new { Id = ((Candidate)Session["user"]).Id, Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        [AllowAnonymous]
        public ActionResult UpdateAccount(int Id)
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }

            try
            {
                Candidate can = CandidateBLL.GetCandidateById(Id);
                PreferredMethodOfContact pref;
                if (can.PreferredMethodOfContact == null)
                {
                    pref = PreferredMethodOfContact.Email;
                }
                else
                {
                    pref = (PreferredMethodOfContact) can.PreferredMethodOfContact;
                }
                RegisterViewModel regView = new RegisterViewModel()
                {
                    Street = can.Street,
                    City = can.City,
                    Province =  can.Province,
                    Country = can.Country,
                    UserId = can.Id,
                    Email = can.Email,
                    FirstName = can.FirstName,
                    LastName = can.LastName,
                    HomePhone = can.HomePhone,
                    WorkPhone = can.WorkPhone,
                    PreferredMethodOfContact = pref
                };

                if (can.Id == Id)
                {
                    return View(regView);
                }

                LogOff();
                return RedirectToAction("Index", "Home");

            }
            catch
            {
                LogOff();
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult UpdateAccount(FormCollection data)
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }
            int id;
            if (data["UserId"] == null)
            {
                id = 0;
            }
            else {
                id = short.Parse(data["UserId"]);
            }
            var candidate = new Candidate()
            {
                Id = id,
                Email = data["Email"],
                FirstName = data["FirstName"],
                LastName = data["LastName"],
                HomePhone = data["HomePhone"],
                WorkPhone = data["WorkPhone"],
                Street = data["Street"],
                City = data["City"],
                Province = ((provinces)Convert.ToInt32(data["Province"])).GetDisplayName(),
                Country = data["Country"],
                PreferredMethodOfContact =  Convert.ToInt32(data["PreferredMethodOfContact"])
            };

            try
            {
                CandidateBLL.UpdateCandidate(candidate);
                return View("ViewAccount", RegisterViewModel.ToCandidateVal(candidate));
            }
            catch
            {
                return View();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        [AllowAnonymous]
        public ActionResult ChangeProgram(int Id)
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }
            if (((Candidate)Session["user"]).Id != Id) {
                LogOff();
                return RedirectToAction("Home", "Home");
            }
            Candidate candidate = CandidateBLL.GetCandidateById(Id);
            return View("ChangeProgram", candidate);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ChangeProgramPost(Candidate candidate)
        {
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }
            Candidate curr = (Candidate)Session["user"];
            //TODO add check to make sure that the program actually contains gen-eds
            Session["user"] = CandidateBLL.ChangeProgram(curr, candidate.RACRequest.ProgramId, candidate.RACRequest.IsGenEdOnly);
            return RedirectToAction("Home", "Home");
        }

        [AllowAnonymous]
        public ActionResult DeleteAccountCandidate()
        {
            /*
             * The method will call the BLL method that deleted the candidate with the ID of the current logged in user.
             * The page will then redirect the Candidate back to the system home page.
             *
             * See also:
             * CandidateBll -> DeleteCandidate
             *
             * Parameters:
             * None 
             *
             * Returns:
             * A call to the LogOff() controller method that ends the current session and redirects to the home page. 
             *
             */
            if (RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("RacAdvisorHome", "RACAdvisor");
            }
            var c = (Candidate)Session["user"];
            if (c != null)
            {
                CandidateBLL.DeleteCandidate(c.Id);
            }

            return LogOff();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            Session["User"] = null;
            Session["Candidate"] = null;
            FormsAuthentication.SignOut();
            //Redirect("http://AnotherApplicaton/Home/LogOut");
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        

    }
}