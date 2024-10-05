using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Products
{
	public class Product
	{
        public required string  Name { get; set; }
        public required string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }

        
        public int? BrandId { get; set; } // Foriegn Key --> ProductBrand 
        public virtual ProductBrand? Brand { get; set; }

        public int? CategoryId { get; set; } // Foriegn Key --> ProductCategory 
		public virtual ProductCategory? Category { get; set; }
    }
}
