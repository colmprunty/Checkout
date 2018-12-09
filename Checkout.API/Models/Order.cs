using System;
using System.Collections.Generic;

namespace Checkout.API.Models
{
    public class Order
    {
        public Order()
        {
            Items = new List<string>();
        }

        public Guid OrderId { get; set; }
        public IList<string> Items { get; set; }
    }
}