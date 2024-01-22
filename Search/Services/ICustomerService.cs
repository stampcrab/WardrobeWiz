using Search.Models;
using System.Threading.Tasks;

namespace Search.Services
{
    public interface ICustomerService
    {
        Task<(bool IsSuccess, Customer Customer, string Message)> GetCustomerAsync(int CustomerId);
    }
}
