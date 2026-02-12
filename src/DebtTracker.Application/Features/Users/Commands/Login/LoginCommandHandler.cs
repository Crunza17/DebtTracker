using AutoMapper;
using MediatR;
using DebtTracker.Application.Common.Interfaces;
using DebtTracker.Application.Common.Models;
using DebtTracker.Application.DTOs.User;
using DebtTracker.Domain.Interfaces;

namespace DebtTracker.Application.Features.Users.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null || !_authService.VerifyPassword(request.Password, user.PasswordHash))
        {
            return Result<AuthResponseDto>.Failure("Email o contraseña incorrectos");
        }

        var token = _authService.GenerateJwtToken(user);
        var userDto = _mapper.Map<UserDto>(user);
        
        return Result<AuthResponseDto>.Success(new AuthResponseDto
        {
            Token = token,
            User = userDto
        });
    }
}