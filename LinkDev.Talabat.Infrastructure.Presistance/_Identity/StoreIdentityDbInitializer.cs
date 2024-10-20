using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Presistance.Common;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Infrastructure.Presistance.Identity
{
	public sealed class StoreIdentityDbInitializer(StoreIdentityDbContext _dbContext , UserManager<ApplicationUser> _userManager) : DbInitializer(_dbContext) , IStoreIdentityDbInitializer
	{

		public override async Task SeedAsync() // Seeds
		{
			var user = new ApplicationUser()
			{
				DisplayName = "Mohamed Nasser",
				UserName = "xma7med",
				Email ="xma7med@gmil.com",
				PhoneNumber = "01012139178"
			
			};

			//await _userManager.CreateAsync(user);	
			await _userManager.CreateAsync(user   , "P@ssw0rd");
		}
	}
}
