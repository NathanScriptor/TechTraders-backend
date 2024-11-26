using ECommerce.Domain.Enum;

namespace ECommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public double Total { get; set; }
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
        public OrderStatus Status { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
