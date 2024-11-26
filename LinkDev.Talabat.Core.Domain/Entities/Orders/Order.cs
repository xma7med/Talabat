﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
    public class Order : BaseAuditableEntity<int>
    {
        
        public required string BuyerEmail { get; set; } // To double check through 
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public required Address ShippingAddress { get; set; }
        // FK ( DeliveryMethod ) 
        public int ? DeliveryMethodId { get; set; }
        public virtual DeliveryMethod ? DeliveryMethod { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal Subtotal { get; set; }

        /// Derived Attribute 
        // First way 
        //[NotMapped]
        //public decimal Total => Subtotal + DeliveryMethod!.Cost;

        // Second way 
        public decimal GetTotal() => Subtotal + DeliveryMethod!.Cost;
        public string PaymentIntentId { get; set; } = "";

    }

}
