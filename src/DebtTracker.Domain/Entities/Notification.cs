using DebtTracker.Domain.Common;
using DebtTracker.Domain.Enums;

namespace DebtTracker.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid? DebtId { get; set; }
    public NotificationType Type { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    
    // Navigation properties
    public User User { get; set; } = null!;
    public Debt? Debt { get; set; }
}