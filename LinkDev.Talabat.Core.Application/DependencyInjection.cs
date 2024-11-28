using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
using LinkDev.Talabat.Core.Application.Services.Basket;
using LinkDev.Talabat.Core.Application.Services.Orders;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Core.Application
{
	public static  class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{


			services.AddAutoMapper(typeof(MappingProfile)); // 1
															//services.AddAutoMapper(typeof(MappingProfile).Assembly); // 2- if u have more than one profile Get All Profiles Inherit from profile in all project  
															//services.AddAutoMapper(M => M.AddProfile<MappingProfile>());// 3 same like 1 create obj from MappingProfile using Parameter less Constructor 
			services.AddAutoMapper(Mapper => Mapper.AddProfile(new MappingProfile() )); // 4 if i want send smth in the cons 

			//*************************************************
			//services.AddScoped(typeof(IProductService) , typeof(ProducService)); I dont need bec i make the obj bymyself in The service manger .. 
			//services.AddScoped<IServiceManager, ServiceManager>();
			services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));


			/// For Service manger To give the req serv for BasketService 
			/// first way 
			//services.AddScoped(typeof(IBasketService), typeof(BasketService));	// 1
			//services.AddScoped(typeof(Func<IBasketService>) , typeof(Func<BasketService>));  // To Register Func<BasketService> Only  But BasketService need some required services so do 1
			/// second way
            services.AddScoped(typeof(IBasketService), typeof(BasketService));
            services.AddScoped(typeof(Func<IBasketService>) , (serviceProvider) =>
			{
				//var basketRepository = serviceProvider.GetRequiredService<IBasketRepository>();
				//var mapper = serviceProvider.GetRequiredService<IMapper>();
				//var configuration = serviceProvider.GetRequiredService<IConfiguration>();		
				//return () => new BasketService(basketRepository , mapper , configuration);
				return () => serviceProvider.GetService<IBasketService>();
			});



            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(Func<IOrderService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IOrderService>();
            });

            return services;
		}
	}
}
