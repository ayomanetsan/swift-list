using AutoMapper;

namespace Application.Tasks.Queries.GetTaskWithDetails
{
    public class LabelResponse
    {
        public required string Title { get; set; }

        public required string Color { get; set; }

        private class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Domain.Entities.Label, LabelResponse>();
            }
        }
    }
}
