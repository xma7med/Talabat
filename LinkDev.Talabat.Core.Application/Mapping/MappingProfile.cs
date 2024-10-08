﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models;
using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Application.Mapping
{
	internal class MappingProfile:Profile
	{
        public MappingProfile()
        {
            // From  A -- to --> B
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand , O => O.MapFrom(s => s.Brand.Name) )
                .ForMember(d => d.Category , O => O.MapFrom(s => s.Category.Name) );


            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();
        }
    }
}
