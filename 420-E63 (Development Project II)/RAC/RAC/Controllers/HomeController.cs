
using RAC.Models;
using RAC.RACModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Configuration;

namespace RAC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.RacAdvisorName = WebConfigurationManager.AppSettings.Get("RacAdvisorName");
            ViewBag.RacAdvisorEmail = WebConfigurationManager.AppSettings.Get("RacAdvisorEmail");
            ViewBag.RacAdvisorPhoneNumber = WebConfigurationManager.AppSettings.Get("RacAdvisorPhoneNumber");
            ViewBag.RacAdvisorFaxNumber = WebConfigurationManager.AppSettings.Get("RacAdvisorFaxNumber");
            return View();
        }

        public ActionResult ReleaseNotes()
        {
            ViewBag.Message = "Chronological release notes for the application.";
            return View();
        }

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
                    if (((User)Session["User"]).UserType == (int)userType.RACAdvisor)
                    {
                        return RedirectToAction("RACAdvisorHome", "RACAdvisor");
                    }
                    else if (((User)Session["User"]).UserType == (int)userType.Candidate)
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
                        Console.WriteLine("Error could not be logged: " + e.ToString());
                    }

                    return RedirectToAction("Index", "Home");

                }
            }
            return View("Index");
        }
    }
}