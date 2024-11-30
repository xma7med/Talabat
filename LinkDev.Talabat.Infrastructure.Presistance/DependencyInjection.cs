using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Infrastructure.Presistance.Data;
using LinkDev.Talabat.Infrastructure.Presistance.Identity;
using LinkDev.Talabat.Infrastructure.Presistance.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Presistance
{
    public static class DependencyInjection
	{
		// Extention Method public static 
		public static IServiceCollection AddPresistanceServices(this IServiceCollection services , IConfiguration configuration)
		{
			#region Store DbContext
			services.AddDbContext<StoreDbContext>((serviceProvider, optionBuilder) =>
				{
					optionBuilder.UseLazyLoadingProxies()
					.UseSqlServer(configuration.GetConnectionString("StoreContext"))
					.AddInterceptors(serviceProvider.GetRequiredService<AuditInterceptor>());

				} /*, contextLifetime: ServiceLifetime.Scoped , optionsLifetime : ServiceLifetime.Scoped*/);

			services.AddScoped(typeof(AuditInterceptor));

            //services.AddScoped<IStoreDbIntializer, StoreDbInitializer>();
            services.AddScoped(typeof(IStoreDbIntializer), typeof(StoreDbInitializer));

			//services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));

			#endregion


			#region Identity DbContext

			services.AddDbContext<StoreIdentityDbContext>((optionBuilder) =>
			{
				optionBuilder.UseLazyLoadingProxies()
				.UseSqlServer(configuration.GetConnectionString("IdentityContext"));
			});

			services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitializer));


			#endregion
			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
			return services; // DI Container 
		}
	}
}
