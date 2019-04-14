using System.ComponentModel.DataAnnotations;

namespace CSAdminMVC.Models
{
	public class LoginModel
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Username"),
		 StringLength(60, ErrorMessage = "Usernames cannot more than 60 characters long")]
		public string Username { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a password")]
		[Display(Name = "Password")]
		public string password { get; set; }
	}
}