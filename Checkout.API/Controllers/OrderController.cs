using Checkout.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        public async Task OrderAddItem(string item)
        {
            await _orderRepository.AddItem(null, item);
        }
    }
}