using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface IProductImageService
    {
        Task UploadImage(List<IFormFile> files, string productId);
        Task DeleteImage(string imageId);
    }
}
