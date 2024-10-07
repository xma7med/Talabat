using LinkDev.Talabat.Core.Domain.Contracts;

namespace LinkDev.Talabat.APIs.Extention
{
	public static class InitializerExtentions
	{
		public static async Task<WebApplication> InitializeStoreContextAsync(this WebApplication app)
		{
			// Ask Run Time Enviroment for an object from "StoreContext" Services Explictly .
			using var scope = app.Services.CreateAsyncScope(); // Create Request
			var service = scope.ServiceProvider;
			var storeContextInitializer/*dbcontext*/ = service.GetRequiredService<IStoreContextIntializer/*StoreContext*/>();

			var loggerfactory = service.GetRequiredService<ILoggerFactory>();
			//var logger = service.GetRequiredService<ILogger<Program>>();
			try
			{
				await storeContextInitializer.InitializeAsync();
				await storeContextInitializer.SeedAsync();
			}
			catch (Exception ex)
			{
				var logger = loggerfactory.CreateLogger<Program>();
				logger.LogError(ex, "an error has been occured during applying the migration  or the data seed");
			}


			// Have to return App 
			return app;
		}
	}
}
