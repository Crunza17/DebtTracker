using DebtTracker.Domain.Entities;
using DebtTracker.Domain.Enums;

namespace DebtTracker.Domain.Interfaces;

public interface IDebtRepository : IRepository<Debt>
{
    Task<IEnumerable<Debt>> GetDebtsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Debt>> GetDebtsByDebtorIdAsync(Guid debtorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Debt>> GetDebtsByCreditorIdAsync(Guid creditorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Debt>> GetDebtsByStatusAsync(DebtStatus status, CancellationToken cancellationToken = default);
    Task<decimal> GetTotalDebtAmountByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}