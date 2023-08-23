using MediatR;

namespace Application.Users.Queries.Login
{
    public record LoginQuery : IRequest<string>
    {
        public string Email { get; init; } = String.Empty;

        public string Password { get; init; } = String.Empty;
    }
}
