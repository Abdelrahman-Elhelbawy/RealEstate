using FluentValidation;
using RealEstate.Application.DTOs.Agent;

namespace RealEstate.Application.Validators.Agent;

public class UpdateAgentValidator
    : AbstractValidator<UpdateAgentDto>
{
    public UpdateAgentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.WhatsApp)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(150);
    }
}