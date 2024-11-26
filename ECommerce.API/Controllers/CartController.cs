using ECommerce.Application.DTOs.Cart;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Retrieves the list of products in the user's cart.
        /// </summary>
        [HttpGet("GetCartForUser")]
        public async Task<IActionResult> GetCart()
        {
            GetCartDto result = await _cartService.GetCartAsync();
            return Ok(new BaseResponseModel<GetCartDto>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Adds a product to the cart.
        /// </summary>
        [HttpPut("AddItemToCart")]
        public async Task<IActionResult> AddProductToCart(string productId, string inventoryId, int quantity, bool isModify = false)
        {
            await _cartService.AddProductToCart(productId, inventoryId, quantity, isModify);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Product added to cart successfully!"));
        }
        /// <summary>
        /// Change quantity by one
        /// </summary>
        [HttpPut("ChangeQuantityByOne")]
        public async Task<IActionResult> ChangeQuantityByOne(string cartId, string inventoryId, string option)
        {
            await _cartService.ChangeQuantityByOne(cartId, inventoryId, option);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "CHANGED quantity successfully!"));
        }

        /// <summary>
        /// Removes a product from the cart by inventoryId.
        /// </summary>
        [HttpDelete("RemoveItemOutOfCart")]
        public async Task<IActionResult> RemoveProductOutOfCart(string inventoryId)
        {
            await _cartService.RemoveProductFromCart(inventoryId);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "Product removed from cart successfully!"));
        }
    }
}