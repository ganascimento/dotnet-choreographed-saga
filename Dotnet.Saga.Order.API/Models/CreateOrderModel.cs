namespace Dotnet.Saga.Order.API.Models;

public class CreateOrderModel
{
    public required decimal DiscountValue { get; set; }
    public required string RequesterName { get; set; }
    public required string RequesterEmail { get; set; }
    public required List<CreateOrderProductModel> Products { get; set; }
}