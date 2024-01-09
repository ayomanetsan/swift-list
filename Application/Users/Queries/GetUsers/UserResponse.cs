using AutoMapper;
using Domain.Entities;

namespace Application.Users.Queries.GetUsers;

public class UserResponse
{
    public Guid Id { get; set; }
    
    public required string Fullname { get; set; }
    
    public required string Email { get; set; }
    
    private class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>();
        }
    }
}
