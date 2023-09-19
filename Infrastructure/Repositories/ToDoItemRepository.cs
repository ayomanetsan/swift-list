using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ToDoItemRepository : Repository<ToDoItem>, IToDoItemRepository
    {
        public ToDoItemRepository(ApplicationDbContext context) : base(context)
        {
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
