using Checkout.API.Controllers;
using Checkout.API.Models;
using Checkout.API.Repositories;
using Checkout.API.Tests.Stubs;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.API.Tests
{
    public class OrderApiTests
    {
        private readonly OrderController _orderController;
        private readonly IOrderRepository _orderRepository;

        public OrderApiTests()
        {
            _orderRepository = new OrderRepositoryStub();
            _orderController = new OrderController(_orderRepository);
        }

        [Fact]
        public async Task OrderAddItem_returns_order_with_item_if_order_already_exists()
        {
            // given
            var orderId = new Guid("011b9776-be8a-4243-841a-ec2d48b627f0");
            var product = new Product { Name = "Triceratops" };
            var newItem = new OrderItem { Product = product, Quantity = 3 };            

            // when
            await _orderController.OrderAddItem(orderId, newItem);

            // then
            var order = await _orderRepository.GetOrder(orderId);

            Assert.NotNull(order);
            Assert.Equal(orderId, order.OrderId);
            Assert.Equal(newItem, order.Items.Single());
            Assert.Equal("Triceratops", order.Items.Single().Product.Name);
            Assert.Equal(3, order.Items.Single().Quantity);
        }

        [Fact]
        public async Task OrderAddItem_creates_new_order_and_adds_item_if_order_doesnt_exist()
        {
            // given            
            var product = new Product { Name = "Stegosaurus" };
            var newItem = new OrderItem { Product = product, Quantity = 2 };

            // when
            var newlyAddedOrder = await _orderController.OrderAddItem(null, newItem);

            // then
            var newOrderFromRepo = await _orderRepository.GetOrder(newlyAddedOrder.OrderId);

            Assert.NotNull(newOrderFromRepo);
            Assert.Equal(newItem, newOrderFromRepo.Items.Single());
            Assert.Equal("Stegosaurus", newOrderFromRepo.Items.Single().Product.Name);
            Assert.Equal(2, newOrderFromRepo.Items.Single().Quantity);
        }

        [Fact]
        public async Task OrderRemoveItem_removes_item_from_order()
        {
            // given
            var orderId = new Guid("ac46275c-c2a6-426e-8128-153f9ae5eda8");
            var itemId = new Guid("4d2fb458-e349-4495-bd51-f026501b1eba");

            // when
            var order = await _orderController.OrderRemoveItem(orderId, itemId);

            // then
            Assert.Null(order.Items.SingleOrDefault(x => x.ItemId == itemId));
        }

        [Fact]
        public async Task OrderClear_should_remove_all_items_from_order()
        {
            // given
            var orderId = new Guid("ac46275c-c2a6-426e-8128-153f9ae5eda8");

            // when
            var order = await _orderController.OrderClear(orderId);

            // then
            Assert.Empty(order.Items);
        }

        [Fact]
        public async Task OrderItemUpdateQuantity_should_update_item_quantity()
        {
            // given
            var orderId = new Guid("ac46275c-c2a6-426e-8128-153f9ae5eda8");
            var itemId = new Guid("4d2fb458-e349-4495-bd51-f026501b1eba");

            // when
            var order = await _orderController.OrderItemUpdateQuantity(orderId, itemId, 400);

            // then
            Assert.Equal(400, order.Items.Single(x => x.ItemId == itemId).Quantity);
        }
    }
}