using ECommerce.Application.DTOs.Auth;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        /// <summary>Sign Up</summary>
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            await _authService.SignUp(model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "SIGNUP successfully!"));
        }

        /// <summary>Sign In and generate token</summary>
        [HttpPost("SignIn")]
        public async Task<IActionResult> Signin(SignInDto model)
        {
            GetSignInDto result = await _authService.SignIn(model);
            return Ok(new BaseResponseModel<GetSignInDto>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Get new tokens for user
        /// </summary>
        [HttpGet("GetNewTokens")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            GetTokenDto result = await _authService.RefreshToken(refreshToken);
            return Ok(new BaseResponseModel<GetTokenDto>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
    }
}