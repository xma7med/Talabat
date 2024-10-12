using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductsController(IServiceManager serviceManger) : ApiControllerBase
	{

		[HttpGet] // Get :  /api/Products
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
		{ 
			var products = await serviceManger.ProductService.GetProductsAsync(specParams);
			return Ok(products);
		}

		[HttpGet("{id:int}")] //  Get :  /api/Products/id
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProduct(int id)
		{ 
			var product = await serviceManger.ProductService.GetProductAsync(id);	

			if (product == null)
				return NotFound();	

			return Ok(product);
		}

		[HttpGet("brands")] // Get :  /api/Products/brands
		public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
		{ 
			var brands = await serviceManger.ProductService.GetBrandsAsync();	
			return Ok(brands);
		}

		[HttpGet("categories")] // Get :  /api/Products/categories
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
		{ 
			var categories = await serviceManger.ProductService.GetcategoriesAsync();	
			return Ok(categories);
		}






	}
}
