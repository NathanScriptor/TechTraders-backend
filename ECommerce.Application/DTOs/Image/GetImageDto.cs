namespace ECommerce.Application.DTOs.Image
{
    public class GetImageDto
    {
        public string ImageLink { get; set; } = string.Empty;
        public bool IsDefaultImage { get; set; }
    }
}
