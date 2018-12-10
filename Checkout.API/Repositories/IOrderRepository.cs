using Checkout.API.Models;
using System;
using System.Threading.Tasks;

namespace Checkout.API.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrder(Guid orderId);
        Task AddOrder(Order order);
        Task<Order> AddItem(Guid? orderId, OrderItem item);
        Task<Order> RemoveItem(Guid orderId, Guid itemId);
        Task<Order> ClearOrder(Guid orderId);
        Task<Order> UpdateQuantity(Guid orderId, Guid itemId, int quantity);
    }
}