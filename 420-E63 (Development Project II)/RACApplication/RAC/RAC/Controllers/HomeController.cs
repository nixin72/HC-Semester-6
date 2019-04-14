using RAC.RACModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System.Web.Configuration;
using RAC.BusinessLogic;

namespace RAC.Controllers
{
    public class HomeController : Controller
    {
        [PageHistory]
        public ActionResult Index()
        {
            return View();
        }

        [PageHistory]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [PageHistory]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.RacAdvisorName = WebConfigurationManager.AppSettings.Get("RacAdvisorName");
            ViewBag.RacAdvisorEmail = WebConfigurationManager.AppSettings.Get("RacAdvisorEmail");
            ViewBag.RacAdvisorPhoneNumber = WebConfigurationManager.AppSettings.Get("RacAdvisorPhoneNumber");
            ViewBag.RacAdvisorFaxNumber = WebConfigurationManager.AppSettings.Get("RacAdvisorFaxNumber");
            return View();
        }

        [PageHistory]
        public ActionResult ReleaseNotes()
        {
            ViewBag.Message = "Chronological release notes for the application.";
            return View();
        }

        [PageHistory]
        public ActionResult Help()
        {
            ViewBag.IsRACAdvisor = RACAdvisorBLL.CheckIsRACAdvisor();
            return View();
        }

        [PageHistory]
        public ActionResult Home()
        {
            /*
          * The method will return the appropriate view based on the proper user type. It will pass in the session to the view.
          *
          * See also:
          * None
          *
          * Parameters:
          * None
          * 
          * Returns:
          * An ActionResult that sends the user to the proper home page.
          * 
          */
            if (Session["User"] != null)
            {
                try
                {
                    if (((RACUser)Session["User"]).UserType == (int)userType.RACAdvisor)
                    {
                        return RedirectToAction("RACAdvisorHome", "RACAdvisor");
                    }
                    else if (((RACUser)Session["User"]).UserType == (int)userType.Candidate)
                    {
                        return RedirectToAction("Home", "Candidate");
                    }
                }
                catch (InvalidCastException ex)
                {
                    try
                    {
                        using (StreamWriter file = new StreamWriter(Server.MapPath("err.log"), true))
                        {
                            file.WriteLine(ex.ToString());
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(@"Error could not be logged: " + e);
                    }

                    return RedirectToAction("Index", "Home");

                }
            }
            return View("Index");
        }

        public ActionResult PreviousPage()
        {
            /*
             * Called whenever a user clicks on the "back button". Redirects the user to the previous page inside of the
             * internal RAC history stack.
             *
             * See also:
             * PageHistoryAttribute.cs
             * Global.asax.cs
             */

            var historyStack = (Stack<string>) Session["historyStack"];

            // `backupPage` exists in the event the stack is empty, which may happen if the user is mixing internal
            // history with browser history.
            string[] backupPage = historyStack.Pop().Split('/');
            string controller;
            string action;
            string parameter;

            // Checks first if the history stack is empty, uses the back-up page as a fallback.
            // The stack cannot be `null`, unless the action is being fired before any `GET` requests are made to a view.
            if (historyStack.Count == 0)
            {
                controller = backupPage[0];
                action = backupPage[1];

                // A length of three means a parameter is required for the previous page
                if (backupPage.Length == 3)
                {
                    parameter = backupPage[2];
                    return RedirectToAction(action, controller, new { Id = parameter });
                }
            }
            else
            {
                string[] previousPage = historyStack.Pop().Split('/');
                controller = previousPage[0];
                action = previousPage[1];
                if (previousPage.Length == 3)
                {
                    parameter = previousPage[2];
                    return RedirectToAction(action, controller, new { Id = parameter });
                }
            }

            return RedirectToAction(action, controller);
        }
    }
}