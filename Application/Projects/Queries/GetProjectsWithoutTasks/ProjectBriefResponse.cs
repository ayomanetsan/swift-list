using AutoMapper;
using Domain.Entities;

namespace Application.Projects.Queries.GetProjectsWithoutTasks
{
    public sealed class ProjectBriefResponse
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        private class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Project, ProjectBriefResponse>();
            }
        }
    }
}
