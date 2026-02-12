using MediatR;
using DebtTracker.Application.Common.Models;
using DebtTracker.Application.DTOs.User;

namespace DebtTracker.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQuery(Guid userId) : IRequest<Result<UserDto>>
{
    public Guid UserId { get; set; } = userId;
}