using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Order;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Product;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Mapping
{
	internal class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
        private readonly IConfiguration configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.Product.PictureUrl))
				return $"{configuration["Urls:ApiBaseUrl"]}/{source.Product.PictureUrl}";
			return string.Empty;
		}
	}
}
