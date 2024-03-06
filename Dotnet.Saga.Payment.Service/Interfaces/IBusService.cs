namespace Dotnet.Saga.Payment.Service.Interfaces;

public interface IBusService
{
    Task PublishPaymentSuccessAsync(object body);
    Task PublishPaymentErrorAsync(object body);
}