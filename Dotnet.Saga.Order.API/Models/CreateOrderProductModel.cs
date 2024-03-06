namespace Dotnet.Saga.Order.API.Models;

public class CreateOrderProductModel
{
    public required Guid ProductId { get; set; }
    public required decimal Value { get; set; }
    public required int Amount { get; set; }
    public string? Note { get; set; }
}