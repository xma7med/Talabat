﻿namespace LinkDev.Talabat.Core.Domain.Entities.Basket
{
	public class BasketItem
	{
		public int Id { get; set; } // Will be the same product Id
		public required string ProductName { get; set; }
		public string? PictureUrl { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public string? Brand { get; set; }
		public string? Category { get; set; }

	}
}
