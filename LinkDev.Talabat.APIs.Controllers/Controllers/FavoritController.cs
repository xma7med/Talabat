using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Product;
using LinkDev.Talabat.Core.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers
{
    public class FavoritController(IServiceManager serviceManager) : BaseApiController
    {

        [HttpGet("{id:int}")] //  Get :  /api/Products/id
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> AddProductToFavorit(int id)
        {
            // User Id 
            // Product Id 

            var product = await serviceManager.ProductService.GetProductAsync(id);

           
            //if (product == null)
            //	return NotFound(new ApiResponse(400, $"The product with id : {id} is not found "));	

            return Ok(product);
        }
    }
}
