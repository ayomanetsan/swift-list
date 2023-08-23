using Application.Common.Interfaces;
using Domain.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseAuditableEntity
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public Task<T> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }

        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _context.Set<T>().ToListAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>();
        }
    }
}
