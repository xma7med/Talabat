﻿using LinkDev.Talabat.Core.Application.Abstraction.Models.Common;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Order
{
    // Take Basket , put item , checkout 
    public class OrderToCreateDto
    {
        public required string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public required AddressDto ShippingAddress { get; set; }

    }
}
