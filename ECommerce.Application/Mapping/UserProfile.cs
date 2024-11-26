using AutoMapper;
using ECommerce.Application.DTOs.User;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<User, PutUserDto>().ReverseMap();
        }
    }
}
