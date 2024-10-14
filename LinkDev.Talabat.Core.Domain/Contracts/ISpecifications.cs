using System.Linq.Expressions;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
	/// To Implement i Specification Design Pattern To Build Dynamic Query = Select = GetAll & GetAllById 
	// Need To Identify Signature for every spec  
	// Make Signature for every and each specs 
	// Implement Interface 
	// Make Function that Take DbSet And Specification Object --> Specification Evaluator  
	public interface ISpecifications<TEntity , TKey> 
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>	
	{
		/// (Where ) Maybe Null cause When i Get All There no Criteria 
		public Expression<Func<TEntity , bool>>? Criteria { get; set; }
		///(Include) return object cause maybe ( brand(BaseAuditableEntity) or OrderItems(ICollection) So to staticfy all return ==> Object ) 
		public List<Expression<Func<TEntity, object  /*BaseAuditableEntity*/>>> Includes { get; set; }
        /// OrderBy - OrderBy Desc Spects - Take one Parameter of TEntity and return the property that will be ordering Based on it, Can be brand or any prop (object )
        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagenationEnable { get; set; }

    }
}
