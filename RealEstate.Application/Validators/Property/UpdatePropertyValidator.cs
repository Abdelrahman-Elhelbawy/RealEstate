using FluentValidation;
using RealEstate.Application.DTOs.Property;

namespace RealEstate.Application.Validators.Property;

public class UpdatePropertyValidator : AbstractValidator<UpdatePropertyDto>
{
    public UpdatePropertyValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Area)
            .GreaterThan(0);

        RuleFor(x => x.Bedrooms)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Bathrooms)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Floor)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Address)
            .MaximumLength(300);

        RuleFor(x => x.City)
            .MaximumLength(100);

        RuleFor(x => x.AgentId)
            .GreaterThan(0);
    }
}