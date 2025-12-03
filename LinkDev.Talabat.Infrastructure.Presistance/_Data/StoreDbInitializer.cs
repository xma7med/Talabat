using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Presistance.Common;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Presistance.Data
{
	internal sealed class StoreDbInitializer(StoreDbContext _dbContext) :DbInitializer(_dbContext) , IStoreDbIntializer
	{
		// Asked using Primary Constructor  
		///private readonly StoreContext _dbContext;
		///public StoreContextInitializer(StoreContext dbContext)
	    ///{
		///	_dbContext = dbContext;
		///}
       //      public async Task InitializeAsync() // Update- Database
		//{
		//	var pendingMigrations =await  _dbContext.Database.GetPendingMigrationsAsync();

		//	if (pendingMigrations.Any())
		//		await _dbContext.Database.MigrateAsync(); // Update-DataBase
		//}

		public override async Task SeedAsync() // Seeds
		{
			if (!_dbContext.Brands.Any()) // check if table is empty first 
			{
				// Get Data - Path 
				var brandsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Presistance/_Data/Seeds/brands.json");
                // JsonSerializer to Deserialize JSON as string into a .NET object
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
				if (brands?.Count > 0)
				{
					///foreach (var brand in brands)
					///{
					///	await _dbContext.Brands.AddAsync(brand);
					///}
					await _dbContext.Set<ProductBrand>().AddRangeAsync(brands);
					await _dbContext.SaveChangesAsync();
				}

			}

			if (!_dbContext.Categories.Any())
			{
				var categoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Presistance/_Data/Seeds/categories.json");
				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
				if (categories?.Count > 0)
				{
					await _dbContext.Set<ProductCategory>().AddRangeAsync(categories);
					await _dbContext.SaveChangesAsync();
				}

			}


			if (!_dbContext.Products.Any())
			{
				var productsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Presistance/_Data/Seeds/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productsData);
				if (products?.Count > 0)
				{
					await _dbContext.Set<Product>().AddRangeAsync(products);
					await _dbContext.SaveChangesAsync();
				}

			}


            if (!_dbContext.DeliveryMethods.Any())
            {
                var deliveryMethodsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Presistance/_Data/Seeds/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
                if (deliveryMethods?.Count > 0)
                {
                    await _dbContext.Set<DeliveryMethod>().AddRangeAsync(deliveryMethods);
                    await _dbContext.SaveChangesAsync();
                }

            }


        }
    }
}
