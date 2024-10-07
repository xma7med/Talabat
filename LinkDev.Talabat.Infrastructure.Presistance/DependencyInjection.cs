using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Presistance.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Presistance
{
	public static class DependencyInjection
	{
		// Extention Method public static 
		public static IServiceCollection AddPresistanceServices(this IServiceCollection services , IConfiguration configuration)
		{
			services.AddDbContext<StoreContext>((optionBuilder) =>
			{
				optionBuilder.UseSqlServer(configuration.GetConnectionString("StoreContext"));
			} /*, contextLifetime: ServiceLifetime.Scoped , optionsLifetime : ServiceLifetime.Scoped*/);

			services.AddScoped<IStoreContextIntializer, StoreContextInitializer> ();	
			services.AddScoped( typeof(IStoreContextIntializer), typeof(StoreContextInitializer));	


			return services; // DI Container 
		}
	}
}
