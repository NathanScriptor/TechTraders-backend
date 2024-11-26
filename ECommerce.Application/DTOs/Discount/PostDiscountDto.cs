namespace ECommerce.Application.DTOs.Discount
{
    public class PostDiscountDto
    {
        public required string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public required double DiscountPercent { get; set; }
        public required bool IsActive { get; set; }
        public required DateTime BeganAt { get; set; }
        public required DateTime ExpiredAt { get; set; } 
    }
}
