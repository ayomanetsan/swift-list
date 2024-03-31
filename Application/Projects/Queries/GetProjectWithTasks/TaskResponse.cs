using AutoMapper;

namespace Application.Projects.Queries.GetProjectWithTasks
{
    public sealed class TaskResponse
    {
        public Guid Id { get; set; }
        
        public required string Title { get; set; }

        public required string Description { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsArchived { get; set; }
        
        public DateTimeOffset DueDate { get; set; }

        private class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Domain.Entities.Task, TaskResponse>();
            }
        }
    }
}
