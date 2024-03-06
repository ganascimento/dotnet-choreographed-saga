namespace Dotnet.Saga.Email.Service.Models;

public class PaymentSuccessConsumerModel
{
    public required Guid OrderId { get; set; }
}