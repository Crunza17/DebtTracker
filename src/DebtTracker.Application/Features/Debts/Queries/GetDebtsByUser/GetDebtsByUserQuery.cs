using DebtTracker.Application.Common.Models;
using DebtTracker.Application.DTOs.Debt;
using MediatR;

namespace DebtTracker.Application.Features.Debts.Queries.GetDebtsByUser;

public class GetDebtsByUserQuery(Guid userId) : IRequest<Result<List<DebtDto>>>
{
    public Guid UserId { get; set; } = userId;
}