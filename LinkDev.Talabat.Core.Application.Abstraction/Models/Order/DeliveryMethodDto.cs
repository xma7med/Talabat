﻿namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Order
{
    public class DeliveryMethodDto
    {
        public int Id { get; set; }
        public required string ShortName { get; set; }
        public required string Description { get; set; }
        public decimal Cost { get; set; }
        public required string DeliveryTime { get; set; }
    }
}
