﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RAC.BusinessLogic;
using RAC.RACModels;

namespace RAC.Controllers
{
    public class RACAdvisorController : Controller
    {
        public ActionResult RacAdvisorHome()
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            var user = (RACUser) Session["User"];
            var r = new RACAdvisorHomeViewModel();
            r = RACAdvisorBLL.MapToHomeViewModel(user, r);
            r.Notifications = RACAdvisorBLL.GetNotifications((int)userType.RACAdvisor);
            return View(r);
        }

        public ActionResult ManageCandidates()
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            List<RACUser> candidates = CandidateBLL.GetAllCandidates();
            return View(candidates);
        }
        
        [HttpPost]
        public PartialViewResult DeleteNotification(int id)
        {

            Notification.DeleteNotification(id);
            var user = (RACUser)Session["User"];
            var r = new RACAdvisorHomeViewModel();
            r = RACAdvisorBLL.MapToHomeViewModel(user, r);
            r.Notifications = RACAdvisorBLL.GetNotifications((int)userType.RACAdvisor);
            return PartialView("Notification", r);
        }

        public ActionResult CandidateDetails(int id)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            RACUser searchCan = CandidateBLL.GetCandidateById(id);
            Session["Candidate"] = searchCan;
            return View(searchCan);
        }

        public ActionResult CreateCandidate()
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            return View();
        }

        public ActionResult ManageProgram()
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            return View();
        }

        public ActionResult ViewProgramDetails()
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            return View("ProgramDetails");
        }

        public ActionResult CreateProgram()
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            return View("CreateProgram");
        }

        public ActionResult ViewPrograms()
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            return View("ViewPrograms", ProgramBLL.GetAllPrograms());
        }


        public ActionResult ProgramDetails(Program program)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            return View(program);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProgram(FormCollection form)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            //Copying program over if it doesnt exist yet
            var programId = Convert.ToInt32(form["ProgramId"]);
            var courseProfile = Convert.ToInt32(form["CourseProfileId"]);
            ProgramBLL.CopyProgramFromClara(programId, courseProfile);
            //Copying program competencies over that don't exist yet
            ProgramBLL.CopyCompetenciesFromClara(programId, false);

            Program createdProgram;

            //Checking to see if no gen-eds was selected

            var IsNoGenEds = Convert.ToBoolean(form["IsNoGenEds"].Split(',')[0]);
            if (IsNoGenEds == false)
            {
            var genEdId = Convert.ToInt32(form["GenEdid"]);
            ProgramBLL.CopyCompetenciesFromClara(genEdId, true);
            //Copy all the courses over if they don't exist yet
            ProgramBLL.CopyCoursesFromClara(courseProfile);
            createdProgram = ProgramBLL.CreateRACProgramWithGenEds(programId, genEdId, courseProfile);
            }
            else
            {
                ProgramBLL.CopyCoursesFromClaraNoGenEds(courseProfile);
                createdProgram = ProgramBLL.CreateRACProgramWithoutGenEds(programId, courseProfile);
            }
            //Copying gen-ed competencies over that don't exist yet
            
            //Go to next screen to add the competency elements
            return RedirectToAction("EditProgram", new { id = createdProgram.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchProgram(string ProgramCode)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            List<ProgramResultsViewModel> list = ProgramBLL.GetProgramsFromClara(ProgramCode);
            list.Sort((x, y) => y.Year.CompareTo(x.Year));
            return PartialView("_ProgramSearchResult", list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchGenEd(string ProgramCode)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            List<ProgramResultsViewModel> list = ProgramBLL.GetProgramsFromClara(ProgramCode);
            list.Sort((x, y) => string.CompareOrdinal(x.ClaraCode, y.ClaraCode));
            return PartialView("_GenEdSearchResult", list);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetProgramCompetencies(int ProgramId)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            var list = ProgramBLL.GetProgramCompetenciesFromClara(ProgramId);
            list.Sort((x, y) => string.CompareOrdinal(x.CompetencyCode, y.CompetencyCode));
            return PartialView("_ProgramCompetenciesSearchResult", list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetGenEdCompetencies(int ProgramId)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            var list = ProgramBLL.GetGenEdCompetenciesFromClara(ProgramId);
            list.Sort((x, y) => string.CompareOrdinal(x.CompetencyCode, y.CompetencyCode));
            return PartialView("_GenEdCompetenciesSearchResult", list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetCourseProfiles(int ProgramId)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            List<CourseProfileViewModel> list = ProgramBLL.GetCourseProfileFromClara(ProgramId);
            list.Sort((x, y) => string.CompareOrdinal(y.CourseProfileCode, x.CourseProfileCode));
            return PartialView("_CourseProfileSearchResult", list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetCourses(int CourseProfileId)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            List<CourseViewModel> list = ProgramBLL.GetCoursesFromClara(CourseProfileId);
            list.Sort((x, y) => string.CompareOrdinal(x.CourseCode, y.CourseCode));
            return PartialView("_CourseSearchResult", list);
        }

        //WILL NOT RETURN PROGRAM SPECIFIC GEN-EDS EITHER, MOSTLY DONE FOR PROGRAMS THAT DO NOT HAVE ANY GEN-EDS IN THEM
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetCoursesNoGenEds(int CourseProfileId)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            List<CourseViewModel> list = ProgramBLL.GetCoursesFromClaraNoGenEds(CourseProfileId);

            list.Sort((x, y) => string.CompareOrdinal(x.CourseCode, y.CourseCode));
            return PartialView("_CourseSearchResult", list);
        }

        [HttpGet]
        public ActionResult EditProgram(int id)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            return View(ProgramBLL.GetProgram(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProgram(Program program)
        {            
            return View(ProgramBLL.UpdateProgram(program));
        }

        [HttpGet]
        public ActionResult ToggleArchive(int id, bool value)
        {
            ProgramBLL.ToggleArchive(id, value);
            return RedirectToAction("ViewPrograms", "RACAdvisor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProgram(int id)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            ProgramBLL.DeleteProgram(id);
            return PartialView("_Programs", ProgramBLL.GetAllPrograms());
        }

        [HttpGet]
        public ActionResult MapCompetencies(int Id)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            return View(ProgramBLL.GetCourseById(Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MapCompetencies(Course course)
        {
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            ProgramBLL.UpdateCourseCompetencies(course);
            return View(ProgramBLL.GetCourseById(course.Id));
        }



        [AllowAnonymous]
        public ActionResult ArchiveAccountRACAdvisor(int id)
        {
            /*
             * The method will call the BLL method that archives the candidate with the ID passed in.
             * The page will then redirect the RACAdvisor back to the ManageCandidate page in their controller.
             *
             * See also:
             * CandidateBll -> ArchiveCandidate
             *
             * Parameters:
             * id: The id of the candidate to be archived
             *
             * Returns:
             * A redirected Action to the RACAdvisor controller and the ManageCandidates View. 
             *
             */


            /*
             * The 0 is the default for a candidate, and if it can't be found in the calling method, then
             * the id will be 0 indicating that the candidate didn't exist. 
             */
            if (!RACAdvisorBLL.CheckIsRACAdvisor())
            {
                return RedirectToAction("Home", "Candidate");
            }
            if (id > 0)
            {
                CandidateBLL.ArchiveCandidate(id);

            }

            return RedirectToAction("ManageCandidates", "RACAdvisor");
        }

    }
}