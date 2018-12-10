using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.API.Models;
using Checkout.API.Repositories;

namespace Checkout.API.Tests.Stubs
{
    public class OrderRepositoryStub : IOrderRepository
    {
        private List<Order> _orders = new List<Order>
        {
            new Order
            {
                OrderId = new Guid("011b9776-be8a-4243-841a-ec2d48b627f0"),
                Items = new List<OrderItem>()
            },
            new Order
            {
                OrderId = new Guid("ac46275c-c2a6-426e-8128-153f9ae5eda8"),
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ItemId = new Guid("4d2fb458-e349-4495-bd51-f026501b1eba"),
                        Product = new Product
                        {
                            Name = "A rock I found"
                        },
                        Quantity = 18
                    }
                }
            }
        };

        public Task<Order> AddItem(Guid? orderId, OrderItem item)
        {
            Order order;
            if (orderId == null)
            {
                order = new Order
                {
                    OrderId = Guid.NewGuid(),
                    Items = new List<OrderItem> { item }
                };
                _orders.Add(order);
            }
            else
            {
                order = _orders.SingleOrDefault(x => x.OrderId == orderId);
                order.Items.Add(item);
            }

            return Task.FromResult(order);
        }

        public Task AddOrder(Order order)
        {
            _orders.Add(order);
            return Task.FromResult(order);
        }

        public Task<Order> GetOrder(Guid orderId)
        {
            return Task.FromResult(_orders.SingleOrDefault(x => x.OrderId == orderId));
        }

        public Task<Order> RemoveItem(Guid orderId, Guid itemId)
        {
            var order = _orders.Single(x => x.OrderId == orderId);
            var item = order.Items.Single(x => x.ItemId == itemId);
            order.Items.Remove(item);

            return Task.FromResult(order);
        }
    }
}