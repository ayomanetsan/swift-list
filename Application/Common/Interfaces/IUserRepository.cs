using Application.Users.Commands.Register;
using Application.Users.Queries.Login;
using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<string> LoginAsync(LoginQuery request, CancellationToken cancellationToken);

        Task<string> RegisterAsync(RegisterUserCommand request, CancellationToken cancellationToken);

        Task<FriendshipStatus> SendFriendRequestAsync(string userEmail, string requesterEmail,
            CancellationToken cancellationToken);

        Task<IEnumerable<Friend>> GetFriendsAsync(string email, CancellationToken cancellationToken);

        Task<FriendshipStatus> HandleFriendRequestAsync(string userEmail, string requesterEmail,
            FriendshipStatus friendshipStatus, CancellationToken cancellationToken);
    }
}
