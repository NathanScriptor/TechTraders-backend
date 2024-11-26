namespace ECommerce.Application.DTOs.Order
{
    public class PostOrderItemDto
    {
        public string Model { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double ItemPrice { get; set; }
        public string InventoryId { get; set; } = string.Empty;
    }
}
