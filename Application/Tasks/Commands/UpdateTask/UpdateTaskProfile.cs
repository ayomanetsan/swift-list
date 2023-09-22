using AutoMapper;

namespace Application.Tasks.Commands.UpdateTask
{
    public class UpdateTaskProfile : Profile
    {
        public UpdateTaskProfile()
        {
            // How do I correctly map the labels?
            // How do I correctly map the ToDoItems?
            // Write me the code to do it


            CreateMap<UpdateTaskCommand, Domain.Entities.Task>()
                .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.Labels))
                .ForMember(dest => dest.ToDoItems, opt => opt.MapFrom(src => src.ToDoItems));
                

        }
    }
}
