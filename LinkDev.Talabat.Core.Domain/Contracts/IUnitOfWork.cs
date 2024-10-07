using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
	public interface IUnitOfWork:IAsyncDisposable
	{
		
		IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>()
			where TEntity : BaseEntity<Tkey> where Tkey : IEquatable<Tkey>;
			
		Task<int> CompleteAsync();
		#region 3rd way using Lazy ---
		//public IGenericRepository<Product, int> ProductRepository { get; set; }
		//public IGenericRepository<ProductBrand, int> BrandsRepository { get; set; }
		//public IGenericRepository<ProductCategory, int> CategoriesRepository { get; set; }

		//Task<int> CompleteAsync(); 
		#endregion

	}
}
