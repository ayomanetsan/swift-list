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

        public async System.Threading.Tasks.Task ChangeArchivationAsync(Guid id, CancellationToken cancellationToken)
        {
            Task? task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (task is null)
            {
                throw new TaskNotFoundException(id);
            }

            task.IsArchived = !task.IsArchived;
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

        public async Task<Guid> UpdateTaskWithDetailsAsync(Task task, CancellationToken cancellationToken)
        {
            var dbTask = await _context.Tasks
                .Include(t => t.Labels) // Include related Labels
                .Include(t => t.ToDoItems) // Include related ToDoItems
                .FirstOrDefaultAsync(t => t.Id == task.Id);

            if (dbTask == null)
            {
                throw new TaskNotFoundException(task.Id);
            }

            dbTask.Title = task.Title;
            dbTask.Description = task.Description;
            dbTask.CreatedAt = task.CreatedAt;
            dbTask.DueDate = task.DueDate;
            dbTask.CreatedBy = task.CreatedBy;


            var range = task.Labels.Count - dbTask.Labels.Count;
            var labelsToAdd = task.Labels.ToList().GetRange(dbTask.Labels.Count, range);
            foreach (var newLabel in labelsToAdd)
            {
                dbTask.Labels.Add(newLabel);
            }

            range = task.ToDoItems.Count - dbTask.ToDoItems.Count;
            var toDoItemsToAdd = task.ToDoItems.ToList().GetRange(dbTask.ToDoItems.Count, range);
            foreach (var newToDoItem in toDoItemsToAdd)
            {
                dbTask.ToDoItems.Add(newToDoItem);
            }

            var dbToDoItems = dbTask.ToDoItems.ToList();
            var reqToDoItems = task.ToDoItems.ToList();

            for (int i = 0; i < dbTask.ToDoItems.Count; i++)
            {
                if (dbToDoItems[i].IsCompleted != reqToDoItems[i].IsCompleted)
                {
                    dbToDoItems[i].IsCompleted = reqToDoItems[i].IsCompleted;
                }
            }

            return dbTask.Id;
        }
    }
}
