using System;

namespace Checkout.API.Models
{
    public class OrderItem
    {
        public OrderItem()
        {
            ItemId = Guid.NewGuid();
        }

        public Guid ItemId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}