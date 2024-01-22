using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Search.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Search.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;


        }
        public async Task<(bool IsSuccess, Customer Customer, string Message)> GetCustomerAsync(int CustomerId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CustomerService");
                var response = await client.GetAsync($"api/customers/{CustomerId}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonCustomer = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<Customer>(jsonCustomer);
                    return (true, customer, "");
                }
                else
                {
                    return (false, null, response.ReasonPhrase);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
