using Application.Users.Commands.Register;
using Application.Users.Queries.Login;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);

        public string Login(LoginQuery credentials);

        public string Register(RegisterUserCommand credentials);
    }
}
