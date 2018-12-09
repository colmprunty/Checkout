using Checkout.API.Models;
using System;
using System.Threading.Tasks;

namespace Checkout.API.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrder(Guid? orderId);
        Task AddOrder(Order order);
        Task<Order> AddItem(Guid? orderId, string item);
    }
}