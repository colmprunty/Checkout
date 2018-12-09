﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.API.Models;

namespace Checkout.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private List<Order> _inMemoryOrders = new List<Order>();        

        public async Task<Order> AddItem(Guid? orderId, string item)
        {
            var order = _inMemoryOrders.SingleOrDefault(x => x.OrderId == orderId);
            if (order == null)
                order = new Order();

            await Task.Run(() => order.Items.Add(item));

            return order;
        }

        public async Task AddOrder(Order order)
        {
            await Task.Run(() => _inMemoryOrders.Add(order));
        }

        public async Task<Order> GetOrder(Guid? orderId)
        {
            return await Task.Run(() => _inMemoryOrders.Single(x => x.OrderId == orderId));
        }
    }
}
