using FluentValidation;
using RealEstate.Application.DTOs.PropertyReport;

namespace RealEstate.Application.Validators.PropertyReport;

public class CreatePropertyReportValidator
    : AbstractValidator<CreatePropertyReportDto>
{
    public CreatePropertyReportValidator()
    {
        RuleFor(x => x.Reason)
            .IsInEnum();

        RuleFor(x => x.Description)
            .MaximumLength(2000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.ReporterName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ReporterPhone)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.PropertyId)
            .GreaterThan(0);
    }
}