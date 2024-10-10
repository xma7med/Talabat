using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Presistance.Reositories.Generic_Repository
{
    internal class GenaricRepository<TEntity, TKey>(StoreContext _dbContext) : IGenericRepository<TEntity, TKey>
		where TEntity : BaseAuditableEntity<TKey>
		where TKey : IEquatable<TKey>

	{
		public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
		//=> withTracking ? await DbContext.Set<TEntity>().ToListAsync() : await DbContext.Set<TEntity>().AsNoTracking().ToListAsync();
		// ******** Fixed --> but da mosakn l2n msh by722 el [open for extention close for modification]
		{
			/// will use specification Design pattern to achieve ==> [open for extention close for modification]
			//if (typeof(TEntity) == typeof(Product)) 
			//{
			//	return withTracking? (IEnumerable<TEntity>) (await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync() ):
			//		(IEnumerable<TEntity>)(await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).AsNoTracking().ToListAsync());

			//}

			//  for other entities Brand - Category ...
			return withTracking ? 
				await _dbContext.Set<TEntity>().ToListAsync() :
				await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync(); 

		}


		///{
		///	 if (withTracking)
		///		return await  _dbContext.Set<TEntity>().ToListAsync();	
		///	 return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
		///}


		// ********Fixed but this not Consider O in Solid The sol is Specification Design Pattern 
		public async Task<TEntity?> GetAsync(TKey id)  /*=> await DbContext.Set<TEntity>().FindAsync(id);*/ //1
		{
			/// will use specification Design pattern to achieve ==> [open for extention close for modification]
			//if (typeof(TEntity) == typeof(Product))

			//	// Return a product with its Brand and Category eagerly loaded
			//	return await _dbContext.Set<Product>().Where(P => P.Id.Equals(id))
			//						  .Include(P => P.Brand)
			//						  .Include(P => P.Category).FirstOrDefaultAsync() as TEntity;

			// For other entities, just use FindAsync (no eager loading)
			return await _dbContext.Set<TEntity>().FindAsync(id);


		}


		public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync (ISpecifications<TEntity, TKey> spec, bool withTracking = false)
		{//       -------------------------Qurery------------------------------------------------ .ToListAsync()
			return await  /*SpecificationEvaluator<TEntity, TKey>.GetQuery(DbContext.Set<TEntity>() , spec)*/ApplySpecifications( spec).ToListAsync();
		}
		public async Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec)
		{
			return await ApplySpecifications( spec).FirstOrDefaultAsync();		
		}




		public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);
		
		public void Update(TEntity entity) =>_dbContext.Set<TEntity>().Update(entity);
		

		public void Delete(TEntity entity) => 	_dbContext.Set<TEntity>().Remove(entity);


		#region Helpers

		private  IQueryable<TEntity> ApplySpecifications( ISpecifications<TEntity, TKey> spec)
		{
			return SpecificationEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec);
		}


		#endregion


	}
}
