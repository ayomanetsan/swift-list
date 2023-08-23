using Domain.Entities;
using Application.Common.Interfaces;
using Application.Users.Queries.Login;
using Application.Users.Commands.Register;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Exceptions;
using AutoMapper;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IPasswordManager _passwordManager;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;

        public UserRepository(ApplicationDbContext context, IPasswordManager passwordManager, IMapper mapper, IJwtProvider jwtProvider)
            : base(context)
        {
            _passwordManager = passwordManager;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> LoginAsync(LoginQuery request, CancellationToken cancellationToken)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(request.Email);
            }

            if (_passwordManager.VerifyPassword(
                password: request.Password,
                hash: user.PasswordHash,
                salt: user.PasswordSalt))
            {
                return _jwtProvider.Generate(user);
            }

            throw new PasswordDoesntMatchException(request.Email);
        }

        public async Task<string> RegisterAsync(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user is not null)
            {
                throw new UserAlreadyExists(request.Email);
            }

            byte[] passwordSalt = _passwordManager.GenerateSalt();
            byte[] passwordHash = _passwordManager.HashPassword(request.Password, passwordSalt);

            user = _mapper.Map<User>(request);
            user.PasswordHash = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);

            Create(user);

            return _jwtProvider.Generate(user);
        }
    }
}
