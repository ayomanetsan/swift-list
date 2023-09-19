using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetProjectsWithoutTasksAsync(string email, CancellationToken cancellationToken);

        Task<Project> GetProjectWithTasksAsync(Guid projectId, CancellationToken cancellationToken);
    }
}
