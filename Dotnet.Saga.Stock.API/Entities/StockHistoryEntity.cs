using Dotnet.Saga.Stock.API.Entities.Base;
using Dotnet.Saga.Stock.API.Enums;

namespace Dotnet.Saga.Stock.API.Entities;

public class StockHistoryEntity : BaseEntity
{
    public required int Amount { get; set; }
    public required Guid OrderId { get; set; }
    public required StockStatusEnum Status { get; set; }
    public virtual StockEntity? Stock { get; set; }
}