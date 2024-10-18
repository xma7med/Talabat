using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Presistance.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.APIs.Extention
{
	public static class IdentityExtension
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services , IConfiguration configuration)
		{

			services.Configure<JwtSetings>(configuration.GetSection("JWTSettings"));

			/// Register Required Service for security / identity Services 
			//webApplicationbuilder.Services.AddIdentity<ApplicationUser , IdentityRole>();	
			services.AddIdentity<ApplicationUser, IdentityRole>((identityOptions) =>
			{
				identityOptions.SignIn.RequireConfirmedAccount = true;
				identityOptions.SignIn.RequireConfirmedEmail = true;
				identityOptions.SignIn.RequireConfirmedPhoneNumber = true;

				/// identityOptions.Password.RequireNonAlphanumeric = true; // #$@%
				/// identityOptions.Password.RequiredUniqueChars = 2;
				/// identityOptions.Password.RequiredLength = 6;
				/// identityOptions.Password.RequireDigit = true;
				/// identityOptions.Password.RequireLowercase = true;
				/// identityOptions.Password.RequireUppercase = true;

				identityOptions.User.RequireUniqueEmail = true;
				// identityOptions.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";

				identityOptions.Lockout.AllowedForNewUsers = true;
				identityOptions.Lockout.MaxFailedAccessAttempts = 5;
				identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);

				/// Search 
				//identityOptions.Stores
				//identityOptions.Tokens
				//identityOptions.ClaimsIdentity

			})
				.AddEntityFrameworkStores<StoreIdentityDbContext>();

			return services;
		}
	}
}
