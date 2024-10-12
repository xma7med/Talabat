﻿using LinkDev.Talabat.Core.Application.Abstraction.Models.Product;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Products
{
	public interface IProductService
    {
        Task<IEnumerable<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams);

        Task<ProductToReturnDto> GetProductAsync(int id);

        Task<IEnumerable<BrandDto>> GetBrandsAsync();

        Task<IEnumerable<CategoryDto>> GetcategoriesAsync();



    }
}
