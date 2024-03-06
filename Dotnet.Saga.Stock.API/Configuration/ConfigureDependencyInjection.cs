using Dotnet.Saga.Stock.API.Infra.Repositories;
using Dotnet.Saga.Stock.API.Interfaces;
using Dotnet.Saga.Stock.API.Services;

namespace Dotnet.Saga.Stock.API.Configuration;

public static class ConfigureDependencyInjection
{
    public static IServiceCollection ConfigDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IStockService, StockService>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStockRepository, StockRepository>();

        return services;
    }
}