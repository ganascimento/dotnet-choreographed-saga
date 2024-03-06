using Dotnet.Saga.Order.API.Bus;
using Dotnet.Saga.Order.API.Infra.Repositories;
using Dotnet.Saga.Order.API.Interfaces;
using Dotnet.Saga.Order.API.Services;

namespace Dotnet.Saga.Order.API.Configuration;

public static class ConfigureDependencyInjection
{
    public static IServiceCollection ConfigDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IBusService, BusService>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}