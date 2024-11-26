using ECommerce.Application.DTOs.Variation;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariationController : ControllerBase
    {
        private readonly IProductVariationService _productVariationService;

        public ProductVariationController(IProductVariationService productVariationService)
        {
            _productVariationService = productVariationService;
        }

        /// <summary>
        /// Retrieves a list of Variations if the ID is null or the variation from that ID.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetVariation(string? variationId)
        {
            IEnumerable<GetVariationDto> result = await _productVariationService.GetVariation(variationId);
            return Ok(new BaseResponseModel<IEnumerable<GetVariationDto>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Adds a new Variation.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostVariation(PostVariationDto model)
        {
            await _productVariationService.PostVariation(model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "CREATED new variation successfully!"));
        }
    }
}