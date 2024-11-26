using AutoMapper;
using ECommerce.Application.DTOs.Order;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Order, GetOrderDto>();
            CreateMap<OrderItem, PostOrderDto>().ReverseMap();
        }
    }
}
