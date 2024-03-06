using Dotnet.Saga.Payment.Service.Enums;
using Dotnet.Saga.Payment.Service.Interfaces;
using Dotnet.Saga.Payment.Service.Models;
using Microsoft.Extensions.Logging;

namespace Dotnet.Saga.Payment.Service.Services;

public class PaymentService : IPaymentService
{
    static int TryCount = 0;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(ILogger<PaymentService> logger)
    {
        _logger = logger;
    }

    public async Task<PaymentResultModel> Send(CreateOrderConsumerModel model)
    {
        _logger.LogInformation($"[PaymentService] TryCount: {PaymentService.TryCount}");

        if (PaymentService.TryCount == 3)
        {
            PaymentService.TryCount = 0;
            return await Task.Run(() => new PaymentResultModel
            {
                Status = PaymentStatusEnum.Error,
                Ident = Guid.NewGuid()
            });
        }

        PaymentService.TryCount += 1;
        return await Task.Run(() => new PaymentResultModel
        {
            Status = PaymentStatusEnum.Success,
            Ident = Guid.NewGuid()
        });
    }
}