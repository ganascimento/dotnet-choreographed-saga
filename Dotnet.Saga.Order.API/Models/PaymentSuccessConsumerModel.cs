namespace Dotnet.Saga.Order.API.Models;

public class PaymentSuccessConsumerModel
{
    public required Guid OrderId { get; set; }
}