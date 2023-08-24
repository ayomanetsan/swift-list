using Task = Domain.Entities.Task;

namespace Application.Common.Interfaces
{
    public interface ITaskRepository : IRepository<Task>
    {
        System.Threading.Tasks.Task ChangeCompletionAsync(Guid id, CancellationToken cancellationToken);
    }
}
