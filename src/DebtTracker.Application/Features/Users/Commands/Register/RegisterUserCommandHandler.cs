using AutoMapper;
using MediatR;
using DebtTracker.Application.Common.Interfaces;
using DebtTracker.Application.Common.Models;
using DebtTracker.Application.DTOs.User;
using DebtTracker.Domain.Entities;
using DebtTracker.Domain.Interfaces;

namespace DebtTracker.Application.Features.Users.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<AuthResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<Result<AuthResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Users.EmailExistsAsync(request.Email, cancellationToken))
        {
            return Result<AuthResponseDto>.Failure("El email ya está registrado");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            PasswordHash = _authService.HashPassword(request.Password),
            PhoneNumber = request.PhoneNumber,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Users.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _authService.GenerateJwtToken(user);
        var userDto = _mapper.Map<UserDto>(user);

        return Result<AuthResponseDto>.Success(new AuthResponseDto
        {
            Token = token,
            User = userDto
        });
    }
}