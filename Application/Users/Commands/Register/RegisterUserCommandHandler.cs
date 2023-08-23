using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Users.Commands.Register
{
    public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;

        public RegisterUserCommandHandler(
            IUserRepository userRepository, 
            IPasswordManager passwordManager, 
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _passwordManager = passwordManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user is not null)
            {
                throw new UserAlreadyExists(request.Email);
            }

            byte[] passwordSalt = _passwordManager.GenerateSalt();
            byte[] passwordHash = _passwordManager.HashPassword(request.Password, passwordSalt);

            user = _mapper.Map<User>(request);
            user.PasswordHash = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);

            _userRepository.Create(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _jwtProvider.Generate(user);
        }
    }
}
