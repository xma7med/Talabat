﻿using System.ComponentModel.DataAnnotations;

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
		public required string PhoneNumber { get; set; }

		[Required]
		[RegularExpression("(?=^.{6,10}$)(?=.\\d)(?=.[a-z])(?=.[A-Z])(?=.[!@#$%^&amp;()_+}{&quot;:;'?/&gt;.&lt;,])(?!.\\s).*$",
			ErrorMessage = "Password Must Have At Least 1 Uppercase, 1 Lowercase, 1 Number, 1 Non Alphnumeric And At Least 6 Characters")]

		public required string Password { get; set; }

	}
}
