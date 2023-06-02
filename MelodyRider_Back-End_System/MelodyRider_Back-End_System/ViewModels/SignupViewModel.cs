using System.ComponentModel.DataAnnotations;

namespace MelodyRider_Back_End_System.ViewModels
{
	public class SignupViewModel
	{
		[Required]
		[StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
