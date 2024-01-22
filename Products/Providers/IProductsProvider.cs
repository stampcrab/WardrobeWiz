using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.Providers
{
    public interface IProductsProvider
    {
        Task<(IEnumerable<Product> products, bool isSuccess, string message)> GetProducts();
        Task<(Product product, bool isSuccess, string message)> GetProductById(int id);
    }
}
