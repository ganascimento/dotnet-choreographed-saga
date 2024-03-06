using Dotnet.Saga.Stock.API.Models;

namespace Dotnet.Saga.Stock.API.Interfaces;

public interface IStockService
{
    Task<StockModel?> GetByProductIdAsync(Guid id);
}