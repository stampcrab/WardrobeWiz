using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Orders.Db;
using Orders.Providers;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<OrderDbContext>(options =>
        {
            options.UseInMemoryDatabase("Orders");
        });
        builder.Services.AddScoped<IOrderProvider, OrderProvider>();
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(Program));

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.MapControllers();
        app.Run();
    }
}

