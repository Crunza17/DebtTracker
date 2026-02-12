using AutoMapper;
using DebtTracker.Application.Common.Models;
using DebtTracker.Application.DTOs.Debt;
using DebtTracker.Domain.Interfaces;
using MediatR;

namespace DebtTracker.Application.Features.Debts.Queries.GetDebtsByUser;

public class GetDebtsByUserQueryHandler : IRequestHandler<GetDebtsByUserQuery, Result<List<DebtDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDebtsByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<DebtDto>>> Handle(GetDebtsByUserQuery request, CancellationToken cancellationToken)
    {
        var debts = await _unitOfWork.Debts.GetDebtsByUserIdAsync(request.UserId, cancellationToken);
        var debtsDto = _mapper.Map<List<DebtDto>>(debts);
        
        return Result<List<DebtDto>>.Success(debtsDto);
    }
}