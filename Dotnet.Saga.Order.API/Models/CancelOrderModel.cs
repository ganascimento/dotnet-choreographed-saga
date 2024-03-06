namespace Dotnet.Saga.Order.API.Models;

public class CancelOrderModel
{
    public required Guid OrderId { get; set; }
}