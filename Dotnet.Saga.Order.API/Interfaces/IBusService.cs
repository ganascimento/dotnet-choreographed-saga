namespace Dotnet.Saga.Order.API.Interfaces;

public interface IBusService
{
    Task PublishCreateOrderAsync(object body);
    Task PublishCancelOrderAsync(object body);
}