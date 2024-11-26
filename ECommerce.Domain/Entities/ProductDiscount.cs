namespace ECommerce.Domain.Entities
{
    public class ProductDiscount : BaseEntity
    {
        public string ProductId { get; set; } = string.Empty;
        public Product? Product { get; set; }
        public string DiscountId { get; set; } = string.Empty;
        public Discount? Discount { get; set; }
    }
}
