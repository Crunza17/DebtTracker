using MediatR;
using DebtTracker.Application.Common.Models;
using DebtTracker.Application.DTOs.Debt;

namespace DebtTracker.Application.Features.Debts.Commands.CreateDebt;

public class CreateDebtCommand : IRequest<Result<DebtDto>>
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid DebtorId { get; set; }
    public Guid CreditorId { get; set; }
    public string Currency { get; set; } = "USD";
    public DateTime? DueDate { get; set; }
}