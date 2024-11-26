namespace ECommerce.Application.DTOs.Product
{
    public class PutProductDto
    {
        public string Model { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = String.Empty;
        public double Price { get; set; }
    }
}
