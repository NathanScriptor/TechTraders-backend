namespace ECommerce.Domain.Entities
{
    public class InventoryVariation
    {
        public string Id { get; set; }

        public InventoryVariation()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        public string ProductInventoryId { get; set; } = string.Empty;
        public ProductInventory? ProductInventory { get; set; }
        public string ProductVariationId { get; set; } = string.Empty;
        public ProductVariation? ProductVariation { get; set; }
    }
}
