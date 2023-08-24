using Application.Common.Interfaces;
using Infrastructure.Data;
using Task = Domain.Entities.Task;

namespace Infrastructure.Repositories
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) 
            : base(context)
        {
        }

        public void ChangeCompletion(Task task)
        {
            task.IsCompleted = !task.IsCompleted;
        }
    }
}
