using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Users.Queries.Login
{
    public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly IJwtProvider _jwtProvider;

        public LoginQueryHandler(IUserRepository userRepository, IPasswordManager passwordManager, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordManager = passwordManager;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(request.Email);
            }

            if (_passwordManager.VerifyPassword(
                password: request.Password,
                hash: user.PasswordHash ?? "",
                salt: user.PasswordSalt ?? ""))
            {
                return _jwtProvider.Generate(user);
            }

            throw new PasswordDoesntMatchException(request.Email);
        }
    }
}
