using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Product;
using LinkDev.Talabat.Core.Domain.Entities.Products;

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
        }
    }
}
