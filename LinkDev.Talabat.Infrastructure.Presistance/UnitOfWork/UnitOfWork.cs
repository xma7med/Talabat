using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Presistance.Data;
using LinkDev.Talabat.Infrastructure.Presistance.Reositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Presistance.UnitOfWork
{
	public  class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;

		private readonly Lazy<IGenericRepository<Product, int> >_ProductRepository;
		private readonly Lazy<IGenericRepository<ProductBrand, int> >_brandRepository;
		private readonly Lazy<IGenericRepository<ProductCategory, int> > _categoryRepository;

		public UnitOfWork(StoreContext  dbContext)
        {
			_dbContext = dbContext;
			_ProductRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenaricRepository<Product,int>(_dbContext));
			_brandRepository = new Lazy<IGenericRepository<ProductBrand, int>>(()=> new GenaricRepository<ProductBrand, int>(_dbContext));
			_categoryRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenaricRepository<ProductCategory, int>(_dbContext));
		}
		public IGenericRepository<Product, int> ProductRepository => _ProductRepository.Value;/*new GenaricRepository<Product, int>(_dbContext);*/
		public IGenericRepository<ProductBrand, int> BrandsRepository => _brandRepository.Value; /*new GenaricRepository<ProductBrand, int>(_dbContext);*/
		public IGenericRepository<ProductCategory, int> CategoriesRepository => _categoryRepository.Value;/*new GenaricRepository<ProductCategory, int>(_dbContext);*/



		public Task<int> CompleteAsync()
		{
			throw new NotImplementedException();
		}

		public ValueTask DisposeAsync()
		{
			throw new NotImplementedException();
		}
	}
}
