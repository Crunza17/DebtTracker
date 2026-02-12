using DebtTracker.Domain.Entities;
using DebtTracker.Domain.Enums;

namespace DebtTracker.Application.Common.Interfaces;

public interface INotificationService
{
    Task SendNotificationAsync(Guid userId, string message, NotificationType type, Guid? debtId = null);
    Task NotifyDebtCreatedAsync(Debt debt);
    Task NotifyPaymentReceivedAsync(Payment payment);
    Task NotifyDebtStatusChangedAsync(Debt debt);
}