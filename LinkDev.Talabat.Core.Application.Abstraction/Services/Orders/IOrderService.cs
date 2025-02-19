using LinkDev.Talabat.Core.Application.Abstraction.Models.Order;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Orders
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order);
        
        // buyerEmail : To Validate that this order for this buyer 
        Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId);

        Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail);
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync();

    }
}
