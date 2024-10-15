using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Basket
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; } // Will be the same product Id
		[Required (ErrorMessage ="Product Name Is Required ")]
		public required string ProductName { get; set; }
		public string? PictureUrl { get; set; }
		[Required]
		[Range(.1,double.MaxValue , ErrorMessage ="Price must be greater than Zero ")]
		public decimal Price { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 1 ")]
		public int Quantity { get; set; }
		public string? Brand { get; set; }
		public string? Category { get; set; }
	}
}
