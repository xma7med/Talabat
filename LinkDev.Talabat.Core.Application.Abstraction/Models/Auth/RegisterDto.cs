using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Auth
{
	public  class RegisterDto
	{
		public required string DisplayName { get; set; }

		[Required]
		public required string UserName { get; set; }

		[Required]
		[EmailAddress]
		public required string Email { get; set; }

		[Required]
		public required string Phone { get; set; }

		[Required]
		[RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&()_+}{\":;'?/<>.,])(?=^.*\\S).*$",
					ErrorMessage = "Password must have at least 1 uppercase, 1 lowercase, 1 number, 1 non-alphanumeric, and be at least 6 characters long")]

		public required string Password { get; set; }

	}
}
