using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models
{
	public class ProductToReturnDto
	{
        public int Id { get; set; }
        public required string Name { get; set; }
		public required string Description { get; set; }
		public string? PictureUrl { get; set; }
		public decimal Price { get; set; }


		public int? BrandId { get; set; } // Foriegn Key --> ProductBrand 
		public  /*ProductBrand*/string? Brand { get; set; } // I dont want make nesting (if u want do it ) I only want the name 

		public int? CategoryId { get; set; } // Foriegn Key --> ProductCategory 
		public  /*ProductCategory*/string ? Category { get; set; }
	}
}
