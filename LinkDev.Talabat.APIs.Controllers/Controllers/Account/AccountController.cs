using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Account
{
    public class AccountController(IServiceManager serviceManager ) : BaseApiController
	{
		[HttpPost("login")] // POST : /api/account/login
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{ 
			var response = await serviceManager.AuthService.LoginAsync(model);
			return Ok(response);
		}

		[HttpPost("register")] // POST: /api/account/register
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var response = await serviceManager.AuthService.RegisterAsync(model);
			return Ok(response);
		}

        [Authorize]
        [HttpGet] // GET : /api/account
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Result = await serviceManager.AuthService.CurrentUser(User);
            return Ok(Result);
        }

		[Authorize]
		[HttpGet("address")] // GET : / api/account/address
		public async Task<ActionResult<AddressDto>> GetUserAddress()
		{ 
			var result = await serviceManager.AuthService.GetUserAddressAsync(User);
			return Ok(result);	
		}

		[Authorize]
		[HttpPut("address")] // PUT : /api/account/address
		public async Task<ActionResult<AddressDto>> UpdateUserAddress( AddressDto addressDto)
		{
			var result = await serviceManager.AuthService.UpdatedUserAddressAsync(User, addressDto);
			return Ok(result);
		}



        [HttpGet("emailExist")] // GET : /api/account/emailExist?email=xma7med@gmail.com
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            var Result = await serviceManager.AuthService.EmailExists(email);
            return Ok(Result);
        }

    }
}
