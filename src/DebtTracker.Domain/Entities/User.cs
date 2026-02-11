using DebtTracker.Domain.Common;

namespace DebtTracker.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    
    // Navigation properties
    public ICollection<Debt> DebtsAsDebtor { get; set; } = new List<Debt>();
    public ICollection<Debt> DebtsAsCreditor { get; set; } = new List<Debt>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}