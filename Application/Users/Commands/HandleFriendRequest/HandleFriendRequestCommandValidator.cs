using FluentValidation;

namespace Application.Users.Commands.AcceptFriendRequest;

public class HandleFriendRequestCommandValidator : AbstractValidator<HandleFriendRequestCommand>
{
    public HandleFriendRequestCommandValidator()
    {
        RuleFor(x => x.UserEmail)
            .NotEmpty().WithMessage("User email is required.")
            .EmailAddress().WithMessage("Provided email is not valid.");
        
        RuleFor(x => x.RequesterEmail)
            .NotEmpty().WithMessage("Requester email is required.")
            .EmailAddress().WithMessage("Provided email is not valid.")
            .NotEqual(x => x.UserEmail).WithMessage("Requester email cannot match user email");
    }
}
