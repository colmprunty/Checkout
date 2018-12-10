using System;
using System.Collections.Generic;

namespace Checkout.API.Models
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
            OrderId = Guid.NewGuid();
        }

        public Guid OrderId { get; set; }
        public IList<OrderItem> Items { get; set; }
    }
}