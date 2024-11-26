using ECommerce.Application.DTOs.Product;
using AutoMapper;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductDto>();

            CreateMap<ProductInventory, ProductInventoryDto>()
                .ForMember(dest => dest.InventoryVariations, opt => opt.MapFrom(src => src.InventoryVariations));

            CreateMap<InventoryVariation, InventoryVariationDto>()
                .ForMember(dest => dest.ProductVariationType, opt => opt.MapFrom(src => src.ProductVariation.Type))
                .ForMember(dest => dest.ProductVariationName, opt => opt.MapFrom(src => src.ProductVariation.Name));
        }
    }

}
