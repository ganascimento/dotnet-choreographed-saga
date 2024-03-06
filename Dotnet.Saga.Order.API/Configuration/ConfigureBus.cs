using Dotnet.Saga.Order.API.Consumers;

namespace Dotnet.Saga.Order.API.Configuration;

public static class ConfigureBus
{
    public static IServiceCollection ConfigBus(this IServiceCollection services)
    {
        services.AddHostedService<PaymentErrorConsumer>();
        services.AddHostedService<PaymentSuccessConsumer>();

        return services;
    }
}