namespace ECommerce.Domain.Entities
{
    public class ProductInventory : BaseEntity
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public Product? Product { get; set; }
        public virtual ICollection<InventoryVariation> InventoryVariations { get; set; } = [];
        public virtual ICollection<CartItem> CartItems { get; set; } = [];
        public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
