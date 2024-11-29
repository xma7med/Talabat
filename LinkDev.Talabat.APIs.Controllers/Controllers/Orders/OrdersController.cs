using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Orders
{
    [Authorize]
    public class OrdersController(IServiceManager serviceManager ):BaseApiController
    {
        [HttpPost] // POST : /api/Orders
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderToCreateDto orderDto )
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrderService.CreateOrderAsync( buyerEmail!, orderDto );    
            return Ok( result );    
        }

        [HttpGet] // POST : /api/Orders
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrderService.GetOrdersForUserAsync( buyerEmail! );    
            return Ok( result );
        }


        [HttpGet("{id}")]   // POST : /api/Orders/id

        public async Task<ActionResult<OrderToReturnDto>> GetOrder(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManager.OrderService.GetOrderByIdAsync(buyerEmail!,id);

            return Ok( result );    

        }


        [HttpGet("deliveryMethods")] // GET : /api/Orders/deliveryMethods
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var result = await serviceManager.OrderService.GetDeliveryMethodAsync();
            return Ok( result );    
        }

    }

}
