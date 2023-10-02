using AutoMapper;

namespace Application.Tasks.Queries.GetTaskWithDetails
{
    public class ToDoItemResponse
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public bool IsCompleted { get; set; } = false;

        private class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Domain.Entities.ToDoItem, ToDoItemResponse>();
                CreateMap<ToDoItemResponse, Domain.Entities.ToDoItem>();
            }
        }
    }
}
