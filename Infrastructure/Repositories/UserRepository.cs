using Domain.Entities;
using Application.Common.Interfaces;
using Application.Users.Queries.Login;
using Application.Users.Commands.Register;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Exceptions;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) 
            : base(context) { }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
            return user;
        }

        public string Login(LoginQuery credentials)
        {
            throw new NotImplementedException();
        }

        public string Register(RegisterUserCommand credentials)
        {
            throw new NotImplementedException();
        }
    }
}
