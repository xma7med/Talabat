using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Services.Employees;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Services
{

	// Will Intialize using Lazy Intialization  
	// Generate Backing Field 
	// Intialize 

	internal class ServiceManager : IServiceManager
	{
		private readonly IUnitOfWork _unitOfWork ;
		private readonly IMapper _mapper ;

		private readonly Lazy<IProductService> _productService;
		private readonly Lazy<IEmployeeService> _employeeService;
		private readonly Lazy<IBasketService> _basketService;
		private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IOrderService> _orderService;


        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration,
			Func<IOrderService> orderServiceFactory, Func<IBasketService> basketServiceFactory , Func<IAuthService> authServiceFactory)
        {
			_unitOfWork = unitOfWork;
			_mapper= mapper;
			_productService =   new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
			_employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(unitOfWork, mapper));
            _orderService = new Lazy<IOrderService>(orderServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _basketService = new Lazy<IBasketService>(basketServiceFactory , LazyThreadSafetyMode.ExecutionAndPublication);
			_authService = new Lazy<IAuthService>(authServiceFactory , LazyThreadSafetyMode.ExecutionAndPublication);
		}
        // Will not Initialize till u Access the prop when u Access will Intilaize and if u access next time in same req wil provide the same obj 
        public IProductService ProductService => _productService.Value;
		public IEmployeeService EmployeeService => _employeeService.Value;
		public IBasketService BasketService => _basketService.Value;
		public IAuthService AuthService => _authService.Value;

        public IOrderService OrderService => _orderService.Value;


        //Not Good way for Intilaization bec : 1- Cause problem that can in the same req Access ServiceManger Many Time  (In same Block ) so make every time new object [ I can handle this but ]-  2 - If i access it many time In different places in code I cant handle it
        //public IProductService ProductService => new ProducService(unitOfWork , mapper);
    }
}
