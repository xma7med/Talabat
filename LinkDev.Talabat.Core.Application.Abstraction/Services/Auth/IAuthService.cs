using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Common;
using System.Security.Claims;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Auth
{
	public interface IAuthService
	{
		Task<UserDto> LoginAsync(LoginDto model);

		Task<UserDto> RegisterAsync(RegisterDto model);
        Task<UserDto> CurrentUser(ClaimsPrincipal claimsPrincipal);
		Task<AddressDto?> GetUserAddressAsync(ClaimsPrincipal claimsPrincipal);
		Task<AddressDto?> UpdatedUserAddressAsync(ClaimsPrincipal claimsPrincipal, AddressDto addressDto);

    }
}
