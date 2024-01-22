using AutoMapper;
using Customers.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerProvider> _logger;

        public CustomerProvider(CustomerDbContext context, IMapper mapper, ILogger<CustomerProvider> logger)
        {
            _ctx = context;
            _mapper = mapper;
            _logger = logger;
            if (!_ctx.Customers.Any())
            {
                seedData();
            }
            
        }
        private void seedData()
        {
            _ctx.Customers.Add(new Db.Customer { Id = 1, Name = "Walter White", Address = "Street 2" });
            _ctx.Customers.Add(new Db.Customer { Id = 2, Name = "Sub Pop", Address = "Street 4" });
            _ctx.Customers.Add(new Db.Customer { Id = 3, Name = "Studio Ghibli", Address = "Street 6" });
            _ctx.SaveChanges();
        }

        public async Task<(IEnumerable<Customer> customers, bool isSuccess, string message)> GetCustomersAsync()
        {
            try
            {

                var result = await _ctx.Customers.ToListAsync();
                if (result.Any() && result != null)
                {
                    var customers = _mapper.Map<IEnumerable<Customer>>(result);
                    return (customers, true, null);
                }
                else
                {
                    return (null, false, "Not Found");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (null, false, e.Message);
            }
        }

        public async Task<(Customer customer, bool isSuccess, string message)> GetCustomerAsync(int id)
        {
            try
            {

                var result = await _ctx.Customers.FindAsync(id);
                if (result != null)
                {
                    var customer = _mapper.Map<Customer>(result);
                    return (customer, true, null);
                }
                else
                {
                    return (null, false, "Not Found");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (null, false, e.Message);
            }
        }
    }
}
