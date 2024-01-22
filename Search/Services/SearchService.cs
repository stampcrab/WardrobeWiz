using System.Threading.Tasks;
using System.Dynamic;
using System.Linq;


namespace Search.Services
{
    public class SearchService : ISearchService
    {
        private IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;

        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            _orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int CustomerId)
        {
            var ordersResult = await _orderService.GetOrdersAsync(CustomerId);
            var productsResult = await _productService.GetProductsAsync();
            var customerResult = await _customerService.GetCustomerAsync(CustomerId);
            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name;
                    }
                }
                return (true, new { 
                    Customer = customerResult.IsSuccess ? customerResult.Customer : 
                    new Models.Customer { Name = "Information not available"  },
                    Orders = ordersResult.Orders });
            }
           return (false, new {Message = "Not found"});
        }
    }
}
