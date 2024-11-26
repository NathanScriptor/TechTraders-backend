namespace ECommerce.Application.DTOs.Inventory
{
    public class PostProductInventoryDto
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ProductId { get; set; } = string.Empty;
    }
}
