using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Presistance._Identity.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Presistance.Identity
{
	/// <summary>
	/// * inherit from               -->  IdentityDbContext
	/// * give options to base Cons  -->  DbContextOptions<TContext>
	/// * Override OnModelCreating To call the base on model...
	/// * Identity Configs 
	/// * Add options to StoreIdentityDbContext In Dependancy Injection Container 
	/// </summary>



	// IdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
	// IdentityDbContext<TUser> : IdentityDbContext<TUser, IdentityRole, string> where TUser : IdentityUser
	// dentityDbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
	public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser >
	{
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options)
			:base (options) 
        {
            
        }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			//builder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly); // I cant  there Configs also for The BaseEntity Congfigs and i want only Configs for StoreIdentityDbContext 
			builder.ApplyConfiguration(new ApplicationUserConfigurations());
			builder.ApplyConfiguration(new AddressConfigurations());

		}
	}
}
