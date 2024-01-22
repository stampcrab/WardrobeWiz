using Search.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Search.Services
{
    public interface IOrderService
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int CustomerId);
    }
}
