﻿using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infrastructure.Presistance.Data;
using LinkDev.Talabat.Infrastructure.Presistance.Identity;
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
			#region Store DbContext
			services.AddDbContext<StoreDbContext>((optionBuilder) =>
				{
					optionBuilder.UseLazyLoadingProxies()
					.UseSqlServer(configuration.GetConnectionString("StoreContext"));
				} /*, contextLifetime: ServiceLifetime.Scoped , optionsLifetime : ServiceLifetime.Scoped*/);

			services.AddScoped<IStoreContextIntializer, StoreDbContextInitializer>();
			services.AddScoped(typeof(IStoreContextIntializer), typeof(StoreDbContextInitializer));

			services.AddScoped(typeof(ISaveChangesInterceptor), typeof(BaseEntityAuditableInterceptor));

			#endregion


			#region Identity DbContext

			services.AddDbContext<StoreIdentityDbContext>((optionBuilder) =>
			{
				optionBuilder.UseLazyLoadingProxies()
				.UseSqlServer(configuration.GetConnectionString("IdentityContext"));
			});

			#endregion
			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
			return services; // DI Container 
		}
	}
}
