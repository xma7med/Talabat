﻿using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Exception;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkDev.Talabat.Core.Application.Services.Auth
{
	public class AuthService (  
		IOptions<JwtSetings> jwtSettings ,
		UserManager<ApplicationUser> userManager , 
		SignInManager<ApplicationUser> signInManager): IAuthService
	{

		private readonly JwtSetings _jwtSettings =jwtSettings.Value;
		public async Task<UserDto> LoginAsync(LoginDto model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);
			if (user is null) throw new UnAuthorizedException("Invalid Login");
			var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure : true);
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


		private async Task<string> GenerateTokenAsync(ApplicationUser user)
		{ 
			
			
			var  privateClaims = new List<Claim>()
			{ 
				new Claim (ClaimTypes.PrimarySid, user.Id),
				new Claim (ClaimTypes.Email, user.Email!),
				new Claim (ClaimTypes.GivenName, user.DisplayName),

			}.Union (await userManager.GetClaimsAsync(user)).ToList();
			foreach (var role in await userManager.GetRolesAsync(user))
				privateClaims.Add(new Claim (ClaimTypes.Role, role));


			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

			var tokenObj = new JwtSecurityToken(
				audience:_jwtSettings.Audience,
				issuer: _jwtSettings.Issuer,	
				expires:DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
				claims:privateClaims,
				// Headre and key 
				signingCredentials:new SigningCredentials(authKey , SecurityAlgorithms.HmacSha256)

				);
			
			// Generate Token 
			return new JwtSecurityTokenHandler().WriteToken(tokenObj);


		}
	}
}
