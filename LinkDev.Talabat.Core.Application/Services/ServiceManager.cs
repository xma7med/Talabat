using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Products;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services
{

	// Will Intialize using Lazy Intialization  
	// Generate Backing Field 
	// Intialize 

	internal class ServiceManager : IServiceManager
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		private readonly Lazy<IProductService> _productService;	

		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
			_productService = new Lazy<IProductService>(()=>new ProducService(unitOfWork , mapper)); // so i dont want to alow DI to any service i already intialize it here 
		}
		// Will not Initialize till u Access the prop when u Access will Intilaize and if u access next time in same req wil provide the same obj 
		public IProductService ProductService => _productService.Value;


		//Not Good way for Intilaization bec : 1- Cause problem that can in the same req Access ServiceManger Many Time  (In same Block ) so make every time new object [ I can handle this but ]-  2 - If i access it many time In different places in code I cant handle it
		//public IProductService ProductService => new ProducService(unitOfWork , mapper);
	}
}
