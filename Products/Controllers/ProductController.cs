using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Products.Providers;
using System.Threading.Tasks;

namespace Products.Controllers {
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase 
    {
        private readonly IProductsProvider productsProvider;
        
        public ProductController(IProductsProvider _productsProvider)
        {

            productsProvider = _productsProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await productsProvider.GetProducts();
            if (result.isSuccess)
            {
                return Ok(result.products);
            }
            else { return NotFound(); }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await productsProvider.GetProductById(id);
            if (result.isSuccess)
            {
                return Ok(result.product);
            }
            else { return NotFound(); }

        }
    }
}
