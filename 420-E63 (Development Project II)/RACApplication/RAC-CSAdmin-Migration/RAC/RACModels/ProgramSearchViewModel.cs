using System.ComponentModel.DataAnnotations;

namespace RAC.RACModels
{
    public class ProgramSearchViewModel
    {
        [Required(ErrorMessage = "You Must Enter a Program Code")]
        [Display(Name = "Program Code:")]
        public string ProgramCode { get; set; }
        [Display(Name = "No Gen-Eds:")]
        public bool IsNoGenEds { get; set; }

        [Required(ErrorMessage = "You Must Enter a Gen-Ed Code")]
        [Display(Name = "Gen-Ed Code:")]
        public string GenEdCode { get; set; }
    }

    public class ProgramResultsViewModel
    {
        [Display(Name="Clara Id")]
        public int ClaraId { get; set; }
        [Display(Name = "Clara Code")]
        public string ClaraCode { get; set; }
        [Display(Name = "Year Of Version")]
        public int Year { get; set; }
        [Display(Name = "Program Name")]
        public string ProgramNameFromClaraEN { get; set; }
        [Display(Name = "Program Name (French)")]
        public string ProgramNameFromClaraFR { get; set; }
        [Display(Name = "# Competencies From Clara")]
        public int NumberOfCompetencies { get; set; }
        [Display(Name = "# Course Profiles From Clara")]
        public int NumberOfCourseProfiles { get; set; }

        [Display(Name = "Type Of Program")]
        public string TypeOfProgram { get; set; }
    }

    public class CourseProfileViewModel
    {
        [Display(Name = "Select One")]
        public int CourseProfileClaraId { get; set; }

        [Display(Name = "Course Profile Name")]
        public string ProfileName { get; set; }
        [Display(Name = "Course Profile Code")]
        public string CourseProfileCode { get; set; }
    }

    public class CompetenciesViewModel
    {
        [Display(Name = "Competency Codes")]
        public string CompetencyCode { get; set; }
        [Display(Name = "Competency Descriptions")]
        public string CompetencyDescription { get; set; }
    }

    public class CourseViewModel
    {
        [Display(Name = "Course Code")]
        public string CourseCode { get; set; }
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
        [Display(Name = "Course Type")]
        public CourseType CourseType { get; set; }

    }


}