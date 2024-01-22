using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductsProvider(ProductsDbContext context, IMapper mapper, ILogger<ProductsProvider> logger)
        {
            _ctx = context;
            _mapper = mapper;
            _logger = logger;

            if(!_ctx.Products.Any())
                SeedData();
        }

        public void SeedData()
        {
            _ctx.Products.Add(
                new Db.Product { Id = 1, Name = "Winter Coat", Description = "very warm" }
            );
            _ctx.Products.Add(
                new Db.Product { Id = 2, Name = "Socks", Description = "very warm pink socks" }
            );
            _ctx.Products.Add(
                new Db.Product { Id = 3, Name = "Dress", Description = "glittery dress" }
            );
            _ctx.SaveChanges();
        }

        public async Task<(IEnumerable<Product> products, bool isSuccess, string message)> GetProducts()
        {
            try
            {
                var products = await _ctx.Products.ToListAsync();
                
                if(products!=null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Product>>(products);
                    return (result, true, null);
                }
                else
                {
                    return (null, false, "Not found");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (null, false, e.Message);
            }
        }
        public async Task<(Product product, bool isSuccess, string message)> GetProductById(int id)
        {
            try
            {
                var result = await _ctx.Products.FindAsync(id);
                if (result != null)
                {
                    var product = _mapper.Map<Product>(result);
                    return (product, true, null);
                }
                else { return (null, false, "Not Found"); }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (null, false, e.Message);
            }
        }
    }
}
