using Application.Common.Interfaces;
using MediatR;

namespace Application.Users.Commands.Register
{
    public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var token = await _userRepository.RegisterAsync(request, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return token;
        }
    }
}
