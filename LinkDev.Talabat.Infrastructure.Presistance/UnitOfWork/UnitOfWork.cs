using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infrastructure.Presistance.Data;
using LinkDev.Talabat.Infrastructure.Presistance.Reositories.Generic_Repository;
using System.Collections.Concurrent;

namespace LinkDev.Talabat.Infrastructure.Presistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork 
	{
		private readonly StoreDbContext _dbContext;

		private readonly ConcurrentDictionary<string, object> _repositories;
		// Common way to implement Unit Of Work 

		public UnitOfWork(StoreDbContext dbContext)
        {
			_dbContext = dbContext;
			               // syntax suger .net  c# 9
			_repositories = new /*ConcurrentDictionary<string, object>*/();
		}

        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>()
			where TEntity : BaseEntity<Tkey>
			where Tkey : IEquatable<Tkey>
		{
			///1- هتبقى مشكله لو نادى ع الميثود اكتر من مره بنفس التايب هيعمله اكتر من اوبجكت
			///return new GenaricRepository<TEntity,Tkey>(_dbContext);	

			///2- with Asyncrounas programming we use ConcurrentCollections
			///var typeName = typeof(TEntity).Name; // For Ex : Product
		    ///if (_repositories.ContainsKey(typeName)) return (IGenericRepository<TEntity, Tkey>) _repositories[typeName];
			///
			///var repository = new GenaricRepository<TEntity,Tkey>(_dbContext);
			///_repositories.Add(typeName, repository);
			///
			///return repository;

			return (IGenericRepository<TEntity, Tkey>)_repositories.GetOrAdd(typeof(TEntity).Name , new GenaricRepository<TEntity,Tkey> (_dbContext));

		}
		public Task<int> CompleteAsync()=> _dbContext.SaveChangesAsync();	

		public async ValueTask DisposeAsync()=> await _dbContext.DisposeAsync();	
		
		
		
		
		
		#region 3rd Way to implement Unit Of Work using Lazy Initialization  
		//private readonly StoreContext _dbContext;

		//private readonly Lazy<IGenericRepository<Product, int> >_ProductRepository;
		//private readonly Lazy<IGenericRepository<ProductBrand, int> >_brandRepository;
		//private readonly Lazy<IGenericRepository<ProductCategory, int> > _categoryRepository;

		//public UnitOfWork(StoreContext  dbContext)
		//      {
		//	_dbContext = dbContext;
		//	_ProductRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenaricRepository<Product,int>(_dbContext));
		//	_brandRepository = new Lazy<IGenericRepository<ProductBrand, int>>(()=> new GenaricRepository<ProductBrand, int>(_dbContext));
		//	_categoryRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenaricRepository<ProductCategory, int>(_dbContext));
		//}
		//public IGenericRepository<Product, int> ProductRepository => _ProductRepository.Value;/*new GenaricRepository<Product, int>(_dbContext);*/
		//public IGenericRepository<ProductBrand, int> BrandsRepository => _brandRepository.Value; /*new GenaricRepository<ProductBrand, int>(_dbContext);*/
		//public IGenericRepository<ProductCategory, int> CategoriesRepository => _categoryRepository.Value;/*new GenaricRepository<ProductCategory, int>(_dbContext);*/



		//public Task<int> CompleteAsync()
		//{
		//	throw new NotImplementedException();
		//}

		//public ValueTask DisposeAsync()
		//{
		//	throw new NotImplementedException();
		//} 
		#endregion

	}
}
