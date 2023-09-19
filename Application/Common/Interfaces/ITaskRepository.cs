using Task = Domain.Entities.Task;

namespace Application.Common.Interfaces
{
    public interface ITaskRepository : IRepository<Task>
    {
        System.Threading.Tasks.Task ChangeCompletionAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Task>> GetTasksByEmailAsync(string email, CancellationToken cancellationToken);

        Task<Task> GetTaskWithDetailsAsync(Guid taskId, CancellationToken cancellationToken);
    }
}
