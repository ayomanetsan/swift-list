using AutoMapper;

namespace Application.Tasks.Commands.UpdateTask
{
    public class UpdateTaskProfile : Profile
    {
        public UpdateTaskProfile()
        {
            CreateMap<UpdateTaskCommand, Domain.Entities.Task>()
                .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.Labels))
                .ForMember(dest => dest.ToDoItems, opt => opt.MapFrom(src => src.ToDoItems));
        }
    }
}
