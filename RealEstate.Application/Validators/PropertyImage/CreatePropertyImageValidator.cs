using FluentValidation;
using RealEstate.Application.DTOs.PropertyImage;

namespace RealEstate.Application.Validators.PropertyImage;

public class CreatePropertyImageValidator
    : AbstractValidator<CreatePropertyImageDto>
{
    public CreatePropertyImageValidator()
    {
        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PropertyId)
            .GreaterThan(0);
    }
}