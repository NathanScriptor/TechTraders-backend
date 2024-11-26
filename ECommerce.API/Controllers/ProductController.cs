using ECommerce.Application.DTOs.Product;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves a product by ID.
        /// </summary>
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(string productId)
        {
            var result = await _productService.GetProductById(productId);
            return Ok(new BaseResponseModel<GetProductDto>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();
            return Ok(new BaseResponseModel<IEnumerable<GetProductDto>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        /// <summary>
        /// Retrives products based on filters
        /// </summary>
        [HttpGet("FilteredProducts")]
        public async Task<IActionResult> GetFilteredProducts([FromQuery] string? brand, [FromQuery] string? priceRange, [FromQuery] string? capacity, [FromQuery] string? sortBy)
        {
            var products = await _productService.FilterProducts(brand, priceRange, capacity, sortBy);
            return Ok(new BaseResponseModel<IEnumerable<GetProductDto>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: products));
        }

        /// <summary>
        /// Retrieves products with pagination.
        /// </summary>
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(int index, int pageSize)
        {
            var result = await _productService.GetProduct(index, pageSize);
            return Ok(new BaseResponseModel<IEnumerable<GetProductDto>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Searches for products by name.
        /// </summary>
        [HttpGet("SearchProductByName")]
        public async Task<IActionResult> SearchProduct(string productName)
        {
            var result = await _productService.SearchProduct(productName);
            return Ok(new BaseResponseModel<IEnumerable<GetProductDto>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Adds product information.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostProduct(PostProductDto model)
        {
            await _productService.PostProduct(model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "CREATED PRODUCT successfully!"));
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            await _productService.DeleteProduct(productId);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "DELETED PRODUCT successfully!"));
        }
    }
}