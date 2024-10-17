using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;

namespace LinkDev.Talabat.Core.Application.Abstraction
{

	// Signiture for Every Service 
	// Public so Controllers Can Reach 
	public interface IServiceManager
	{
		public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
    }
}
