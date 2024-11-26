using AutoMapper;
using ECommerce.Application.DTOs.Inventory;
using ECommerce.Application.DTOs.Product;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mapping
{
    public class ProductInventoryProfile : Profile
    {
        public ProductInventoryProfile()
        {
            CreateMap<ProductInventory, GetProductInventoryDto>();
            CreateMap<ProductInventory, PostProductInventoryDto>().ReverseMap();
            CreateMap<ProductInventory, ProductInventoryDto>();
        }
    }
}
