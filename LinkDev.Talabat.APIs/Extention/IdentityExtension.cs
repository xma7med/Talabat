using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Presistance.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
				//identityOptions.SignIn.RequireConfirmedAccount = true;
				//identityOptions.SignIn.RequireConfirmedEmail = true;
				//identityOptions.SignIn.RequireConfirmedPhoneNumber = true;

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
				///identityOptions.Stores
				///identityOptions.Tokens
				///identityOptions.ClaimsIdentity
			})
				.AddEntityFrameworkStores<StoreIdentityDbContext>();

			//services.AddAuthentication(); // By defualt it have been Called through => AddIdentity , defualt Schema 
            //services.AddAuthentication("Hamada"); // change defualt schema with the defualt handler 
            services.AddAuthentication((authenticationOption) =>
			{
				authenticationOption.DefaultAuthenticateScheme = /*"Bearer"*/ JwtBearerDefaults.AuthenticationScheme;
				authenticationOption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;	
			}) // change defualt schema and Handler 
				.AddJwtBearer((options)=>
				{
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,


						ValidAudience = configuration["JWTSettings:Audience"],
						ValidIssuer = configuration["JWTSettings:Issure"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]!)),
						ClockSkew=TimeSpan.Zero,// SomeTimes After expiration token dosent expire cause of diff Time Zone
                                                // this make token expire at the time 
                    };
				})
				/*.AddJwtBearer("Bearer02" , (opti) => )*/;
			//-----------------------------------------
			services.AddScoped(typeof(IAuthService) , typeof(AuthService));
			services.AddScoped(typeof(Func<IAuthService>), (serviceProvider) =>
			{ 
				return () => serviceProvider.GetRequiredService<IAuthService>();
			});
			//-----------------------------------------


			return services;
		}
	}
}
