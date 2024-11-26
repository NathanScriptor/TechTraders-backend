namespace ECommerce.Application.DTOs.Product
{
    public class PostProductDto
    {
        public required string Model { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
    }
}
