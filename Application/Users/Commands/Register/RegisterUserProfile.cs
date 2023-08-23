using AutoMapper;
using Domain.Entities;

namespace Application.Users.Commands.Register
{
    public class RegisterUserProfile : Profile
    {
        public RegisterUserProfile()
        {
            CreateMap<RegisterUserCommand, User>();
        }
    }
}
