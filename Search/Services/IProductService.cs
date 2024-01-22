using Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Search.Services
{
    public interface IProductService
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
    }
}
