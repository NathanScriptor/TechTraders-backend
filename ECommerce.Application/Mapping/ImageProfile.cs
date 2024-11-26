using AutoMapper;
using ECommerce.Application.DTOs.Image;
using ECommerce.Application.DTOs.Product;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mapping
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<ProductImage, GetImageDto>().ReverseMap();
        }
    }
}
