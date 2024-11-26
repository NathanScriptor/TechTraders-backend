namespace ECommerce.Domain.Entities
{
    public class Discount : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double DiscountPercent { get; set; }
        public bool IsActive { get; set; }
        public DateTime BeganAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<ProductDiscount> ProductDiscounts { get; set; } = [];
    }
}
