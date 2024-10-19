using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Presistance.Data;
using LinkDev.Talabat.Infrastructure.Presistance.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Talabat.Dashboard
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			// Add services to the container.
			// first step bring all configs related to DB 
			#region For DB
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<StoreDbContext>((optionBuilder) =>
			{
				optionBuilder.UseLazyLoadingProxies()
				.UseSqlServer(builder.Configuration.GetConnectionString("StoreContext"));
			} /*, contextLifetime: ServiceLifetime.Scoped , optionsLifetime : ServiceLifetime.Scoped*/);

			// to let him know that i will use appUser insted of IdentityUser
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>((identityOptions) =>
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
				//identityOptions.Stores
				//identityOptions.Tokens
				//identityOptions.ClaimsIdentity

			})
			.AddEntityFrameworkStores<StoreIdentityDbContext>();

			builder.Services.AddDbContext<StoreIdentityDbContext>((optionBuilder) =>
			{
				optionBuilder.UseLazyLoadingProxies()
				.UseSqlServer(builder.Configuration.GetConnectionString("IdentityContext"));
			});

			#endregion


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
