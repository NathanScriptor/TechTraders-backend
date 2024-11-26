namespace ECommerce.Application.DTOs.Cart
{
    public class PutCartDto
    {
        public ICollection<string> ProductIds { get; set; } = [];
    }
}
