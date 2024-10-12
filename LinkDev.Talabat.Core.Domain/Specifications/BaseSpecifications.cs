﻿using LinkDev.Talabat.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications
{
	public abstract  class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
		where TEntity : BaseAuditableEntity<TKey>
		where TKey : IEquatable<TKey>	
	{
		public Expression<Func<TEntity, bool>>? Criteria { get; set; } = null; 
		public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new /*List<Expression<Func<TEntity, object>>>*/();
		public Expression<Func<TEntity, object>>? OrderBy { get; set ; } = null; 
		public Expression<Func<TEntity, object>>? OrderByDesc { get ; set ; } = null;


        protected BaseSpecifications()
        {
            
        }


		// Use it to  build specification object to build query that Get All Items 
		protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteriaExpression)
        {

			Criteria = criteriaExpression; //  P=>P.BrandId==2 && true

			//Criteria = null;	
			//Includes = new List<Expression<Func<TEntity, object>>>();

		}

		// Use it to build specification object to build query that Get All Items 
		protected BaseSpecifications(TKey id )
        {
			Criteria = E=>E.Id.Equals(id);
			//Includes = new List<Expression<Func<TEntity, object>>>();
		}



		/// Helpers Method 
		private protected virtual void AddOrderBy(Expression<Func<TEntity, object>> orderbyExpression)
		{ 
			OrderBy = orderbyExpression;
		}
		private protected virtual void AddOrderByDesc(Expression<Func<TEntity, object>> orderbyExpressionDesc)
		{
			OrderByDesc = orderbyExpressionDesc;
		}

		private protected virtual void AddIncludes()
		{
			
		}

	}
}
