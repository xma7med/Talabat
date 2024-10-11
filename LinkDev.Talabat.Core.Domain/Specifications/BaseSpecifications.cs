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
		// Where 
		public Expression<Func<TEntity, bool>>? Criteria { get; set; } = null; 
		// Includes 
		public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new /*List<Expression<Func<TEntity, object>>>*/();


		// Use it to  build specification object to build query that Get All Items 
		public BaseSpecifications()
        {
			//Criteria = null;	
			//Includes = new List<Expression<Func<TEntity, object>>>();

		}

		// Use it to build specification object to build query that Get All Items 
		public BaseSpecifications(TKey id )
        {
			Criteria = E=>E.Id.Equals(id);
			//Includes = new List<Expression<Func<TEntity, object>>>();
		}
	}
}
