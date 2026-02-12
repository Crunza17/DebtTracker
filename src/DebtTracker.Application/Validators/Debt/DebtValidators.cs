using FluentValidation;
using DebtTracker.Application.DTOs.Debt;

namespace DebtTracker.Application.Validators.Debt;

public class CreateDebtDtoValidator : AbstractValidator<CreateDebtDto>
{
    public CreateDebtDtoValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("El monto debe ser mayor a 0");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es requerida")
            .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres");

        RuleFor(x => x.DebtorId)
            .NotEmpty().WithMessage("El deudor es requerido");

        RuleFor(x => x.CreditorId)
            .NotEmpty().WithMessage("El acreedor es requerido");

        RuleFor(x => x)
            .Must(x => x.DebtorId != x.CreditorId)
            .WithMessage("El deudor y el acreedor no pueden ser la misma persona");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("La moneda es requerida")
            .MaximumLength(3).WithMessage("El código de moneda debe tener 3 caracteres");

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("La fecha de vencimiento debe ser futura")
            .When(x => x.DueDate.HasValue);
    }
}

public class UpdateDebtDtoValidator : AbstractValidator<UpdateDebtDto>
{
    public UpdateDebtDtoValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("El monto debe ser mayor a 0")
            .When(x => x.Amount.HasValue);

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("La fecha de vencimiento debe ser futura")
            .When(x => x.DueDate.HasValue);
    }
}