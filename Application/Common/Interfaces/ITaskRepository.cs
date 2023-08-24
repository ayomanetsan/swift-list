using Task = Domain.Entities.Task;

namespace Application.Common.Interfaces
{
    public interface ITaskRepository : IRepository<Task>
    {
        void ChangeCompletion(Task task);
    }
}
