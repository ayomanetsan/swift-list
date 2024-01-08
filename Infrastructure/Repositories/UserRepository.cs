using Domain.Entities;
using Application.Common.Interfaces;
using Application.Users.Queries.Login;
using Application.Users.Commands.Register;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Exceptions;
using AutoMapper;
using Domain.Enums;

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

        public async Task<FriendshipStatus> SendFriendRequestAsync(string userEmail, string requesterEmail,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userEmail, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(userEmail);
            }
            
            var requester = await _context.Users.FirstOrDefaultAsync(x => x.Email == requesterEmail, cancellationToken);

            if (requester is null)
            {
                throw new UserNotFoundException(requesterEmail);
            }
            
            var friend = await _context.Friends
                .FirstOrDefaultAsync(x =>
                        (x.UserId == user.Id && x.RequesterId == requester.Id) ||
                        (x.UserId == requester.Id && x.RequesterId == user.Id),
                    cancellationToken);

            switch (friend)
            {
                case null:
                    friend = new Friend()
                    {
                        UserId = user.Id,
                        RequesterId = requester.Id,
                        FriendshipStatus = FriendshipStatus.Pending
                    };

                    await _context.AddAsync(friend, cancellationToken);
                    break;
                case { FriendshipStatus: FriendshipStatus.Rejected }:
                    friend.FriendshipStatus = FriendshipStatus.Pending;
                    break;
            }

            return friend.FriendshipStatus;
        }

        public async Task<IEnumerable<Friend>> GetFriendsAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(email);
            }
            
            var friends = await _context.Friends
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Requester)
                .Where(x => (x.UserId == user.Id || x.RequesterId == user.Id) && x.FriendshipStatus != FriendshipStatus.Rejected)
                .ToListAsync(cancellationToken);

            return friends;
        }

        public async Task<FriendshipStatus> HandleFriendRequestAsync(string userEmail, string requesterEmail, FriendshipStatus friendshipStatus,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userEmail, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(userEmail);
            }
            
            var requester = await _context.Users.FirstOrDefaultAsync(x => x.Email == requesterEmail, cancellationToken);

            if (requester is null)
            {
                throw new UserNotFoundException(requesterEmail);
            }
            
            var friend = await _context.Friends
                .FirstOrDefaultAsync(x =>
                    x.UserId == user.Id &&
                    x.RequesterId == requester.Id &&
                    x.FriendshipStatus != FriendshipStatus.Rejected, 
                    cancellationToken: cancellationToken);

            if (friend is null)
            {
                throw new FriendshipNotFoundException(userEmail, requesterEmail);
            }

            friend.FriendshipStatus = friendshipStatus;
            return friend.FriendshipStatus;
        }
    }
}
