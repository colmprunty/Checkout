using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.API.Models;

namespace Checkout.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private List<Order> _inMemoryOrders = new List<Order>();

        public void AddOrder(Order order)
        {
            _inMemoryOrders.Add(order);
        }

        public Order GetOrder(Guid orderId)
        {
            return _inMemoryOrders.Single(x => x.OrderId == orderId);
        }
    }
}
