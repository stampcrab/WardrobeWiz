using Microsoft.AspNetCore.Mvc;
using Orders.Providers;
using System.Threading.Tasks;

namespace Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private IOrderProvider _orderProvider;

        public OrderController(IOrderProvider orderProvider)
        {
            _orderProvider = orderProvider;
        }
        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GetOrders(int CustomerId)
        {
            var result = await _orderProvider.GetOrdersAsync(CustomerId);
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
