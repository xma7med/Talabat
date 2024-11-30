using LinkDev.Talabat.Core.Application.Abstraction.Models.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Exception;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using LinkDev.Talabat.Core.Application.Extention;

namespace LinkDev.Talabat.Core.Application.Services.Auth
{
	public class AuthService ( 
		IMapper mapper ,
		IConfiguration configuration ,
		IOptions<JwtSetings> jwtSettings ,
		UserManager<ApplicationUser> userManager , 
		SignInManager<ApplicationUser> signInManager): IAuthService
	{

		private readonly JwtSetings _jwtSettings =jwtSettings.Value;

        public async Task<UserDto> CurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email!);
            return new UserDto()
            {
                Id = user!.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user)
            };
        }

        public async Task<AddressDto?> GetUserAddressAsync(ClaimsPrincipal claimsPrincipal)
        {
            //var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email)!;
            /// FindFirstValue => by default not include Navigation Prperty [ make Extention method like it to include navigation property ] 
			//var user = await userManager.FindByEmailAsync(email);
			var user = await userManager.FindUserWithAddressAsync(claimsPrincipal);
			var address = mapper.Map<AddressDto>(user!.Address);
			return address;
        }
        public async Task<AddressDto?> UpdatedUserAddressAsync(ClaimsPrincipal claimsPrincipal, AddressDto addressDto)
        {
			var updatedAddress = mapper.Map<Address>(addressDto);
			var user = await userManager.FindUserWithAddressAsync(claimsPrincipal);

			/// Make sure that will update the existing user address - To not add another address 
			if (user!.Address is not null)
			{
				updatedAddress.Id = user.Address.Id;
			}

			user.Address= updatedAddress;

			var result = await userManager.UpdateAsync(user);
			if (!result.Succeeded) throw new BadRequestException(result.Errors.Select(error => error.Description).Aggregate((X, Y) => $"{X} , {Y}"));
			return addressDto;
				
        }

        // Check If User Exist 
        // Check If Pass Is Valid 
        public async Task<UserDto> LoginAsync(LoginDto model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);
			if (user is null) throw new UnAuthorizedException("Invalid Login");
			var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure : true);
           
			if (result.IsNotAllowed) throw new UnAuthorizedException("Account not confirmed yet.");
            if (result.IsLockedOut) throw new UnAuthorizedException("Account is locked.");
            // if (result.RequiresTwoFactor) throw new UnauthorizedException("Requires Two-Factor Authentication."); // may handled by front end 

            if (!result.Succeeded) throw new UnAuthorizedException("Invalid Login");

			var response = new UserDto()
			{
				Id = user.Id,
				DisplayName	= user.DisplayName,	
				Email = user.Email!,
				Token =await GenerateTokenAsync(user),	

			};
			return response;
		}
        // if not valid there are [ Automatic Model Validation with [ApiController] ] intercept the req and if not valid will not enter the end point 
        public async Task<UserDto> RegisterAsync(RegisterDto model)
		{
			var user = new ApplicationUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				UserName = model.UserName,
				PhoneNumber = model.Phone,
			};
			// to use hashing to the password  in user manager 
			var result = await userManager.CreateAsync(user , model.Password);

			if (!result.Succeeded) throw new ValidationException() { Errors = result.Errors.Select(E => E.Description) };

			var response = new UserDto()
			{
				Id = user.Id,
				DisplayName = user.DisplayName,
				Email = user.Email!,
				Token = await GenerateTokenAsync(user)

			};
			return response;
		}


        // implemnting generate token 
        // step 1 : private method take the user to be generate token to him 
        // token => 1) header - 2) payload[registered / public for Auth & private for Info Exchange ] - 3) signature 
        //
        private async Task<string> GenerateTokenAsync(ApplicationUser user)
		{   /// payloda - claims [ Public - Private claims for info excchange]
			// public Claims 


            var claims = new List<Claim>()
			{ 
				new Claim (ClaimTypes.PrimarySid, user.Id), // The claim that i used in Interceptor 
				new Claim (ClaimTypes.Email, user.Email!),
				new Claim (ClaimTypes.GivenName, user.DisplayName),
				//new Claim ("MyKey", user.DisplayName),// Private claim

			}.Union (await userManager.GetClaimsAsync(user)).ToList(); //User Claims 
			foreach (var role in await userManager.GetRolesAsync(user))// Roles As Claims 
                claims.Add(new Claim (ClaimTypes.Role, role));
            
			//---------------------------------------------------------------------------------------------------------------

            //Key must Convert to Bytes 
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            /// Signature 
            var signingCredentials = new SigningCredentials(authKey , SecurityAlgorithms.HmacSha256);
			//---------------------------------------------------------------------------------------------------------------
			
			var tokenObj = new JwtSecurityToken(
				/// Payload
				// first pass the register Claims
				issuer: /*configuration["JWTSettings:Issuer"]*/ _jwtSettings.Issuer,	
				audience:_jwtSettings.Audience,
				expires:DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
				// My Claims
				claims: claims,
				signingCredentials: signingCredentials /*new SigningCredentials(authKey , SecurityAlgorithms.HmacSha256)*/
                );
			// Generate / Create  Token 
			return new JwtSecurityTokenHandler().WriteToken(tokenObj);
		}
	}
}
