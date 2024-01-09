using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Task = Domain.Entities.Task;

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

        public async Task<Project> GetProjectWithTasksAsync(Guid projectId, bool taskPredicate, CancellationToken cancellationToken)
        {
            var project = await _context.Projects
                .AsNoTracking()
                .Include(x => x.Tasks.Where(t => t.IsArchived == taskPredicate))
                .FirstOrDefaultAsync(x => x.Id == projectId, cancellationToken);

            if (project is null)
            {
                throw new ProjectNotFoundException(projectId);
            }

            return project;
        }

        public async Task<AccessRights> GetAccessRightsAsync(Guid projectId, string email, CancellationToken cancellationToken)
        {
            var projectUser = await _context.ProjectUsers
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ProjectId == projectId && x.User.Email == email, cancellationToken);

            if (projectUser is null)
            {
                throw new ProjectNotFoundException(projectId);
            }
            
            return projectUser.AccessRights;
        }

        public async Task<AccessRights> GrantAccessRightsAsync(Guid projectId, string email, AccessRights accessRights,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(email);
            }
            
            var projectUser = await _context.ProjectUsers
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ProjectId == projectId && x.User.Email == email, cancellationToken);

            if (projectUser is null)
            {
                var newProjectUser = new ProjectUsers()
                {
                    UserId = user.Id,
                    ProjectId = projectId,
                    AccessRights = accessRights
                };

                await _context.ProjectUsers.AddAsync(newProjectUser, cancellationToken);
                return newProjectUser.AccessRights;
            }

            projectUser.AccessRights = accessRights;
            return projectUser.AccessRights;
        }

        public async Task<IEnumerable<ProjectUsers>> GetProjectAccessRightsAsync(Guid projectId, CancellationToken cancellationToken)
        {
            var projectUsers = await _context.ProjectUsers
                .AsNoTracking()
                .Include(x => x.User)
                .Where(x => x.ProjectId == projectId)
                .ToListAsync(cancellationToken);

            if (projectUsers is null)
            {
                throw new ProjectNotFoundException(projectId);
            }

            return projectUsers;
        }
    }
}
