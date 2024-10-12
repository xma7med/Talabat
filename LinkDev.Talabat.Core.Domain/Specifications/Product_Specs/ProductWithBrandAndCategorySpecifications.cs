using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Product_Specs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
	{
		// This Object is Created via this Constructor , Will Be Used for Building the Query that Get All Products 
		public ProductWithBrandAndCategorySpecifications(string? sort)
			: base()
		{
			AddIncludes();

			AddOrderBy(P => P.Name); // Change the default sorting 

			if (!string.IsNullOrWhiteSpace(sort))
			{
				switch (sort)
				{
					case "nameDesc":
						AddOrderByDesc(P => P.Name);
						break;	
					case "priceAsc":
						AddOrderBy(P => P.Price);
						//OrderBy = P => P.Price;
						break;
					case "priceDesc":
						AddOrderByDesc(P => P.Description);
						break;
					default:
						break;
				}
			}
			//else
			//	AddOrderBy(P => P.Name);

		}



		// This Object is Created via this Constructor , Will Be Used for Building the Query that Get a specific  Product 

		public ProductWithBrandAndCategorySpecifications(int Id)
			: base(Id)
		{
			AddIncludes();
		}


		private protected override void AddIncludes()
		{
			base.AddIncludes();

			Includes.Add(P => P.Brand!);
			Includes.Add(P => P.Category!);
		}


	}
}
