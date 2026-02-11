using DebtTracker.Domain.Entities;

namespace DebtTracker.Domain.Interfaces;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<IEnumerable<Payment>> GetPaymentsByDebtIdAsync(Guid debtId, CancellationToken cancellationToken = default);
    Task<decimal> GetTotalPaidAmountByDebtIdAsync(Guid debtId, CancellationToken cancellationToken = default);
}