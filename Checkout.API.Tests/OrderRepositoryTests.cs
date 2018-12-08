using Checkout.API.Models;
using Checkout.API.Repositories;
using System;
using Xunit;

namespace Checkout.API.Tests
{
    public class OrderRepositoryTests
    {
        [Fact]
        public void AddOrder_should_add_order_to_repo()
        {
            // given
            var orderId = Guid.NewGuid();
            var orderRepository = new OrderRepository();
            var newOrder = new Order { OrderId = orderId };

            // when
            orderRepository.AddOrder(newOrder);

            // then
            var orderFromRepo = orderRepository.GetOrder(orderId);

            Assert.NotNull(orderFromRepo);
        }
    }
}
