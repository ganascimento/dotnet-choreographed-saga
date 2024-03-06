namespace Dotnet.Saga.Email.Service.Interfaces;

public interface IEmailService
{
    Task SendCancelOrderAsync(Guid orderId);
    Task SendPaymentErrorAsync(Guid orderId);
    Task SendPaymentSuccessAsync(Guid orderId);
}