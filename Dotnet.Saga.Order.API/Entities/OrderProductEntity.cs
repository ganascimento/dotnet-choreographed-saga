using Dotnet.Saga.Order.API.Entities.Base;

namespace Dotnet.Saga.Order.API.Entities;

public class OrderProductEntity : BaseEntity
{
    public required Guid ProductId { get; set; }
    public required decimal Value { get; set; }
    public required int Amount { get; set; }
    public string? Note { get; set; }
}