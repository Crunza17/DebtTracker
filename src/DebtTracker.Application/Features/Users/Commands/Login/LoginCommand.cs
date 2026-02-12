using MediatR;
using DebtTracker.Application.Common.Models;
using DebtTracker.Application.DTOs.User;

namespace DebtTracker.Application.Features.Users.Commands.Login;

public class LoginCommand : IRequest<Result<AuthResponseDto>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}