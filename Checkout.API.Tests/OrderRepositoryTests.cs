using Checkout.API.Repositories;
using Checkout.Resources.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.API.Tests
{
    public class OrderRepositoryTests
    {
        [Fact]
        public async Task AddItem_without_order_should_create_order()
        {
            // given
            var orderRepository = new OrderRepository();
            var product = new Product { Name = "A large cat" };
            var item = new OrderItem { Product = product, Quantity = 4 };

            // when
            var newOrder = await orderRepository.AddItem(null, item);

            // then
            Assert.NotNull(newOrder);
            Assert.Equal("A large cat", newOrder.Items.Single(x => x.ItemId == item.ItemId).Product.Name);
            Assert.Equal(4, newOrder.Items.First().Quantity);
        }

        [Fact]
        public async Task AddOrder_should_add_order_to_repo()
        {
            // given
            var orderId = Guid.NewGuid();
            var orderRepository = new OrderRepository();
            var newOrder = new Order { OrderId = orderId };

            // when
            await orderRepository.AddOrder(newOrder);

            // then
            var orderFromRepo = orderRepository.GetOrder(orderId);

            Assert.NotNull(orderFromRepo);
        }

        [Fact]
        public async Task RemoveItem_should_remove_item()
        {
            // given
            var orderRepository = new OrderRepository();
            var product = new Product { Name = "A thing that looks a bit like a panda" };
            var item = new OrderItem { Product = product, Quantity = 1 };
            var order = await orderRepository.AddItem(null, item);

            // when
            var orderAfterRemoving = await orderRepository.RemoveItem(order.OrderId, item.ItemId);

            // then
            Assert.Null(orderAfterRemoving.Items.SingleOrDefault(x => x.ItemId == item.ItemId));
        }

        [Fact]
        public async Task ClearOrder_should_remove_all_items()
        {
            // given
            var orderRepository = new OrderRepository();
            var product = new Product { Name = "Shoe" };
            var item = new OrderItem { Product = product, Quantity = 2 };
            var product2 = new Product { Name = "Hat" };
            var item2 = new OrderItem { Product = product2, Quantity = 1 };
            var order = await orderRepository.AddItem(null, item);
            await orderRepository.AddItem(order.OrderId, item2);

            // when
            var cleanOrder = await orderRepository.ClearOrder(order.OrderId);

            // then
            Assert.Equal(0, cleanOrder.Items.Count);
        }

        [Fact]
        public async Task UpdateQuantity_should_update_quantity()
        {
            // given
            var orderRepository = new OrderRepository();
            var product = new Product { Name = "Saxophone " };
            var item = new OrderItem { Product = product, Quantity = 1 };
            var order = await orderRepository.AddItem(null, item);

            // when
            var updatedOrder = await orderRepository.UpdateQuantity(order.OrderId, item.ItemId, 3);

            // then
            Assert.Equal(3, updatedOrder.Items.Single(x => x.ItemId == item.ItemId).Quantity);
        }
    }
}