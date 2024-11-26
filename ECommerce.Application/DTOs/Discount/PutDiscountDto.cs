namespace ECommerce.Application.DTOs.Discount
{
    public class PutDiscountDto
    {
        public required string Name { get; set; }
        public required string Description{ get; set; }
        public required double DiscountPercent { get; set; }
        public required bool IsActive { get; set; }
        public required DateTime BeganAt { get; set; }
        public required DateTime ExpiredAt { get; set; }
    }
}
