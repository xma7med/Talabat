
using LinkDev.Talabat.Infrastructure.Presistance;
using LinkDev.Talabat.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.APIs
{
	public class Program
	{
		public static void Main(string[] args)
		{

			/// the builder to build ASP.NET Core App
			var webApplicationbuilder = WebApplication.CreateBuilder(args);

			#region Confgure Services 
			// Add services to the container.

			webApplicationbuilder.Services.AddControllers();  // Register Required Services by ASP.NET Core --> Web APIs To DI Container 
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			webApplicationbuilder.Services.AddEndpointsApiExplorer();
			webApplicationbuilder.Services.AddSwaggerGen();


			// Register Required services for Presistance layer 
			webApplicationbuilder.Services.AddPresistanceServices(webApplicationbuilder.Configuration);	// first way
			//DependencyInjection.AddPresistanceServices(webApplicationbuilder.Services , webApplicationbuilder.Configuration);	// traditional way 


			#endregion


			var app = webApplicationbuilder.Build();


			#region Configure Kestral Middlewares

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			//app.UseAuthorization();


			app.MapControllers(); 
			#endregion

			app.Run();

		}
	}
}
