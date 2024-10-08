using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Presistance.Reositories
{
    internal class GenaricRepository<TEntity, TKey>(StoreContext DbContext) : IGenericRepository<TEntity, TKey>
		where TEntity : BaseAuditableEntity<TKey>
		where TKey : IEquatable<TKey>

	{
		public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
			=> withTracking ? await DbContext.Set<TEntity>().ToListAsync() : await DbContext.Set<TEntity>().AsNoTracking().ToListAsync();
		// ******** Fix This Enable Eager loading Day03 part 03 14:00
		//{
		//	if (typeof(TEntity) == typeof(Product))
		//	{
		//		withTracking? await DbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync() :
		//			await DbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).AsNoTracking().ToListAsync();

		//	}
		//	withTracking? await DbContext.Set<TEntity>().ToListAsync() :
		//		await DbContext.Set<TEntity>().AsNoTracking().ToListAsync();

		//}



		///{
		///	 if (withTracking)
		///		return await  _dbContext.Set<TEntity>().ToListAsync();	
		///	 return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
		///}


		// ******** Refactor This GetById Enable Eager loading Day03 part 03 14:00
		public async Task<TEntity?> GetAsync(TKey id) => await DbContext.Set<TEntity>().FindAsync(id);
		//{
		//	if (typeof(TEntity) == typeof(Product))

		//		// Return a product with its Brand and Category eagerly loaded
		//		return await DbContext.Set<Product>()
		//							  .Include(P => P.Brand)
		//							  .Include(P => P.Category)
		//							  .FirstOrDefaultAsync(P => P.Id==id) ;
		//	   // For other entities, just use FindAsync (no eager loading)
		//	    return await DbContext.Set<TEntity>().FindAsync(id);


		//}




		public async Task AddAsync(TEntity entity) => await DbContext.Set<TEntity>().AddAsync(entity);
		
		public void Update(TEntity entity) =>DbContext.Set<TEntity>().Update(entity);
		

		public void Delete(TEntity entity) => 	DbContext.Set<TEntity>().Remove(entity);	
		

		

	}
}
