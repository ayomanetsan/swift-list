using Domain.Common;

namespace Application.Common.Interfaces
{
    public interface IRepository<T> where T : BaseAuditableEntity
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<T> GetAsync(Guid id, CancellationToken cancellationToken);

        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

        IQueryable<T> GetQueryable();
    }
}
