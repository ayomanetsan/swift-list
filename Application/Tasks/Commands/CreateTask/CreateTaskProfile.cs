using AutoMapper;
using Task = Domain.Entities.Task;

namespace Application.Tasks.Commands.CreateTask
{
    public class CreateTaskProfile : Profile
    {
        public CreateTaskProfile() 
        {
            CreateMap<CreateTaskCommand, Task>()
                .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.Labels))
                .ForMember(dest => dest.ToDoItems, opt => opt.MapFrom(src => src.ToDoItems));
        }
    }
}
