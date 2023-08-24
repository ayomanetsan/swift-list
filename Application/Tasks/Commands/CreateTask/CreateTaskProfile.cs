using AutoMapper;
using Task = Domain.Entities.Task;

namespace Application.Tasks.Commands.CreateTask
{
    public class CreateTaskProfile : Profile
    {
        public CreateTaskProfile() 
        {
            CreateMap<CreateTaskCommand, Task>();
        }
    }
}
