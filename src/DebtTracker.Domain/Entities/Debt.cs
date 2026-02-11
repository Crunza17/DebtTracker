using DebtTracker.Domain.Common;
using DebtTracker.Domain.Enums;

namespace DebtTracker.Domain.Entities;

public class Debt : BaseEntity
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public DebtStatus Status { get; set; } = DebtStatus.Pending;
    public string Currency { get; set; } = "EUR";
    public DateTime? DueDate { get; set; }
    
    public Guid DebtorId { get; set; } 
    public Guid CreditorId { get; set; } 
    
    public User Debtor { get; set; } = null!;
    public User Creditor { get; set; } = null!;
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    
    // Computed property
    public decimal AmountPaid => Payments.Sum(p => p.Amount);
    public decimal AmountRemaining => Amount - AmountPaid;
}