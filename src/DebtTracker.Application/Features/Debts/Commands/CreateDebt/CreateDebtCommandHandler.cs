using AutoMapper;
using DebtTracker.Application.Common.Interfaces;
using DebtTracker.Application.Common.Models;
using DebtTracker.Application.DTOs.Debt;
using DebtTracker.Application.DTOs.User;
using DebtTracker.Application.Features.Users.Commands.Register;
using DebtTracker.Domain.Entities;
using DebtTracker.Domain.Enums;
using DebtTracker.Domain.Interfaces;
using MediatR;

namespace DebtTracker.Application.Features.Debts.Commands.CreateDebt;

public class CreateDebtCommandHandler :  IRequestHandler<CreateDebtCommand, Result<DebtDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public CreateDebtCommandHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task<Result<DebtDto>> Handle(CreateDebtCommand request, CancellationToken cancellationToken)
    {
        var debtor = await _unitOfWork.Users.GetByIdAsync(request.DebtorId, cancellationToken);
        var creditor = await _unitOfWork.Users.GetByIdAsync(request.CreditorId, cancellationToken);

        if (debtor is null || creditor is null)
        {
            return Result<DebtDto>.Failure("Usuario no encontrado");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            
            var debt = new Debt
            {
                Id = Guid.NewGuid(),
                Amount = request.Amount,
                Description = request.Description,
                DebtorId = request.DebtorId,
                CreditorId = request.CreditorId,
                Currency = request.Currency,
                DueDate = request.DueDate,
                Status = DebtStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };
            
            await _unitOfWork.Debts.AddAsync(debt, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            debt.Debtor = debtor;
            debt.Creditor = creditor;

            await _notificationService.NotifyDebtCreatedAsync(debt);
            
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            
            var debtDto = _mapper.Map<DebtDto>(debt);
            return Result<DebtDto>.Success(debtDto);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result<DebtDto>.Failure($"Error al crear la deuda: {ex.Message}");
        }
    }
}