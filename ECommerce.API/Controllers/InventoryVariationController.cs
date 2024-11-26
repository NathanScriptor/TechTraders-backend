using ECommerce.Application.DTOs.InventoryVariation;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryVariationController(IInventoryVariationService inventoryVariationService) : ControllerBase
    {
        private readonly IInventoryVariationService _inventoryVariationService = inventoryVariationService;
        /// <summary>
        /// Link Inventory with Variation
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> LinkInventoryWithVariation(PostInventoryVariationDto model)
        {
            await _inventoryVariationService.LinkInventoryWithVariation(model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "LINKED successfully!"));
        }
        /// <summary>
        /// Delete connection between Inventory and Variation
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteInventoryVariation(string id)
        {
            await _inventoryVariationService.DeleteInventoryVariation(id);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "DELETED successfully!"));
        }
    }
}
