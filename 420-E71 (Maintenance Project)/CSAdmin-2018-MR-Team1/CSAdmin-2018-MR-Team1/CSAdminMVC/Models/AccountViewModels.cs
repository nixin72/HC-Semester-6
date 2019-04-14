using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CSAdminMVC.Models
{
    public enum PreferredMethodOfContact
    {
        [Display(Name = "Email")]
        Email = 0,
        [Display(Name = "Home Phone")]
        HomePhone = 1,
        [Display(Name = "Work Phone")]
        WorkPhone = 2
    }

    public class RegisterViewModel
    {
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
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}$", ErrorMessage = "Please enter a valid password")]
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

        public ApplicationUser ToUser()
        {
            return new ApplicationUser() {
                Id = UserId.ToString(),
                FirstName = FirstName,
                LastName = LastName,
                UserName = Email,
                Email = Email,
                HomePhone = HomePhone,
                WorkPhone = WorkPhone,
                UserType = UserType,
                City = City,
                Street = Street,
                Province = Province,
                Country = Country,
                PreferredMethodOfContact = (int)PreferredMethodOfContact,
                IsArchived = IsArchived,
            };
        }
    }
}