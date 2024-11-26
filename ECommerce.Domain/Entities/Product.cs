namespace ECommerce.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Model { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public ICollection<ProductDiscount> ProductDiscounts { get; set; } = [];
        public ICollection<ProductImage> ProductImages { get; set; } = [];
        public ICollection<ProductInventory> ProductInventories { get; set; } = [];
        public ICollection<CartItem> CartItems { get; set; } = [];
    }
}
