using AutoMapper;

namespace Application.Labels.Commands.CreateLabel
{
    public class CreateLabelProfile : Profile
    {
        public CreateLabelProfile()
        {
            CreateMap<CreateLabelCommand, Domain.Entities.Label>();
        }
    }
}
