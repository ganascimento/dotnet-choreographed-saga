using Dotnet.Saga.Email.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Dotnet.Saga.Email.Service.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public async Task SendCancelOrderAsync(Guid orderId)
    {
        _logger.LogInformation($"Send email cancel order {orderId}...");
        await Task.Delay(1000);
        _logger.LogInformation("E-mail sended!");
    }

    public async Task SendPaymentErrorAsync(Guid orderId)
    {
        _logger.LogInformation($"Send email payment error order {orderId}...");
        await Task.Delay(1000);
        _logger.LogInformation("E-mail sended!");
    }

    public async Task SendPaymentSuccessAsync(Guid orderId)
    {
        _logger.LogInformation($"Send email payment success order {orderId}...");
        await Task.Delay(1000);
        _logger.LogInformation("E-mail sended!");
    }
}