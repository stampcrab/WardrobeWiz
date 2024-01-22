using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customers.Providers
{
    public interface ICustomerProvider
    {
        public Task<(IEnumerable<Customer> customers, bool isSuccess, string message)> GetCustomersAsync();
        public Task<(Customer customer, bool isSuccess, string message)> GetCustomerAsync(int id);
    }
}
