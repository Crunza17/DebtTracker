using DebtTracker.Domain.Enums;

namespace DebtTracker.Application.DTOs.Notification;

public class NotificationDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? DebtId { get; set; }
    public NotificationType Type { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReadAt { get; set; }
}

public class CreateNotificationDto
{
    public Guid UserId { get; set; }
    public Guid? DebtId { get; set; }
    public NotificationType Type { get; set; }
    public string Message { get; set; } = string.Empty;
}