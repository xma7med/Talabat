
using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using LinkDev.Talabat.APIs.Extention;
using LinkDev.Talabat.APIs.Middlewares;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infrastructure.Presistance;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{ 

			/// the builder to build ASP.NET Core App
			var webApplicationbuilder = WebApplication.CreateBuilder(args);

			#region Confgure Services 
			// Add services to the container.

			webApplicationbuilder.Services.AddControllers() //Register Required Services by ASP.NET Core --> Web APIs To DI Container 
										 .ConfigureApiBehaviorOptions(options =>
										 {
											 /// 2.1 Second Way to Handle Validation Exceptions By Configuring The Factory That Generate The Validation Response
											 options.SuppressModelStateInvalidFilter = false ;// false = On  true = Off  ==> The Default Action Filter Come from [ApiControoller]
											 options.InvalidModelStateResponseFactory = (actionContext) =>
											 {
												 var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
																		.SelectMany(P => P.Value!.Errors) // Bec every Parameter have many Error ( object )
																		.Select(E => E.ErrorMessage);
												 return new BadRequestObjectResult(new ApiValidationErrorResponse()
												 {
													 Errors = errors
												 });
											 };
										 }) 
										 .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);
			/// 2.2 Second Way to Handle Validation Exceptions By Configuring The Factory That Generate The Validation Response

			webApplicationbuilder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				/// Second Way to Handle Validation Exceptions By Configuring The Factory That Generate The Validation Response
				options.SuppressModelStateInvalidFilter = false;// false = On  true = Off  ==> The Default Action Filter Come from [ApiControoller]
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
										   .SelectMany(P => P.Value!.Errors) // Bec every Parameter have many Error ( object )
										   .Select(E => E.ErrorMessage);
					return new BadRequestObjectResult(new ApiValidationErrorResponse()
					{
						Errors = errors
					});
				};
			}

				);



			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationbuilder.Services.AddEndpointsApiExplorer();
			webApplicationbuilder.Services.AddSwaggerGen();


			// Register Required services for Presistance layer 
			webApplicationbuilder.Services.AddPresistanceServices(webApplicationbuilder.Configuration); // first way
																										//DependencyInjection.AddPresistanceServices(webApplicationbuilder.Services , webApplicationbuilder.Configuration);	// traditional way 
			webApplicationbuilder.Services.AddApplicationServices();
			
			webApplicationbuilder.Services.AddHttpContextAccessor(); // Register All required services for 	HttpContextAccessor Not Only HttpContextAccessor
			webApplicationbuilder.Services.AddScoped(typeof(ILoggedInUserService) , typeof(LoggedInUserService));
			#endregion

			var app = webApplicationbuilder.Build();


			#region DataBase Initializer

			#region Update DataBase & Data Seed -- Canceled -->  Done Refactor  
			//using var scope = app.Services.CreateAsyncScope(); // Create Request
			//var service = scope.ServiceProvider;
			//var storeContextInitializer/*dbcontext*/ = service.GetRequiredService<IStoreContextIntializer/*StoreContext*/>();
			//// Ask Run Time Enviroment for an object from "StoreContext" Services Explictly .
			////***********************************************************************************
			//var loggerfactory = service.GetRequiredService<ILoggerFactory>();
			////var logger = service.GetRequiredService<ILogger<Program>>();
			//try
			//{
			//	/// Refactor Done 1
			//	/// var pendingMigrations = dbcontext.Database.GetPendingMigrations();
			//	///
			//	///if (pendingMigrations.Any())
			//	///	await dbcontext.Database.MigrateAsync(); // Update-DataBase
			//	///
			//	/// //******************************************************************
			//	///  // Data seed
			//	///
			//	///await StoreContextSeed.SeedAsync(dbcontext);

			// Refactor 2 
			//	await storeContextInitializer.InitializeAsync();	
			//	await storeContextInitializer.SeedAsync();



			//}
			//catch (Exception ex)
			//{

			//	var logger = loggerfactory.CreateLogger<Program>();
			//	logger.LogError(ex, "an error has been occured during applying the migration  or the data seed");
			//} 
			#endregion

			// Add Db Initializer & Seed into extention method to WebApplicationBuilder
			await app.InitializeStoreContextAsync();



			#endregion

			#region Configure Kestral Middlewares
			
			app.UseMiddleware<CustomExceptionHandlerMiddleware>();	


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseStaticFiles();	
			app.MapControllers(); 
			#endregion

			app.Run();

		}
	}
}
