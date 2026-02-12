using AutoMapper;
using MediatR;
using DebtTracker.Application.Common.Interfaces;
using DebtTracker.Application.Common.Models;
using DebtTracker.Application.DTOs.Payment;
using DebtTracker.Domain.Entities;
using DebtTracker.Domain.Enums;
using DebtTracker.Domain.Interfaces;

namespace DebtTracker.Application.Features.Payments.Commands.RegisterPayment;

public class RegisterPaymentCommandHandler : IRequestHandler<RegisterPaymentCommand, Result<PaymentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public RegisterPaymentCommandHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task<Result<PaymentDto>> Handle(RegisterPaymentCommand request, CancellationToken cancellationToken)
    {
        var debt = await _unitOfWork.Debts.GetByIdAsync(request.DebtId, cancellationToken);

        if (debt is null)
        {
            return Result<PaymentDto>.Failure("Deuda no encontrada");
        }

        if (debt.Status is DebtStatus.Paid)
        {
            return Result<PaymentDto>.Failure("Esta deuda ya está completamente pagada");
        }

        if (debt.Status is DebtStatus.Cancelled)
        {
            return Result<PaymentDto>.Failure("Esta deuda está cancelada");
        }

        var totalPaid = await _unitOfWork.Payments.GetTotalPaidAmountByDebtIdAsync(request.DebtId, cancellationToken);
        var remaining = debt.Amount - totalPaid;

        if (request.Amount > remaining)
        {
            return Result<PaymentDto>.Failure($"El monto excede la deuda restante de {remaining:C}");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                DebtId = request.DebtId,
                Amount = request.Amount,
                PaymentDate = request.PaymentDate,
                Notes = request.Notes,
                PaymentMethod = request.PaymentMethod,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Payments.AddAsync(payment, cancellationToken);

            var newTotalPaid = totalPaid + request.Amount;
            
            if (newTotalPaid >= debt.Amount)
            {
                debt.Status = DebtStatus.Paid;
            }
            else if (newTotalPaid > 0)
            {
                debt.Status = DebtStatus.PartiallyPaid;
            }

            debt.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Debts.UpdateAsync(debt, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            payment.Debt = debt;

            await _notificationService.NotifyPaymentReceivedAsync(payment);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            var paymentDto = _mapper.Map<PaymentDto>(payment);
            return Result<PaymentDto>.Success(paymentDto);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result<PaymentDto>.Failure($"Error al registrar el pago: {ex.Message}");
        }
    }
}