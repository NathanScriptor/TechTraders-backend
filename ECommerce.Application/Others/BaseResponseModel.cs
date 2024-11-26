using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Others
{
    public class BaseResponseModel<T>(int statusCode, string code, T? data, string? message = null)
    {
        public T? Data { get; set; } = data;
        public string? Message { get; set; } = message;
        public int StatusCode { get; set; } = statusCode;
        public string Code { get; set; } = code;

        public static BaseResponseModel<T> OkResponseModel(T data, string code = ResponseCodeConstants.SUCCESS)
        {
            return new BaseResponseModel<T>(StatusCodes.Status200OK, code, data);
        }

        public static BaseResponseModel<T> NotFoundResponseModel(T? data, string code = ResponseCodeConstants.NOT_FOUND)
        {
            return new BaseResponseModel<T>(StatusCodes.Status404NotFound, code, data);
        }

        public static BaseResponseModel<T> BadRequestResponseModel(T? data, string code = ResponseCodeConstants.FAILED)
        {
            return new BaseResponseModel<T>(StatusCodes.Status400BadRequest, code, data);
        }

        public static BaseResponseModel<T> InternalErrorResponseModel(T? data, string code = ResponseCodeConstants.FAILED)
        {
            return new BaseResponseModel<T>(StatusCodes.Status500InternalServerError, code, data);
        }
    }

    public class BaseResponseModel : BaseResponseModel<object>
    {
        public BaseResponseModel(int statusCode, string code, object? data, string? message = null) : base(statusCode, code, data, message)
        {
        }
        public BaseResponseModel(int statusCode, string code, string? message) : base(statusCode, code, message)
        {
        }
    }
}
