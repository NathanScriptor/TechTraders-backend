using ECommerce.Application.Others;
using System.Net;
using System.Text.Json;

namespace ECommerce.API.Middleware
{
    public class AuthenticationHandlingMiddleware(RequestDelegate next, ILogger<AuthenticationHandlingMiddleware> logger, IHttpContextAccessor contextAccessor)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<AuthenticationHandlingMiddleware> _logger = logger;
        private readonly IEnumerable<string> _excludedUris =
            [
                "/api/Auth/SignIn",
                "/api/Auth/SignUp",
                "/api/Auth/GetNewTokens",
                "/api/Cart/AddItemToCart",
                "/api/Cart/RemoveItemOutOfCart",
                "/api/Product/GetProduct",
                "/api/Product/GetAllProducts",
                "/api/Product/FilteredProducts",
                "/api/Product/GetProductById"
            ];
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

        public async Task Invoke(HttpContext context)
        {
            if (HasPermission(context))
            {
                await _next(context);
            }
            else
            {
                var code = HttpStatusCode.Forbidden;
                var result = JsonSerializer.Serialize(new { error = "You don't have permission to access this feature" });
                context.Response.ContentType = "application/json";
                context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                context.Response.StatusCode = (int)code;
                await context.Response.WriteAsync(result);
            }
        }

        private bool HasPermission(HttpContext context)
        {
            string requestUri = context.Request.Path.Value!;
            if (_excludedUris.Contains(requestUri) || !requestUri!.StartsWith("/api/")) return true;
            string idUser = "";
            if (_contextAccessor != null)
            {
                idUser = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            }
            try
            {
                {
                    if (idUser != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking permissions");
                return false;
            }
        }
    }
}
