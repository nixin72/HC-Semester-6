using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RAC.RACModels;
using RAC.BusinessLogic;
using System.Web.Mvc;
using System.Linq;

namespace RAC.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        /*
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        */
        public int UserId { get; set; }

        [Display(Name = "*First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Display(Name = "*Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Display(Name = "*Email")]
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "*Home Phone")]
        [Required(ErrorMessage = "Home Phone is Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid Phone number")]
        public string HomePhone { get; set; }

        [Display(Name = "Work Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid Work Phone number")]
        public string WorkPhone { get; set; }

        public int UserType { get; set; }

        [Display(Name = "*Password")]
        [Required(ErrorMessage = "Password is Required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}$", ErrorMessage = "Please enter a valid password" )]
        public string Password { get; set; }

        [Display(Name = "*Confirm Password")]
        [Required(ErrorMessage = "Please Confirm your Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }

        public string Street { get; set; }
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a province")]
        [Display(Name = "*Province")]
        public string Province { get; set; }
        public string Country { get; set; }

        [Display(Name = "*Method Of Contact")]
        [Required(ErrorMessage = "Method Of Contact is Required")]
        public PreferredMethodOfContact PreferredMethodOfContact { get; set; }

        [Display(Name = "*Program")]
        [Required(ErrorMessage = "Please Choose a Program")]
        public int ProgramId { get; set; }

        [Display(Name = "General Education Only")]
        public bool GenEdOnly { get; set; }

        public bool IsArchived { get; set; }


        public IList<SelectListItem> ProgramsOffered { get; set; }

        public IList<SelectListItem> GetProgramsOfferedDropDownList()
        {
            IEnumerable<Program> programs = DbContext.Context.Program;
            List<SelectListItem> programsOffered = new List<SelectListItem>();
            foreach (Program program in programs)
            {
                if (!program.ProgramMinistryData.Name.Equals("General Education"))
                {
                    programsOffered.Add(new SelectListItem()
                    {
                        Text = program.ProgramMinistryData.Name,
                        Value = program.Id.ToString()
                    });
                }

            }

            //Looping to put them in alphabetical order
            programsOffered = programsOffered.OrderBy(x => x.Text).ToList();

            return programsOffered;
        }

        public RACUser ToCandidate()
        {
            RACUser can = CandidateBLL.GetCandidateById(UserId);
            can.Id = UserId;
            can.FirstName = FirstName;
            can.LastName = LastName;
            can.Email = Email;
            can.HomePhone = HomePhone;
            can.WorkPhone = WorkPhone;
            can.UserType = UserType;
            can.Street = Street;
            can.City = City;
            can.Province = Province;
            can.Country = Country;
            can.PreferredMethodOfContact = (int)PreferredMethodOfContact;
            can.IsArchived = IsArchived;

            return can;
        }

        public static RegisterViewModel ToCandidateVal(RACUser candidate)
        {
            var val = new RegisterViewModel();
            try
            {
                val.UserId = candidate.Id;
                val.FirstName = candidate.FirstName;
                val.LastName = candidate.LastName;
                val.Email = candidate.Email;
                val.HomePhone = candidate.HomePhone;
                val.WorkPhone = candidate.WorkPhone;
                val.UserType = candidate.UserType;
                val.Street = candidate.Street;
                val.City = candidate.City;
                val.Province = candidate.Province;
                val.Country = candidate.Country;
                if (candidate.PreferredMethodOfContact != null)
                {
                    val.PreferredMethodOfContact = (PreferredMethodOfContact) candidate.PreferredMethodOfContact;
                }

                val.IsArchived = candidate.IsArchived;
            }
            catch
            {
                val.PreferredMethodOfContact = PreferredMethodOfContact.Email;
            }

            return val;
        }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
