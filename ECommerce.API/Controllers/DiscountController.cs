using ECommerce.Application.DTOs.Discount;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        /// <summary>
        /// Retrieves discount information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDiscount(string id)
        {
            GetDiscountDto result = await _discountService.GetDiscount(id);
            return Ok(new BaseResponseModel<GetDiscountDto>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Creates a new discount.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostDiscount(PostDiscountDto model)
        {
            await _discountService.PostDiscount(model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "CREATE DISCOUNT successfully!"));
        }

        /// <summary>
        /// Updates discount information.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> PutDiscount(string id, PutDiscountDto model)
        {
            await _discountService.PutDiscount(id, model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "MODIFY DISCOUNT successfully!"));
        }

        /// <summary>
        /// Deletes a discount.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteDiscount(string id)
        {
            await _discountService.DeleteDiscount(id);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "DELETE DISCOUNT successfully!"));
        }
    }
}