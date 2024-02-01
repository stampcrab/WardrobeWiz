using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Products.Db;
using Products.Profiles;
using Products.Providers;

namespace WardrobeWizUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async void TestGetProductById()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase("TestGetProductById").Options;
            var context = new ProductsDbContext(options);
            var profile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(config);
            AddSeedProducts(context);
            var productProvider = new ProductsProvider(context,mapper, null);
            var res = await productProvider.GetProductById(1);
            Assert.True(res.isSuccess);
            Assert.True(res.product.Id == 1);
            Assert.Null(res.message);


        }

        private void AddSeedProducts(ProductsDbContext context)
        {
            context.Products.Add(new Products.Db.Product { Id = 1, Name = Guid.NewGuid().ToString() });
            context.Products.Add(new Products.Db.Product { Id = 2, Name = Guid.NewGuid().ToString() });
            context.Products.Add(new Products.Db.Product { Id = 3, Name = Guid.NewGuid().ToString() });
            context.SaveChanges();
        }

        [Fact]
        public async void TestGetProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase("TestGetProducts").Options;
            var context = new ProductsDbContext(options);
            var profile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(config);
            AddSeedProducts(context);
            var productProvider = new ProductsProvider(context, mapper, null);
            var res = await productProvider.GetProducts();
            Assert.True(res.isSuccess);
            Assert.True(res.products.Any());
            Assert.Null(res.message);


        }
    }
}