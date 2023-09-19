using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IToDoItemRepository : IRepository<ToDoItem>
    {
        System.Threading.Tasks.Task ChangeCompletionAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<ToDoItem>> GetByTaskId(Guid taskId, CancellationToken cancellationToken);
    }
}
