namespace Dotnet.Saga.Stock.API.Models;

public class PaymentErrorConsumerModel
{
    public required Guid OrderId { get; set; }
}