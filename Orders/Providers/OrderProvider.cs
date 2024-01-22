using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orders.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrderDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public OrderProvider(OrderDbContext context, IMapper mapper, ILogger<OrderProvider> logger)
        {
            _ctx = context;
            _mapper = mapper;
            _logger = logger;
            seedData();
        }

        private void seedData()
        {
            _ctx.Orders.Add(new Db.Order
            {
                Id = 1,
                CustomerId = 2,
                OrderDate = new DateOnly(),
                Total = 25,
                Items = new List<Db.OrderItem> {
                    new Db.OrderItem{ Id = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 1 },
                    new Db.OrderItem{ Id = 2,  OrderId = 1, ProductId = 2, Quantity = 1, UnitPrice = 24 } }
            });

            if (!_ctx.Orders.Any())
                _ctx.SaveChanges();
        }
        public async Task<(IEnumerable<Order> Orders, bool IsSuccess, string message)> GetOrdersAsync(int CustomerId)
        {
            try
            {
                var items = await _ctx.Orders
                    .Where(order =>  order.CustomerId == CustomerId)
                    .ToListAsync();
                if (items != null && items.Any())
                {
                    var orders = _mapper.Map<IEnumerable<Order>>(items);
                    return (orders, true, null);

                }
                else
                    return (null, false, "Not Found");
                 
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (null, false, e.Message);
            }
        }
    }
}
