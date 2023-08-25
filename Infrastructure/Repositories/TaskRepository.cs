using Application.Common.Interfaces;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Entities.Task;

namespace Infrastructure.Repositories
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) 
            : base(context)
        {
        }

        public async System.Threading.Tasks.Task ChangeCompletionAsync(Guid id, CancellationToken cancellationToken)
        {
            Task? task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (task is null) 
            {
                throw new TaskNotFoundException(id);
            }

            task.IsCompleted = !task.IsCompleted;
        }

        public async Task<List<Task>> GetTasksByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var tasks = await _context.Tasks.Where(t => t.CreatedBy == email).ToListAsync();
            return tasks;
        }
    }
}
