namespace Dotnet.Saga.Stock.API.Models;

public class StockModel
{
    public required Guid ProductId { get; set; }
    public required int Amount { get; set; }
}