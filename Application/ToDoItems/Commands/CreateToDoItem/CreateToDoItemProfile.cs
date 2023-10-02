using AutoMapper;
using Domain.Entities;

namespace Application.ToDoItems.Commands.CreateToDoItem
{
    public class CreateToDoItemProfile : Profile
    {
        public CreateToDoItemProfile() 
        {
            CreateMap<CreateToDoItemCommand, ToDoItem>();
        }
    }
}
