﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Services.Employees;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;

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
		private readonly Lazy<IEmployeeService> _employeeService;	

		public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
			_productService = new Lazy<IProductService>(()=>new ProductService(unitOfWork , mapper)); // so i dont want to alow DI to any service i already intialize it here 
			_employeeService = new Lazy<IEmployeeService>(()=>new EmployeeService(unitOfWork , mapper)); // 
		}
		// Will not Initialize till u Access the prop when u Access will Intilaize and if u access next time in same req wil provide the same obj 
		public IProductService ProductService => _productService.Value;
		public IEmployeeService EmployeeService => _employeeService.Value;


		//Not Good way for Intilaization bec : 1- Cause problem that can in the same req Access ServiceManger Many Time  (In same Block ) so make every time new object [ I can handle this but ]-  2 - If i access it many time In different places in code I cant handle it
		//public IProductService ProductService => new ProducService(unitOfWork , mapper);
	}
}
