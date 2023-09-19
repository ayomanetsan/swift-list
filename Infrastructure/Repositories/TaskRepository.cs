using Application.Common.Interfaces;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
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

        public async Task<Task> GetTaskWithDetailsAsync(Guid taskId, CancellationToken cancellationToken)
        {
            Task? task = await _context.Tasks
                .AsNoTracking()
                .Include(x => x.Labels)
                .Include(x => x.ToDoItems)
                .FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);

            if (task is null)
            {
                throw new TaskNotFoundException(taskId);
            }

            return task;
        }
    }
}
