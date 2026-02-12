using MediatR;
using DebtTracker.Application.Common.Models;
using DebtTracker.Application.DTOs.Payment;

namespace DebtTracker.Application.Features.Payments.Commands.RegisterPayment;

public class RegisterPaymentCommand : IRequest<Result<PaymentDto>>
{
    public Guid DebtId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Notes { get; set; }
    public string? PaymentMethod { get; set; }
}