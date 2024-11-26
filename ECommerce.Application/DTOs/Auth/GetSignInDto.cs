using ECommerce.Application.DTOs.User;

namespace ECommerce.Application.DTOs.Auth
{
    public class GetSignInDto
    {
        public GetUserDto Person { get; set; } = new GetUserDto();
        public GetTokenDto Token { get; set; } = new GetTokenDto();
    }
}
