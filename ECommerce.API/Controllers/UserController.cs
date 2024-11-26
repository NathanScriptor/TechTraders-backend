using ECommerce.Application.DTOs.User;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves the current user's information.
        /// </summary>
        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            GetUserDto result = await _userService.GetCurrentUser();
            return Ok(new BaseResponseModel<GetUserDto>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }
        /// <summary>
        /// Get current user's lastname
        /// </summary>
        [HttpGet("GetUserName")]
        public async Task<IActionResult> GetUserName()
        {
            string lastname = await _userService.GetUserName();
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: lastname));
        }

        /// <summary>
        /// Get current user's role
        /// </summary>
        [HttpGet("GetRole")]
        public async Task<IActionResult> GetRole()
        {
            string result = await _userService.GetRole();
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Retrieves user information by username.
        /// </summary>
        [HttpGet("GetUserByUsername")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            GetUserDto result = await _userService.GetUserByUsername(username);
            return Ok(new BaseResponseModel<GetUserDto>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Updates user information.
        /// </summary>
        [HttpPut("PutUser")]
        public async Task<IActionResult> PutUser(string id, PutUserDto model)
        {
            await _userService.PutUser(id, model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "UPDATE USER successfully!"));
        }
    }
}