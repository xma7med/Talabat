using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Employee;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Order;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Product;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
// Alias Name 
using UserAddress = LinkDev.Talabat.Core.Domain.Entities.Identity.Address;
using OrderAddress = LinkDev.Talabat.Core.Domain.Entities.Orders.Address;

namespace LinkDev.Talabat.Core.Application.Mapping
{
    internal class MappingProfile:Profile
	{
        public MappingProfile()
        {
            // From  A -- to --> B
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, O => O.MapFrom(s => s.Brand!.Name))
                .ForMember(d => d.Category, O => O.MapFrom(s => s.Category!.Name))
                //.ForMember(d => d.PictureUrl , O=> O.MapFrom(s => $"{"https://localhost:7116"}{s.PictureUrl}"));
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
             

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();


			CreateMap<Employee, EmployeeToReturnDto>();

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, option => option.MapFrom(src => src.DeliveryMethod!.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, option => option.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, option => option.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, option => option.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<OrderAddress, AddressDto>().ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodDto>();

            CreateMap<UserAddress, AddressDto>().ReverseMap();

        }
	}
}
