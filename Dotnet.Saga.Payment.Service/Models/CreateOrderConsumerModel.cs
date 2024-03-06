namespace Dotnet.Saga.Payment.Service.Models;

public class CreateOrderConsumerModel
{
    public required Guid Id { get; set; }
    public required decimal TotalValue { get; set; }
}