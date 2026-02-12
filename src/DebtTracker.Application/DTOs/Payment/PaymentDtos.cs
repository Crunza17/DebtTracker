namespace DebtTracker.Application.DTOs.Payment;

public class PaymentDto
{
    public Guid Id { get; set; }
    public Guid DebtId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Notes { get; set; }
    public string? PaymentMethod { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreatePaymentDto
{
    public Guid DebtId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Notes { get; set; }
    public string? PaymentMethod { get; set; }
}