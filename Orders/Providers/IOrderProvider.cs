using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Providers
{
    public interface IOrderProvider
    {
        Task<(IEnumerable<Order> Orders, bool IsSuccess, string message)> GetOrdersAsync(int CustomerId);
    }
}
