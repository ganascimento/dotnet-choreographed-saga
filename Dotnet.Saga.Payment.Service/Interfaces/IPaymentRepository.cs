using Dotnet.Saga.Payment.Service.Entities;

namespace Dotnet.Saga.Payment.Service.Interfaces;

public interface IPaymentRepository
{
    Task<PaymentEntity?> GetByIdAsync(Guid id);
    Task<PaymentEntity> CreateAsync(PaymentEntity entity);
    Task UpdateAsync(PaymentEntity entity);
}