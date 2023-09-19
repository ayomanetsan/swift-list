using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IToDoItemRepository : IRepository<ToDoItem>
    {
        Task<IEnumerable<ToDoItem>> GetByTaskId(Guid taskId, CancellationToken cancellationToken);
    }
}
