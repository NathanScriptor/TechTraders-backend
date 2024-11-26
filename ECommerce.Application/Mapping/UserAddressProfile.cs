using AutoMapper;
using ECommerce.Application.DTOs.UserAddress;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mapping
{
    public class UserAddressProfile : Profile
    {
        public UserAddressProfile()
        {
            CreateMap<UserAddress, GetUserAddressDto>().ReverseMap();
        }
    }
}
