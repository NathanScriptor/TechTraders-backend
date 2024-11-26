namespace ECommerce.Application.DTOs.Product
{
    public class GetProductDto
    {
        public string Id { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ProductInventoryDto> ProductInventories { get; set; } = new();
        public List<ProductImageDto> ProductImages { get; set; } = new();
    }

    public class ProductImageDto
    {
        public string ImageLink { get; set; }
        public bool IsDefaultImage { get; set; }
    }

    public class ProductInventoryDto
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public double OriginalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public List<InventoryVariationDto> InventoryVariations { get; set; } = new();
    }

    public class InventoryVariationDto
    {
        public string ProductVariationType { get; set; } = string.Empty;
        public string ProductVariationName { get; set; } = string.Empty;
    }
}
