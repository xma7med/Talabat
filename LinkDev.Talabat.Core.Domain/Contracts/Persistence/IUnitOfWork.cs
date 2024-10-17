namespace LinkDev.Talabat.Core.Domain.Contracts.Persistence
{
	public interface IUnitOfWork : IAsyncDisposable
    {

        IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>()
            where TEntity : BaseAuditableEntity<Tkey> where Tkey : IEquatable<Tkey>;

        Task<int> CompleteAsync();
        #region 3rd way using Lazy ---
        //public IGenericRepository<Product, int> ProductRepository { get; set; }
        //public IGenericRepository<ProductBrand, int> BrandsRepository { get; set; }
        //public IGenericRepository<ProductCategory, int> CategoriesRepository { get; set; }

        //Task<int> CompleteAsync(); 
        #endregion

    }
}
