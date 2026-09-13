using FluentValidation;
using RealEstate.Application.DTOs.Message;

namespace RealEstate.Application.Validators.Message;

public class CreateMessageValidator : AbstractValidator<CreateMessageDto>
{
    public CreateMessageValidator()
    {
        RuleFor(x => x.SenderName)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.SenderName));

        RuleFor(x => x.SenderPhone)
            .MaximumLength(20)
            .When(x => !string.IsNullOrWhiteSpace(x.SenderPhone));

        RuleFor(x => x.SenderEmail)
            .EmailAddress()
            .MaximumLength(150)
            .When(x => !string.IsNullOrWhiteSpace(x.SenderEmail));

        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(x => x.PropertyId)
            .GreaterThan(0);
    }
}