using FluentValidation;
using DebtTracker.Application.DTOs.Payment;

namespace DebtTracker.Application.Validators.Payment;

public class CreatePaymentDtoValidator : AbstractValidator<CreatePaymentDto>
{
    public CreatePaymentDtoValidator()
    {
        RuleFor(x => x.DebtId)
            .NotEmpty().WithMessage("La deuda es requerida");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("El monto debe ser mayor a 0");

        RuleFor(x => x.PaymentDate)
            .NotEmpty().WithMessage("La fecha de pago es requerida")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("La fecha de pago no puede ser futura");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Las notas no pueden exceder 500 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Notes));

        RuleFor(x => x.PaymentMethod)
            .MaximumLength(50).WithMessage("El método de pago no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.PaymentMethod));
    }
}