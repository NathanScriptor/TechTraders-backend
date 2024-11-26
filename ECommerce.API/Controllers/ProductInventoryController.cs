using ECommerce.Application.DTOs.Inventory;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInventoryController : ControllerBase
    {
        private readonly IProductInventoryService _productInventoryService;

        public ProductInventoryController(IProductInventoryService productInventoryService)
        {
            _productInventoryService = productInventoryService;
        }

        /// <summary>
        /// Retrieves ProductInventory by ProductId.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetProductInventoryByProductId(string productId)
        {
            IEnumerable<GetProductInventoryDto> result = await _productInventoryService.GetProductInventoryByProductId(productId);
            return Ok(new BaseResponseModel<IEnumerable<GetProductInventoryDto>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Links Product with Variation.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostProductInventory(PostProductInventoryDto model)
        {
            await _productInventoryService.PostProductInventory(model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "LINK product to variation successfully!"));
        }

        /// <summary>
        /// Changes the quantity.
        /// </summary>
        [HttpPut("ChangeQuantity")]
        public async Task<IActionResult> ChangeQuantity(string inventoryId, int quantity)
        {
            await _productInventoryService.ChangeQuantity(inventoryId, quantity);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "CHANGE quantity successfully!"));
        }

        /// <summary>
        /// Changes the price.
        /// </summary>
        [HttpPut("ChangePrice")]
        public async Task<IActionResult> ChangePrice(string inventoryId, double price)
        {
            await _productInventoryService.ChangePrice(inventoryId, price);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "CHANGE price successfully!"));
        }

        /// <summary>
        /// Deletes the link between Product and Variation.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteProductInventory(string id)
        {
            await _productInventoryService.DeleteProductInventory(id);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "DELETED ProductInventory successfully!"));
        }
    }
}