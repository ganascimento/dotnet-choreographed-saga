using Dotnet.Saga.Stock.API.Interfaces;
using Dotnet.Saga.Stock.API.Models;

namespace Dotnet.Saga.Stock.API.Services;

public class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;

    public StockService(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<StockModel?> GetByProductIdAsync(Guid id)
    {
        var stock = await _stockRepository.GetByProductIdAsync(id);
        if (stock == null)
            throw new KeyNotFoundException("Stock not found!");

        return new StockModel
        {
            Amount = stock.Amount,
            ProductId = stock.Product!.Id
        };
    }
}