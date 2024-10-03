
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
