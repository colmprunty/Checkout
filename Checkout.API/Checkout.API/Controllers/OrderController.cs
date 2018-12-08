using Checkout.API.Repositories;
using Microsoft.AspNetCore.Mvc;
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

        //[Route("/api/order/add")]
        //[HttpPost]
        //public async Task OrderAdd()
        //{
        //    await _orderRepository.AddToOrder();
        //}
    }
}