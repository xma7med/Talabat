using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction
{

    // Signiture for Every Service 
    // Public so Controllers Can Reach 
    public interface IServiceManager
	{
		public IProductService ProductService { get; }	
	}
}
