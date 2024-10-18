using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Auth
{
	public class LoginDto
	{
		[Required]
		[EmailAddress]
		public required string Email { get; set; }

		[Required]
		//[DataType(DataType.Password)]	// Just for Display we dont have thing to displaY 
		public required string Password { get; set; }
    }
}
