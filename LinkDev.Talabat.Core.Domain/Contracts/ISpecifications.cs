using System.Linq.Expressions;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
	/// To Implement i Specification Design Pattern To Build Dynamic Query = Select = GetAll & GetAllById 
	// Need To Identify Signature for every spec  
	// Make Signature for every and each specs 
	// Implement Interface 
	// Make Function that Take DbSet And Specification Object 
	public interface ISpecifications<TEntity , TKey> 
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>	
	{

		// Maybe Null cause When i Get All There no Criteria 
		public Expression<Func<TEntity , bool>>? Criteria { get; set; }

		// return object cause maybe ( brand or OrderItems) 
		public List<Expression<Func<TEntity, object  /*BaseAuditableEntity*/>>> Includes { get; set; }

        /// OrderBy - OrderBy Desc Spects

        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagenationEnable { get; set; }

    }
}
