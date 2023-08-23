using Application.Common.Interfaces;
using MediatR;

namespace Application.Users.Queries.Login
{
    public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var token = await _userRepository.LoginAsync(request, cancellationToken);
            return token;
        }
    }
}
