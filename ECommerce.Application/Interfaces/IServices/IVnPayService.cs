using ECommerce.Application.DTOs.VNPay;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);

    }
}
