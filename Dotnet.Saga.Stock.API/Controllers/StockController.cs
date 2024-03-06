using Dotnet.Saga.Stock.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Saga.Stock.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductStock([FromQuery] Guid productId)
    {
        try
        {
            var result = await _stockService.GetByProductIdAsync(productId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}