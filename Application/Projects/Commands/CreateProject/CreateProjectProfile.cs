using AutoMapper;
using Domain.Entities;

namespace Application.Projects.Commands.CreateProject
{
    public class CreateProjectProfile : Profile
    {
        public CreateProjectProfile()
        {
            CreateMap<CreateProjectCommand, Project>();
        }
    }
}
