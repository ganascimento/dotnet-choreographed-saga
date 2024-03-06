using Dotnet.Saga.Order.API.Entities;

namespace Dotnet.Saga.Order.API.Interfaces;

public interface IOrderRepository
{
    Task<OrderEntity?> GetByIdAsync(Guid id);
    Task<OrderEntity?> CreateAsync(OrderEntity entity);
    Task UpdateAsync(OrderEntity entity);
}