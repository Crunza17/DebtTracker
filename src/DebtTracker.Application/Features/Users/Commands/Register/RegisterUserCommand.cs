using MediatR;
using DebtTracker.Application.Common.Models;
using DebtTracker.Application.DTOs.User;

namespace DebtTracker.Application.Features.Users.Commands.Register;

public class RegisterUserCommand : IRequest<Result<AuthResponseDto>>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
}