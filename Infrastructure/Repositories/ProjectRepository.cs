using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> GetProjectsWithoutTasksAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Projects
                .Where(x => x.CreatedBy == email)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public Task<Project> GetProjectWithTasksAsync(Guid projectId, CancellationToken cancellationToken)
        {
            return _context.Projects
                .Include(x => x.Tasks)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == projectId, cancellationToken)!;
        }
    }
}
