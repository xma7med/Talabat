﻿using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;

namespace LinkDev.Talabat.Infrastructure.Presistance.Reositories.Generic_Repository
{
	internal static class SpecificationEvaluator<TEntity , TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{

		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
		{
			var query = inputQuery; // Ex : _dbContext.Set<Product>()

			if (spec.Criteria != null)// P => P.Id==10
				query = query.Where(spec.Criteria);

			// query =  _dbContext.Set<Product>().Where(P => P.Id==10);
			// include Experssion 
			// 1.  P => P.Brand 
			// 2.  P=> P.Category
			// ...
			// Expression

			if (spec.OrderByDesc is not null)
				query = query.OrderByDescending(spec.OrderByDesc);
			else if (spec.OrderBy is not null)
				query = query.OrderBy(spec.OrderBy);


			if(spec.IsPagenationEnable)
				query=query.Skip(spec.Skip).Take(spec.Take);


			//								Seed 
			query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

			//Deffered Execution 
			// query = _dbContext.Set<Product>().Where(P => P.Id==10).Include(P => P.Brand );
			// query = _dbContext.Set<Product>().Where(P => P.Id==10).Include(P => P.Brand ).Include( P=> P.Category);


			///string[] Names = { "Ahmed", "Nasr", "Eldien" };
			///string Message = "Hello";
			/// // Using Aggregate to concatenate the names into the Message
			///Message = Names.Aggregate(Message, (str01, str02) => $"{str01} {str02}");



			return query;
		}
	}
}
