using System.Text.Json.Serialization;

namespace ECommerce.Domain.Entities
{
    public class CartItem
    {
        public CartItem()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        public string Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public double ItemPrice { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; } =  string.Empty;
        public string ProductInventoryId { get; set; } = string.Empty;  
        public ProductInventory? ProductInventory { get; set; }
        public string CartId { get; set; } = string.Empty;
        public Cart? Cart { get; set; }
        
    }
}
