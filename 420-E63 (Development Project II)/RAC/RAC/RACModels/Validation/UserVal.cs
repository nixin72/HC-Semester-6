using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Globalization;
using RAC.BusinessLogic;


namespace RAC.Validation
{
    public class UserVal
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
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}$", ErrorMessage = "Please enter a valid password. It must match the following: <br />- Contains between 8 and 20 characters.<br />- Contains only numbers and letters.<br />- Contains at least one number.<br />- Contains at least one letter.<br />- Contains no special symbols or characters.")]
        public string Password { get; set; }
        
        [Display(Name = "*Confirm Password")]
        [Required(ErrorMessage = "Please Confirm your Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }

    }

   
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (CandidateBLL.GetUser((string)value) != null)
            {
                string errorMessage = FormatErrorMessage((validationContext.DisplayName));
                return new ValidationResult(errorMessage);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name);
        }

    }

    
}