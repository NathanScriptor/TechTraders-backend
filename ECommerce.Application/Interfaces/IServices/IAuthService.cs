using ECommerce.Application.DTOs.Auth;
using ECommerce.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<GetUserDto> GetInfo();
        Task<GetSignInDto> SignIn(SignInDto request);
        Task SignUp(SignUpDto model);
        Task<GetTokenDto> RefreshToken(string refreshToken);
    }
}
