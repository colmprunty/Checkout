using Checkout.API.Models;
using System;
using System.Threading.Tasks;

namespace Checkout.API.Repositories
{
    public interface IOrderRepository
    {
        Order GetOrder(Guid orderId);
        void AddOrder(Order order);
    }
}