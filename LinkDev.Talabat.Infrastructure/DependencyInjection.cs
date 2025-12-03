using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Infrastructure.Basket_Repository;
using LinkDev.Talabat.Infrastructure.Cach_Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace LinkDev.Talabat.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(typeof(IConnectionMultiplexer), (serviceProvider) =>
			{
				var connectionString = configuration.GetConnectionString("Redis");
				var connectionMultiplexerObj = ConnectionMultiplexer.Connect(connectionString!);
				return connectionMultiplexerObj;
			});

			// SingleTon Bec i want it to live untill the session is treminated 
			services.AddSingleton(typeof(IResponseCasheService) , typeof(ResponseCasheService));	
			services.AddScoped (typeof(IBasketRepository), typeof(BasketRepository));

			//services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));

			return services;
		}

	}
}
