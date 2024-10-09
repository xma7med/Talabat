using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Core.Application
{
	public static  class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{


			services.AddAutoMapper(typeof(MappingProfile)); // 1
															//services.AddAutoMapper(typeof(MappingProfile).Assembly); // 2- if u have more than one profile 
															//services.AddAutoMapper(M => M.AddProfile<MappingProfile>());// 3

			//*************************************************

			//services.AddScoped<IServiceManager, ServiceManager>();
			services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));
			return services;
		}
	}
}
