using AutoMapper;
using ECommerce.Application.DTOs.Cart;
using ECommerce.Application.DTOs.CartItem;
using ECommerce.Application.DTOs.Variation;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mapping
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, GetCartDto>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));

            CreateMap<CartItem, GetCartItemDto>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.ProductInventory.Product.Model))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.ProductVariations, opt => opt.MapFrom(src => src.ProductInventory.InventoryVariations.Select(iv => iv.ProductVariation)));

            CreateMap<ProductVariation, GetVariationDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
        }
    }
}
