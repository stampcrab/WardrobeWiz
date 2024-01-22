using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Search.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Search.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            
        }
        public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProductService");
                var response = await client.GetAsync($"api/products");
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    IEnumerable<Product> result = JsonConvert.DeserializeObject<IEnumerable<Product>>(jsonContent);
                    return (true, result, "");
                }
                else
                {
                    return (false, null, response.ReasonPhrase);
                }
            }
            catch (Exception e) {
                _logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
