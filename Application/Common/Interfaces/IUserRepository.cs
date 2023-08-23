using Application.Users.Commands.Register;
using Application.Users.Queries.Login;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<string> LoginAsync(LoginQuery request, CancellationToken cancellationToken);

        public Task<string> RegisterAsync(RegisterUserCommand request, CancellationToken cancellationToken);
    }
}
