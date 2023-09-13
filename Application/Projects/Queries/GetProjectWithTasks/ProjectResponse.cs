using AutoMapper;
using Domain.Entities;

namespace Application.Projects.Queries.GetProjectWithTasks
{
    public sealed class ProjectResponse
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public IEnumerable<TaskResponse> Tasks { get; set; } = null!;

        private class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Project, ProjectResponse>();
            }
        }
    }
}
