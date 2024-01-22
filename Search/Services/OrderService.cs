using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            
        }
        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int CustomerId)
        {
            try { 
            var client = _httpClientFactory.CreateClient("OrderService");
            var response = await client.GetAsync($"api/orders/{CustomerId}");
            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                IEnumerable<Order> result = JsonConvert.DeserializeObject<IEnumerable<Order>>(jsonContent);
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
