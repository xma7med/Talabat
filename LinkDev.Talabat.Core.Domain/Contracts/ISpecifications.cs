using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
	/// To Implement i Specification Design Pattern To Build Dynamic Query = Select = GetAll & GetAllById 
	// Need To Identify Signature for every spec  
	// Make Signature for every and each specs 
	// Implement Interface 
	// Make Function that Take DbSet And Specification Object 
	public interface ISpecifications<TEntity , TKey> 
		where TEntity : BaseAuditableEntity<TKey>
		where TKey : IEquatable<TKey>	
	{


		// Maybe Null cause When i Get All There no Criteria 
		public Expression<Func<TEntity , bool>>? Criteria { get; set; }



		// return object cause maybe ( brand or OrderItems) 
		public List<Expression<Func<TEntity, object  /*BaseAuditableEntity*/>>> Includes { get; set; }	

	}
}
