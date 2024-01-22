using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Search.Services;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddScoped<ISearchService, SearchService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();

        builder.Services.AddHttpClient("OrderService", config =>
        {
            config.BaseAddress = new Uri(builder.Configuration["Services:Orders"]);
        });
        builder.Services.AddHttpClient("ProductService", config =>
        {
            config.BaseAddress = new Uri(builder.Configuration["Services:Products"]);
        });
        builder.Services.AddHttpClient("CustomerService", config =>
        {
            config.BaseAddress = new Uri(builder.Configuration["Services:Customers"]);
        }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMinutes(1)));
        builder.Services.AddControllers();
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.MapControllers();
        app.Run();
    }
}