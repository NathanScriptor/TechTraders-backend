using ECommerce.Application.DTOs.User;
using ECommerce.Application.DTOs.UserAddress;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly IUserAddressService _userAddressService;

        public UserAddressController(IUserAddressService userAddressService)
        {
            _userAddressService = userAddressService;
        }

        /// <summary>
        /// Retrieves the user's address information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUserAddress()
        {
            IEnumerable<GetUserAddressDto> result = await _userAddressService.GetUserAddress();
            return Ok(new BaseResponseModel<IEnumerable<GetUserAddressDto>>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: result));
        }

        /// <summary>
        /// Adds a user's address.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostUserAddress(PostUserAddressDto model)
        {
            await _userAddressService.PostUserAddress(model);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "CREATED userAddress successfully!"));
        }

        /// <summary>
        /// Deletes a user's address.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAddress(string userAddressId)
        {
            await _userAddressService.DeleteUserAddress(userAddressId);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "DELETED userAddress successfully!"));
        }

        /// <summary>
        /// Sets the default user address.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> SetDefaultAddress(string userAddressId)
        {
            await _userAddressService.SetDefaultAddress(userAddressId);
            return Ok(new BaseResponseModel<string>(
                statusCode: StatusCodes.Status200OK,
                code: ResponseCodeConstants.SUCCESS,
                data: "SET default userAddress successfully!"));
        }
    }
}