using DebtTracker.Domain.Enums;

namespace DebtTracker.Application.DTOs.Debt;

public class DebtDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public DebtStatus Status { get; set; }
    public string Currency { get; set; } = "EUR";
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Debtor and Creditor info
    public Guid DebtorId { get; set; }
    public string DebtorName { get; set; } = string.Empty;
    public Guid CreditorId { get; set; }
    public string CreditorName { get; set; } = string.Empty;
    
    // Calculated properties
    public decimal AmountPaid { get; set; }
    public decimal AmountRemaining { get; set; }
}

public class CreateDebtDto
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid DebtorId { get; set; }
    public Guid CreditorId { get; set; }
    public string Currency { get; set; } = "EUR";
    public DateTime? DueDate { get; set; }
}

public class UpdateDebtDto
{
    public decimal? Amount { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public DebtStatus? Status { get; set; }
}

public class DebtSummaryDto
{
    public decimal TotalOwed { get; set; } 
    public decimal TotalOwedToMe { get; set; } 
    public decimal NetBalance { get; set; }     
    public int ActiveDebtsCount { get; set; }
}