using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _productImageService;
        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }
        /// <summary>
        /// Uploads images for a product.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UploadImage(List<IFormFile> files, string productId)
        {
            await _productImageService.UploadImage(files, productId);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Uploaded images successfully!"));
        }
        /// <summary>
        /// Deletes an image.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteImage(string imageId)
        {
            await _productImageService.DeleteImage(imageId);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Deleted images successfully!"));
        }
    }
}