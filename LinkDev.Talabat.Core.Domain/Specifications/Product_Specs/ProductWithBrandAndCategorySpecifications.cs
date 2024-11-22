using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Domain.Specifications.Product_Specs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
	{
		// This Object is Created via this Constructor , Will Be Used for Building the Query that Get All Products 
		public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId , int pageSize, int pageIndex ,string? Search)
			: base(
				  // لان ممكن يسيرش بالكاتيجوري و البراند و الاسم 
				  P=>
				    (string.IsNullOrEmpty(Search) || P.NormalizedName.Contains(Search))
				    &&
				    (!brandId.HasValue    || P.BrandId    == brandId.Value)
				    &&
				    ((!categoryId.HasValue )|| P.CategoryId == categoryId.Value)
				  
				  )
		{
			AddIncludes();

			//AddOrderBy(P => P.Name); // Change the default sorting 

			//if (!string.IsNullOrWhiteSpace(sort))
			//{

			//   Oreder By 

				switch (sort)
				{
					case "nameDesc":
						AddOrderByDesc(P => P.Name);
						break;
				    case "nameAesc":
					    AddOrderBy(P => P.Name);
				    	break;
				    case "priceAsc":
						AddOrderBy(P => P.Price);
						//OrderBy = P => P.Price;
						break;
					case "priceDesc":
						AddOrderByDesc(P => P.Description);
						break;
					default:
					AddOrderBy(P => P.Id); // Change the default sorting 
					break;
				}
			//}
			//else
			//	AddOrderBy(P => P.Name);


			/// Ex
			// totalProducts = 18 ~ 20
			// pageSize      = 5
			// pageIndex     = 3
			ApplyPagenation(pageSize*(pageIndex-1), pageSize);

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
