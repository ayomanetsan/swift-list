using FluentValidation;

namespace Application.Users.Queries.GetFriends;

public class GetFriendsQueryValidator : AbstractValidator<GetFriendsQuery>
{
    public GetFriendsQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Provided email is not valid.");
    }
}