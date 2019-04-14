using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RAC.Validation;
using System.Globalization;
using RAC.BusinessLogic;

namespace RAC.Models.Validation
{
    public class ForgotPasswordVal
    {


        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Id { get; set; }

        [Required]
        [StringLength(24, MinimumLength = 12)]
        public string Code { get; set; }

        //FOR THE FORGOT PASSWORD VIEW
        [Display(Name = "*Email")]
        [Required(ErrorMessage = "Email is Required")]
        [ExistingEmail(ErrorMessage = "This email doesn't exist")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        //FOR THE RESET PASSWORD VIEW
        [Display(Name = "*New Password")]
        [Required(ErrorMessage = "New Password is Required")]
        public string Password { get; set; }

        [Display(Name = "*Confirm Password")]
        [Required(ErrorMessage = "Please Confirm your Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }




        public class ExistingEmailAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (CandidateBLL.GetUser((String)value) == null)
                {
                    var errorMessage = FormatErrorMessage((validationContext.DisplayName));
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
}