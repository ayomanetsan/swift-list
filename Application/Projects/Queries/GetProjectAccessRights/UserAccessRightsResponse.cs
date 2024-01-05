using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Projects.Queries.GetProjectAccessRights;

public record UserAccessRightsResponse
{
    public required string Fullname { get; set; }
    
    public required string Email { get; set; }
    
    public AccessRights AccessRights { get; set; }

    private class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProjectUsers, UserAccessRightsResponse>()
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.User.Fullname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
        }
    }
}
