using ECommerce.Domain.Enum;

namespace ECommerce.Application.DTOs.Order
{
    public class PostOrderDto
    {
        public string UserId { get; set; } = string.Empty;
        public double Total { get; set; }
        public IEnumerable<PostOrderItemDto> OrderItems { get; set; } = [];
    }
}
