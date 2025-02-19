using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Order;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Exception;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specifications.Orders;

namespace LinkDev.Talabat.Core.Application.Services.Orders
{
    public class OrderService (IBasketService basketService , IUnitOfWork unitOfWork , IMapper mapper ) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
        {
            /// Note : I will trust only 2 info from the client Id , Quantity 
            /// the rest info i will get it from my DB
            
            // 1. Get Basket From Baskets Repo
            var basket = await basketService.GetCustomerBasketAsync(order.BasketId);

            // 2. Get Selected Items at Basket From Products Repo

            var orderItems = new List<OrderItem>();
            if (basket.Items.Count() > 0)
            {
                var productRepository = unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                { 
                    var product = await productRepository.GetAsync(item.Id);

                    if (product != null)
                    { 
                        var productItemOrdered = new ProductItemOrdered()
                    { 
                        ProductId = product.Id, 
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    };

                        var orderItem = new OrderItem() 
                        {
                            Product = productItemOrdered,
                            Price = product.Price,
                            Quantity = item.Quantity,
                        };

                        orderItems.Add(orderItem); 
                    }
                }
            }

            // 3. Calculate SubTotal
            
            var subTotal = orderItems.Sum(item  => item.Price * item.Quantity); // Aggregation 

            // 4. Map Address 

            var address = mapper.Map<Address>(order.ShippingAddress);

            // 5. Get Delivery Method 
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(order.DeliveryMethodId);

            // 6. Create Order

            var orderToCreate = new Order()
            { 
                BuyerEmail = buyerEmail,    
                ShippingAddress = address,  
                Items = orderItems,
                Subtotal = subTotal,    
                DeliveryMethod = deliveryMethod,
            };

            await unitOfWork.GetRepository<Order , int >().AddAsync(orderToCreate);

            // 7. Save To Database

            var created = await unitOfWork.CompleteAsync() > 0;
            if (!created) throw new  BadRequestException("an error occured during creating order ");
            return mapper.Map<OrderToReturnDto>(orderToCreate);

        }


        public async Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderSpecs = new OrderSpecifications(buyerEmail);
            var orders = await unitOfWork.GetRepository<Order, int>().GetWithSpecAsync(orderSpecs);

            return mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            // 2- build specs && make Constructor for this case 
            var orderSpecs = new OrderSpecifications(buyerEmail, orderId);

            // 1- Used Get with Specs Becuase i want to validate over email 
            var order = await unitOfWork.GetRepository<Order, int>().GetWithSpecAsync(orderSpecs);

            // null if not found or not this email order
            if (order is null) throw new NotFoundException(nameof(Order), orderId);
            return mapper.Map<OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

            return mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }


    }
}
