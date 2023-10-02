using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ToDoItemRepository : Repository<ToDoItem>, IToDoItemRepository
    {
        public ToDoItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async System.Threading.Tasks.Task ChangeCompletionAsync(Guid id, CancellationToken cancellationToken)
        {
            ToDoItem? task = await _context.ToDoItems.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (task is null)
            {
                throw new ToDoItemNotFoundException(id);
            }

            task.IsCompleted = !task.IsCompleted;
        }

        public async Task<IEnumerable<ToDoItem>> GetByTaskId(Guid taskId, CancellationToken cancellationToken)
        {
            return await _context.ToDoItems
                .Where(x => x.TaskId == taskId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
