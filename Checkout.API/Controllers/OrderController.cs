using Checkout.API.Models;
using Checkout.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Checkout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;            
        }

        [Route("/api/order/additem")]
        [HttpPost]
        public async Task<Order> OrderAddItem(Guid? orderId, OrderItem item)
        {
            return await _orderRepository.AddItem(orderId, item);
        }

        [Route("/api/order/removeitem")]
        [HttpDelete]
        public async Task<Order> OrderRemoveItem(Guid orderId, Guid itemId)
        {
            return await _orderRepository.RemoveItem(orderId, itemId);
        }

        [Route("/api/order/clear")]
        [HttpDelete]
        public async Task<Order> OrderClear(Guid orderId)
        {
            return await _orderRepository.ClearOrder(orderId);
        }

        [Route("/api/order/itemupdatequantity")]
        [HttpPut]
        public async Task<Order> OrderItemUpdateQuantity(Guid orderId, Guid itemId, int quantity)
        {
            return await _orderRepository.UpdateQuantity(orderId, itemId, quantity);
        }
    }
}