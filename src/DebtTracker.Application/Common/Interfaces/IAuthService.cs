using DebtTracker.Domain.Entities;

namespace DebtTracker.Application.Common.Interfaces;

public interface IAuthService
{
    string GenerateJwtToken(User user);
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}