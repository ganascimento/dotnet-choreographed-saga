using Dotnet.Saga.Payment.Service.Models;

namespace Dotnet.Saga.Payment.Service.Interfaces;

public interface IPaymentService
{
    Task<PaymentResultModel> Send(CreateOrderConsumerModel model);
}