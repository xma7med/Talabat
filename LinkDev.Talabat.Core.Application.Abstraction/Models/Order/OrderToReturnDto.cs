using LinkDev.Talabat.Core.Application.Abstraction.Models.Common;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Order
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }

        public required string BuyerEmail { get; set; } 
        public DateTime OrderDate { get; set; }
        public required string  Status { get; set; } 
        public  AddressDto ShippingAddress { get; set; }
        public int? DeliveryMethodId { get; set; }
        // i need to return just name 
        public  string ? DeliveryMethod { get; set; }
        public  ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public decimal Subtotal { get; set; }
        // Will read from [  GetTotal() ]
        public decimal Total { get; set; }
        //public decimal GetTotal() => Subtotal + DeliveryMethod!.Cost;
        //public string PaymentIntentId { get; set; } = ""; // i dont need it untill now 

    }
}
