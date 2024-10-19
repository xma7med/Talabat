using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Presistance.Data;
using LinkDev.Talabat.Infrastructure.Presistance.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Talabat.Dashboard
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			// Add services to the container.
			// first step bring all configs related to DB 
			builder.Services.AddControllersWithViews();
			#region For DB
			builder.Services.AddDbContext<StoreDbContext>((optionBuilder) =>
			{
				optionBuilder.UseLazyLoadingProxies()
				.UseSqlServer(builder.Configuration.GetConnectionString("StoreContext"));
			});
			// to let him know that i will use appUser insted of IdentityUser
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>((identityOptions) =>
			{
				identityOptions.User.RequireUniqueEmail = true;
				identityOptions.Lockout.AllowedForNewUsers = true;
				identityOptions.Lockout.MaxFailedAccessAttempts = 5;
				identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);
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
