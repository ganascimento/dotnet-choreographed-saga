using Dotnet.Saga.Stock.API.Consumers;

namespace Dotnet.Saga.Stock.API.Configuration;

public static class ConfigureBus
{
    public static IServiceCollection ConfigBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<CancelOrderConsumer>();
        services.AddHostedService<CreateOrderConsumer>();
        services.AddHostedService<PaymentErrorConsumer>();

        return services;
    }
}