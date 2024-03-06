namespace Dotnet.Saga.Stock.API.Models;

public class ProductModel
{
    public required string Name { get; set; }
    public decimal CurrentValue { get; set; }
    public string? Description { get; set; }
}