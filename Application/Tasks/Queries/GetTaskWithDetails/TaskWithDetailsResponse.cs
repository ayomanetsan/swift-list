using Application.Tasks.Queries.GetTaskWithDetails;
using AutoMapper;

namespace Application.Tasks.Queries.GetTaskWithToDoItems
{
    public class TaskWithDetailsResponse
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset DueDate { get; set; }

        public required string CreatedBy { get; set; }

        public bool IsCompleted { get; set; }

        public IEnumerable<LabelResponse> Labels { get; set; } = null!;

        public IEnumerable<ToDoItemResponse> ToDoItems { get; set; } = null!;

        private class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Domain.Entities.Task, TaskWithDetailsResponse>();
            }
        }
    }
}
