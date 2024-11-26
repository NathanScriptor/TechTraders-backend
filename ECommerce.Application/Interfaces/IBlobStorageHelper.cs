using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Interfaces
{
    public interface IBlobStorageHelper
    {
        Task<string> UploadFileAsync(IFormFile? file);
        Task DeleteFileAsync(string fileName);
    }
}
