using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Db;
using Products.Providers;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<ProductsDbContext>(options =>
        {
            options.UseInMemoryDatabase("Products");
        } );
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddScoped<IProductsProvider, ProductsProvider>();
        builder.Services.AddControllers();
        var app = builder.Build();

        app.MapControllers();
        app.Run();
    }
}