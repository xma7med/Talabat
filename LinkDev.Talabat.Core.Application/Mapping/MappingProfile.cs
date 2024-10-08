using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models;
using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Application.Mapping
{
	internal class MappingProfile:Profile
	{
        public MappingProfile()
        {
            // From  A -- to --> B
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();
            CreateMap<Product, ProductToReturnDto>();
        }
    }
}
