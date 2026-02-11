using DebtTracker.Domain.Common;

namespace DebtTracker.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid DebtId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Notes { get; set; }
    public string? PaymentMethod { get; set; } 
    
    // Navigation properties
    public Debt Debt { get; set; } = null!;
}