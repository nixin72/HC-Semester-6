using System;
using System.Web.Mvc;
using CSAdmin2.Logic;
using CSAdmin2.Model;

namespace CSAdminMVC.Content
{
	public class SemesterController : Controller
	{
		// GET: Semester
		public ActionResult Index()
		{
			return View();
		}

		public Setting SelectSettings()
		{
			return SemesterManager.SelectSettings();
		}

		public DateTime? GetSemesterEndDate(string yearSemester)
		{
			if (yearSemester != null || yearSemester != "")
			{
				short temp = Convert.ToInt16(yearSemester);
				return SemesterManager.GetSemesterEndDate(temp);
			}

			return null;
		}
		
		public int UpdateSemester(string yearSemester, string endDate)
		{
			return SemesterManager.UpdateCurrentSemester(Convert.ToInt16(yearSemester), Convert.ToDateTime(endDate));
		}

		public string getYearSemesterString(short yearSemester)
		{
			return Format.FormatSemester(yearSemester);
		}
		
		public int SelectNumNewStudents(int AnSession)
		{
			return UserManager.NumAddCESStudents(AnSession);
		}
		
		public int SelectNumNewCSAdminStudents(int AnSession)
		{
			return UserManager.SelectNumNewCSAdminStudents(AnSession);
		}
		
		public int SelectNumActivateStudents(int AnSession)
		{
			return UserManager.SelectNumActivateStudents(AnSession);
		}
		
		public int SelectNumDeactivateStudents(int AnSession)
		{
			return SemesterManager.SelectNumDeactivateStudents(AnSession);
		}

		public int SelectNumDelStudents(int AnSession)
		{
			return SemesterManager.SelectNumDelStudents(AnSession);
		}

		public int SelectNumNewFaculty(int AnSession)
		{
			return SemesterManager.SelectNumNewFaculty(AnSession);
		}

		public int SelectNumNewCoordinators(int AnSession)
		{
			return SemesterManager.SelectNumNewCoordinators(AnSession);
		}

		public int SelectNumDelCoordinators(int AnSession)
		{
			return SemesterManager.SelectNumDelCoordinators(AnSession);
		}

		public int updateSemesterFaculty()
		{
            return NewSemesterManager.UpdateTeachers() + NewSemesterManager.UpdateCoordinators();
        }

        public int updateSemesterStudents()
        {
            return NewSemesterManager.UpdateStudents();
        }

    }
}