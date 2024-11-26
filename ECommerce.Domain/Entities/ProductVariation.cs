namespace ECommerce.Domain.Entities
{
    public class ProductVariation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ICollection<InventoryVariation> InventoryVariations { get; set; } = [];
    }
}
