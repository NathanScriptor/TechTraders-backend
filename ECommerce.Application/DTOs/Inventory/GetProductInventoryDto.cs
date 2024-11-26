using ECommerce.Domain.Entities;

namespace ECommerce.Application.DTOs.Inventory
{
    public class GetProductInventoryDto
    {
        public string ProductId { get; set; } = string.Empty;
        public ECommerce.Domain.Entities.Product? Product { get; set; }
        public string ProductVariationId { get; set; } = string.Empty;
        public ProductVariation? ProductVariation { get; set; }
        public int Quantity { get; set; }
    }
}
