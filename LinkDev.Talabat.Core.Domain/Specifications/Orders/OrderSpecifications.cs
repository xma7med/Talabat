﻿using LinkDev.Talabat.Core.Domain.Entities.Orders;

namespace LinkDev.Talabat.Core.Domain.Specifications.Orders
{
    public class OrderSpecifications:BaseSpecifications<Order, int >
    {
        // For GetOrderByIdAsync
        public OrderSpecifications(string buyerEmail , int orderId)
           : base(order => order.Id == orderId && order.BuyerEmail == buyerEmail)
        {
            AddIncludes();
            
        }


        public OrderSpecifications(string buyerEmail )
            :base (order => order.BuyerEmail==buyerEmail)
        {
            AddIncludes();
            // I need ordered from the latest to the older 
            AddOrderByDesc(order => order.OrderDate);
        }

        private protected override void AddIncludes()
        {
            base.AddIncludes();

            Includes.Add(order => order.Items!);
            Includes.Add(order => order.DeliveryMethod!);
        }
    }
}
