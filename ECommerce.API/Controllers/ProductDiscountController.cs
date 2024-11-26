using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDiscountController : ControllerBase
    {
        private readonly IProductDiscountService _productDiscountService;

        public ProductDiscountController(IProductDiscountService productDiscountService)
        {
            _productDiscountService = productDiscountService;
        }

        /// <summary>
        /// Links a product with a discount.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostProductDiscount(string discountId, string productId)
        {
            await _productDiscountService.PostProductDiscount(discountId, productId);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "CREATED PRODUCT DISCOUNT successfully!"));
        }
    }
}