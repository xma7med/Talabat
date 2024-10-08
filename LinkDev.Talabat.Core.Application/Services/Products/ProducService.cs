using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Application.Services.Products
{
    // Remember I dont Neeed It PUBLIC OutSide layer dependOn ABSTRACTION NOT IMPLEMENTATION 
    // INJECT MAPPER  - Allow DI for IMapper 
    internal class ProducService(IUnitOfWork unitofWork, IMapper mapper) : IProductService
    {

        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
         => mapper.Map<IEnumerable<ProductToReturnDto>>(await unitofWork.GetRepository<Product, int>().GetAllAsync());

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            return mapper.Map<ProductToReturnDto>(await unitofWork.GetRepository<Product, int>().GetAsync(id));

        }


        // you can make Enhancement -- Lookup(Search)
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
            => mapper.Map<IEnumerable<BrandDto>>(await unitofWork.GetRepository<ProductBrand, int>().GetAllAsync());


        public async Task<IEnumerable<CategoryDto>> GetcategoriesAsync()
          => mapper.Map<IEnumerable<CategoryDto>>(await unitofWork.GetRepository<ProductCategory, int>().GetAllAsync());


    }
}
