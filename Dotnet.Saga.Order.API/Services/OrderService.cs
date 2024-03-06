using Dotnet.Saga.Order.API.Entities;
using Dotnet.Saga.Order.API.Enums;
using Dotnet.Saga.Order.API.Interfaces;
using Dotnet.Saga.Order.API.Models;

namespace Dotnet.Saga.Order.API.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBusService _busService;

    public OrderService(IOrderRepository orderRepository, IBusService busService)
    {
        _orderRepository = orderRepository;
        _busService = busService;
    }

    public async Task<Guid> CreateAsync(CreateOrderModel model)
    {
        try
        {
            var order = new OrderEntity
            {
                DiscountValue = model.DiscountValue,
                Requester = new RequesterEntity
                {
                    Email = model.RequesterEmail,
                    Name = model.RequesterName
                }
            };

            model.Products.ForEach(product =>
            {
                order.AddProduct(new OrderProductEntity
                {
                    Amount = product.Amount,
                    ProductId = product.ProductId,
                    Value = product.Value
                });
            });

            var result = await _orderRepository.CreateAsync(order);
            if (result == null)
                throw new InvalidOperationException("Erro to create order!");

            await _busService.PublishCreateOrderAsync(order);

            return result.Id;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }
    }

    public async Task CancelAsync(CancelOrderModel model)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(model.OrderId);
            if (order == null)
                throw new KeyNotFoundException("Order not found!");

            order.SetStatus(OrderStatusEnum.Canceled);

            await _orderRepository.UpdateAsync(order);
            await _busService.PublishCancelOrderAsync(order);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }
    }
}
