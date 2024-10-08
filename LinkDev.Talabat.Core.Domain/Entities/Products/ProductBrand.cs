using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Products
{
	public class ProductBrand : BaseAuditableEntity<int>
	{
        public required string  Name { get; set; }


    }
}

        // i dont need to navigate from here + there are infinty loop prop here 
        //public ICollection<Product> Products { get; set; } = new HashSet<Product>();