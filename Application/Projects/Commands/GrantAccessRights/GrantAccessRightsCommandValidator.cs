using FluentValidation;

namespace Application.Projects.Commands.GrantAccessRights;

public class GrantAccessRightsCommandValidator : AbstractValidator<GrantAccessRightsCommand>
{
    public GrantAccessRightsCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Provided email is not valid.");
    }
}
