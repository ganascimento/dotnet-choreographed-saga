namespace Dotnet.Saga.Stock.API.Models;

public class CreateOrderConsumerModel
{
    public required Guid Id { get; set; }
    public required List<CreateOrderProductConsumerModel> Products { get; set; }
}

public class CreateOrderProductConsumerModel
{
    public required Guid ProductId { get; set; }
    public required int Amount { get; set; }
}