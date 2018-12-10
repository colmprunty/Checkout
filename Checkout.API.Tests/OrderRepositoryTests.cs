using Checkout.API.Models;
using Checkout.API.Repositories;
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
    }
}