using ECommerce.Application.DTOs.Variation;

namespace ECommerce.Application.DTOs.CartItem
{
    public class GetCartItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public double ItemPrice { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; } = string.Empty;
        public string ProductInventoryId { get; set; } = string.Empty;
        public ICollection<GetVariationDto> ProductVariations { get; set; }
    }
}
