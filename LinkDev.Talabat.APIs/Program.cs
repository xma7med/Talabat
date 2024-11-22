using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using LinkDev.Talabat.APIs.Extention;
using LinkDev.Talabat.APIs.Middlewares;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infrastructure;
using LinkDev.Talabat.Infrastructure.Presistance;
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
											 ///  modifying the ApiBehaviorOptions
											 options.SuppressModelStateInvalidFilter = false ;// false = On  true = Off  ==> The Default Action Filter Come from [ApiControoller]
											 options.InvalidModelStateResponseFactory = (actionContext) =>
											 {
												 var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
																		.Select(P => new ApiValidationErrorResponse.ValidationError() 
																		{
																			Field =P.Key,
																			Errors = P.Value!.Errors.Select(E => E.ErrorMessage)
																		});
																		//.SelectMany(P => P.Value!.Errors) // Bec every Parameter have many Error ( object )
																		//.Select(E => E.ErrorMessage);
												 return new BadRequestObjectResult(new ApiValidationErrorResponse()
												 {
													 Errors = errors
												 });
											 };
										 }) 
										 .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);
			// 2.2 Second Way to Handle Validation Exceptions By Configuring The Factory That Generate The Validation Response
			///webApplicationbuilder.Services.Configure<ApiBehaviorOptions>(options =>
			///{
			///	/// Second Way to Handle Validation Exceptions By Configuring The Factory That Generate The Validation Response
			///	options.SuppressModelStateInvalidFilter = false;// false = On  true = Off  ==> The Default Action Filter Come from [ApiControoller]
			///	options.InvalidModelStateResponseFactory = (actionContext) =>
			///	{
			///		var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
			///							   .SelectMany(P => P.Value!.Errors) // Bec every Parameter have many Error ( object )
			///							   .Select(E => E.ErrorMessage);
			///		return new BadRequestObjectResult(new ApiValidationErrorResponse()
			///		{
			///			Errors = errors
			///		});
			///	};
			///}
			///	);



			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationbuilder.Services.AddEndpointsApiExplorer();
			webApplicationbuilder.Services.AddSwaggerGen();

			webApplicationbuilder.Services.AddHttpContextAccessor(); // Register All required services for 	HttpContextAccessor Not Only HttpContextAccessor
			webApplicationbuilder.Services.AddScoped(typeof(ILoggedInUserService) , typeof(LoggedInUserService));



			webApplicationbuilder.Services.AddApplicationServices();
			// Register Required services for Presistance layer 
			webApplicationbuilder.Services.AddPresistanceServices(webApplicationbuilder.Configuration); // first way
			webApplicationbuilder.Services.AddInfrastructureServices(webApplicationbuilder.Configuration);
			//DependencyInjection.AddPresistanceServices(webApplicationbuilder.Services , webApplicationbuilder.Configuration);	// traditional way 

			webApplicationbuilder.Services.AddIdentityServices(webApplicationbuilder.Configuration);

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
			await app.InitializeDbAsync();



			#endregion

			#region Configure Kestral Middlewares
			
			app.UseMiddleware<ExceptionHandlerMiddleware>();	


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseStatusCodePagesWithReExecute("/Errors/{0}"); // middlware for not fount , bad , unAut - when status code not 200
			// will return the req and execute this endpoint 

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseStaticFiles();	
			app.MapControllers(); 
			#endregion

			app.Run();

		}
	}
}



// Some reminders 

///**************************************************************************************************************************************************************************************//


///This code snippet is customizing how the API handles invalid model states by modifying the ApiBehaviorOptions in ASP.NET Core. Let me break it down for you:

///SuppressModelStateInvalidFilter = false;:

///This property is used to control whether the framework's default behavior of automatically returning a 400 Bad Request when the model state is invalid is suppressed or not.
///By setting it to false, you are keeping the default behavior active. This means that if there is a validation error in the incoming request, the framework will automatically respond with a 400 Bad Request.
///InvalidModelStateResponseFactory:

///This delegate is used to customize the response returned when the model state is invalid.
///Here, it's being set to a lambda function (actionContext) => {} that defines how the response will be built when there is a validation error.
///Extracting Validation Errors:

///Inside the lambda function, the code filters through actionContext.ModelState to find all the model validation errors.
///actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0) filters the properties in the model state that have validation errors.
///SelectMany(P => P.Value!.Errors) extracts the individual error messages for each property.
///Select(E => E.ErrorMessage) selects the actual error message text.
///Returning a Custom Error Response:

///After collecting the errors, it creates a BadRequestObjectResult (which is the 400 Bad Request response).
///The BadRequestObjectResult wraps a custom response object, in this case, an ApiValidationErrorResponse.
///The ApiValidationErrorResponse includes the list of error messages (Errors = errors) gathered from the model state.
///Summary:
///This code customizes the behavior of your API when it encounters invalid model states. Instead of using the default error message format, it collects the validation error messages and returns a structured response (ApiValidationErrorResponse) with those errors. This gives you more control over how your API communicates validation errors back to the client.


//.ConfigureApiBehaviorOptions(options =>
// {
//  /// 2.1 Second Way to Handle Validation Exceptions By Configuring The Factory That Generate The Validation Response
//  ///  modifying the ApiBehaviorOptions
//  options.SuppressModelStateInvalidFilter = false;// false = On  true = Off  ==> The Default Action Filter Come from [ApiControoller]
//  options.InvalidModelStateResponseFactory = (actionContext) =>
//  {
//	  var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
//							 .SelectMany(P => P.Value!.Errors) // Bec every Parameter have many Error ( object )
//							 .Select(E => E.ErrorMessage);
//	  return new BadRequestObjectResult(new ApiValidationErrorResponse()
//	  {
//		  Errors = errors
//	  });
//  };
// }) 


///**************************************************************************************************************************************************************************************//


