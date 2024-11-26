using AutoMapper;
using ECommerce.Application.DTOs.Variation;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mapping
{
    public class ProductVariationProfile : Profile
    {
        public ProductVariationProfile()
        {
            CreateMap<ProductVariation, GetVariationDto>().ReverseMap();
        }
    }
}
