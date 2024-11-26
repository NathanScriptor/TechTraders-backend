using ECommerce.Application.DTOs.Order;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            IEnumerable<GetOrderDto> result = await _orderService.GetAllOrders();
            return Ok(new BaseResponseModel<IEnumerable<GetOrderDto>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Retrieves order details.
        /// </summary>
        [HttpGet("GetOrderDetail")]
        public async Task<IActionResult> GetOrderDetail(string orderId)
        {
            GetOrderDto result = await _orderService.GetOrderDetail(orderId);
            return Ok(new BaseResponseModel<GetOrderDto>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Retrieves orders by user.
        /// </summary>
        [HttpGet("GetOrdersByUser")]
        public async Task<IActionResult> GetOrdersByUser(string userId)
        {
            IEnumerable<GetOrderDto> result = await _orderService.GetOrdersByUser(userId);
            return Ok(new BaseResponseModel<IEnumerable<GetOrderDto>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        [HttpPost("CreateNewOrder")]
        public async Task<IActionResult> PostOrder()
        {
            string orderId = await _orderService.PostOrder();
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: orderId));
        }
        
        /// <summary>
        /// Attach new item to an order
        /// </summary>
        [HttpPost("AttachItemToOrder")]
        public async Task<IActionResult> PostOrderItem(string orderId, PostOrderItemDto model)
        {
            await _orderService.PostOrderItem(orderId, model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "ATTACHED ITEMS TO ORDER successfully!"));
        }
    }
}