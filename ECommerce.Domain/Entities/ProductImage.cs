namespace ECommerce.Domain.Entities
{
    public class ProductImage
    {
        public ProductImage()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        public string Id { get; set; }
        public string ImageLink { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public bool IsDefaultImage { get; set; }
        public Product? Product { get; set; }
    }
}
