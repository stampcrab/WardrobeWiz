using Customers.Providers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProvider customerProvider;

        public CustomerController(ICustomerProvider _customerProvider)
        {
            customerProvider = _customerProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await customerProvider.GetCustomersAsync();
            if (result.isSuccess)
            {
                return Ok(result.customers);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var result = await customerProvider.GetCustomerAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.customer);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
