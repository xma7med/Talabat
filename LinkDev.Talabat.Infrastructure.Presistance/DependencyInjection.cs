using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infrastructure.Presistance.Data;
using LinkDev.Talabat.Infrastructure.Presistance.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

			services.AddScoped(typeof(ISaveChangesInterceptor), typeof(BaseEntityAuditableInterceptor));

			return services; // DI Container 
		}
	}
}
