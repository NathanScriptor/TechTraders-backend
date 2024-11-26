using ECommerce.Application.DTOs.Discount;
using ECommerce.Domain.Entities;
using AutoMapper;

namespace ECommerce.Application.Mapping
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Discount, GetDiscountDto>().ReverseMap();
            CreateMap<Discount, PutDiscountDto>().ReverseMap();
        }
    }
}
