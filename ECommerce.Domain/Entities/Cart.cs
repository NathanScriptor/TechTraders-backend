namespace ECommerce.Domain.Entities
{
    public class Cart : BaseEntity
    { 
        public double? Total { get; set; }
        public string UserId { get; set; } = String.Empty;
        public User? User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = [];
    }
}
