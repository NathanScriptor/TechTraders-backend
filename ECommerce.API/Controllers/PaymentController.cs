using ECommerce.Application.DTOs.VNPay;
using ECommerce.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        public PaymentController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }
        /// <summary>
        /// Create a vnpay payment url
        /// </summary>
        [HttpPost("CreatePaymentUrlVnpay")]
        public string CreatePaymentUrlVnpay([FromBody]PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return url;
        }

        /// <summary>
        /// Call back the vnpay's payment
        /// </summary>
        [HttpGet("PaymentCallbackVnpay")]
        public IActionResult PaymentCallbackVnpay()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Ok(response.VnPayResponseCode);
        }

    }
}
