using Dotnet.Saga.Order.API.Models;

namespace Dotnet.Saga.Order.API.Interfaces;

public interface IOrderService
{
    Task<Guid> CreateAsync(CreateOrderModel model);
    Task CancelAsync(CancelOrderModel model);
}