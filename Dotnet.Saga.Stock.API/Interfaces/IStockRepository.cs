using Dotnet.Saga.Stock.API.Entities;

namespace Dotnet.Saga.Stock.API.Interfaces;

public interface IStockRepository
{
    Task<StockEntity?> GetByProductIdAsync(Guid id);
    Task<List<StockEntity>?> GetByOrderIdAsync(Guid id);
    Task UpdateAsync(List<StockEntity> entities, bool added = true);
}