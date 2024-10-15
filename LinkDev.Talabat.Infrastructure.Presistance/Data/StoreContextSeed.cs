namespace LinkDev.Talabat.Infrastructure.Presistance.Data
{
	// Canceled After Refactor

	//public class StoreContextSeed 
	//{
	//	public static async Task SeedAsync(StoreContext dbContext)
	//	{
	// Refactor to StoreContextIntializer 
	///if (!dbContext.Brands.Any()) // check if table is empty first 
	///{
	///	// Get Data - Path 
	///	var brandsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Presistance/Data/Seeds/brands.json");
	///	// JsonSerializer to Deserialize from json as string --> object 
	///	var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
	///	if (brands?.Count > 0)
	///	{
	///		///foreach (var brand in brands)
	///		///{
	///		///	await dbContext.Brands.AddAsync(brand);
	///		///}
	///		await dbContext.Set<ProductBrand>().AddRangeAsync(brands);
	///		await dbContext.SaveChangesAsync();	
	///	}
	///}

	///if (!dbContext.Categories.Any())  
	///{
	///	var categoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Presistance/Data/Seeds/categories.json");
	///	var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
	///	if (categories?.Count > 0)
	///	{
	///		await dbContext.Set<ProductCategory>().AddRangeAsync(categories);
	///		await dbContext.SaveChangesAsync();
	///	}
	///}

	///if (!dbContext.Products.Any())
	///{
	///	var productsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Presistance/Data/Seeds/products.json");
	///	var products = JsonSerializer.Deserialize<List<Product>>(productsData);
	///	if (products?.Count > 0)
	///	{
	///		await dbContext.Set<Product>().AddRangeAsync(products);
	///		await dbContext.SaveChangesAsync();
	///	}
	///}



	//	}
	//}
}
