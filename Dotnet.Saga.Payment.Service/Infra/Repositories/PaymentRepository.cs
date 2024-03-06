using Dotnet.Saga.Payment.Service.Entities;
using Dotnet.Saga.Payment.Service.Infra.Context;
using Dotnet.Saga.Payment.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Saga.Payment.Service.Infra.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly DataContext _context;
    private readonly DbSet<PaymentEntity> _dbSet;

    public PaymentRepository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<PaymentEntity>();
    }

    public async Task<PaymentEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.Where(payment => payment.Id == id).FirstOrDefaultAsync();
    }

    public async Task<PaymentEntity> CreateAsync(PaymentEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task UpdateAsync(PaymentEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}