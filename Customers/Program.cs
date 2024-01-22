using Customers.Db;
using Customers.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<CustomerDbContext>(options =>
        {
            options.UseInMemoryDatabase("Customers");
        });
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddScoped<ICustomerProvider, CustomerProvider>();
        builder.Services.AddControllers();
        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}