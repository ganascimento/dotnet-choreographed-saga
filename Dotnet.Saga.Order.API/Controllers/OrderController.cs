using Dotnet.Saga.Order.API.Interfaces;
using Dotnet.Saga.Order.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Saga.Order.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderModel model)
    {
        try
        {
            var result = await _orderService.CreateAsync(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> CancelAsync([FromBody] CancelOrderModel model)
    {
        try
        {
            await _orderService.CancelAsync(model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}