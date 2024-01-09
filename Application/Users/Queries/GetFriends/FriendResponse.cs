using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Users.Queries.GetFriends;

public class FriendResponse
{
    public required string UserFullname { get; set; }
    
    public required string UserEmail { get; set; }
    
    public required string RequesterFullname { get; set; }
    
    public required string RequesterEmail { get; set; }
    
    public FriendshipStatus FriendshipStatus { get; set; }

    private class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Friend, FriendResponse>()
                .ForMember(dest => dest.UserFullname, opt => opt.MapFrom(src => src.User.Fullname))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.RequesterFullname, opt => opt.MapFrom(src => src.Requester.Fullname))
                .ForMember(dest => dest.RequesterEmail, opt => opt.MapFrom(src => src.Requester.Email));
        }
    }
}
