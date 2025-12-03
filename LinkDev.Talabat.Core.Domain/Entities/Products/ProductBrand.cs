namespace LinkDev.Talabat.Core.Domain.Entities.Products
{
	public class ProductBrand : BaseAuditableEntity<int>
	{
        public required string  Name { get; set; }


    }
}

        // i dont need to navigate from here + there are infinty loop prop here 
        //public ICollection<Product> Products { get; set; } = new HashSet<Product>();