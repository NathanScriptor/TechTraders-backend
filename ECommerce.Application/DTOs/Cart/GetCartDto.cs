
using ECommerce.Application.DTOs.CartItem;

namespace ECommerce.Application.DTOs.Cart
{
    public class GetCartDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public ICollection<GetCartItemDto> CartItems { get; set; } = [];
        public double? Total { get; set; }
    }
}
