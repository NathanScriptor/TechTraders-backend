namespace ECommerce.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public string Model { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double ItemPrice { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public Order? Order { get; set; }
        public string ProductInventoryId { get; set; } = string.Empty;
        public ProductInventory? ProductInventory { get; set; }
    }
}
