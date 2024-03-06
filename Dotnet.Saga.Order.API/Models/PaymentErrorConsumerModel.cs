namespace Dotnet.Saga.Order.API.Models;

public class PaymentErrorConsumerModel
{
    public required Guid OrderId { get; set; }
}