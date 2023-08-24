using MediatR;

namespace Application.Users.Commands.Register
{
    public record RegisterUserCommand : IRequest<string>
    {
        public string Fullname { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;
    }
}
