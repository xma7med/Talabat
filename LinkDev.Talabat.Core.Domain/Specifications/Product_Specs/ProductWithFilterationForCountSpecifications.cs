using LinkDev.Talabat.Core.Domain.Entities.Products;

namespace LinkDev.Talabat.Core.Domain.Specifications.Product_Specs
{
	public class ProductWithFilterationForCountSpecifications : BaseSpecifications<Product, int>
	{
        public ProductWithFilterationForCountSpecifications(int? brandId, int? categoryId , string? Search)
            : base(

				  P =>
				   (string.IsNullOrEmpty(Search) || P.NormalizedName.Contains(Search))
					&&
				   ((!brandId.HasValue) || P.BrandId == brandId.Value)
					&&
				   ((!categoryId.HasValue) || P.CategoryId == categoryId.Value)
				  ) 
        {
            
        }
    }
}
