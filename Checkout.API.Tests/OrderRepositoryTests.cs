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

            // when
            var newOrder = await orderRepository.AddItem(null, "new item");

            // then
            Assert.NotNull(newOrder);
            Assert.Equal("new item", newOrder.Items.First());
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
    }
}