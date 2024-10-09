﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Product;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specifications;
using LinkDev.Talabat.Core.Domain.Specifications.Product_Specs;

namespace LinkDev.Talabat.Core.Application.Services.Products
{
    // Remember I dont Neeed It PUBLIC OutSide layer dependOn ABSTRACTION NOT IMPLEMENTATION 
    // INJECT MAPPER  - Allow DI for IMapper 
    internal class ProducService(IUnitOfWork unitofWork, IMapper mapper) : IProductService
    {

        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
        { 
            //var specs = new BaseSpecifications<Product , int >();   
            var specs = new ProductWithBrandAndCategorySpecifications();
            var products =mapper.Map<IEnumerable<ProductToReturnDto>>(await unitofWork.GetRepository<Product, int>().GetAllWithSpecAsync(specs));
            return products;    
		}
		//=> mapper.Map<IEnumerable<ProductToReturnDto>>(await unitofWork.GetRepository<Product, int>().GetAllAsync());

		public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
			var specs = new ProductWithBrandAndCategorySpecifications(id);  // Creating specifications object with the product id
			var product = await unitofWork.GetRepository<Product, int>().GetWithSpecAsync(specs);  // Using the specification to get the product asynchronously
			var mappedProduct = mapper.Map<ProductToReturnDto>(product);  // Mapping the product to a DTO (ProductToReturnDto)

			return mappedProduct;  

		}


        // you can make Enhancement -- Lookup(Search)
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
			var brands = await unitofWork.GetRepository<ProductBrand, int>().GetAllAsync();
			var brandsToReturn = mapper.Map<IEnumerable<BrandDto>>(brands);

			return brandsToReturn;
		}
            //=> mapper.Map<IEnumerable<BrandDto>>(await unitofWork.GetRepository<ProductBrand, int>().GetAllAsync());


        public async Task<IEnumerable<CategoryDto>> GetcategoriesAsync()
          => mapper.Map<IEnumerable<CategoryDto>>(await unitofWork.GetRepository<ProductCategory, int>().GetAllAsync());


    }
}
