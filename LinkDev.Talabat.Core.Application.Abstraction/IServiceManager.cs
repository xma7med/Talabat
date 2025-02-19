using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;

namespace LinkDev.Talabat.Core.Application.Abstraction
{

	// Signiture for Every Service 
	// Public so Controllers Can Reach 
	public interface IServiceManager
	{
		public IProductService ProductService { get; }
        public IBasketService BasketService { get; }

        public IAuthService AuthService { get; }
        public IOrderService OrderService { get; }
        public IEmployeeServices EmployeeService { get; }
        public IDepartmentService DepartmentService { get; }

    }
}
