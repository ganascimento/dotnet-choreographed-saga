namespace Dotnet.Saga.Email.Service.Models;

public class PaymentErrorConsumerModel
{
    public required Guid OrderId { get; set; }
}