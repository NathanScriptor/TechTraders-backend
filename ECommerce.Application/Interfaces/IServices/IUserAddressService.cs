using ECommerce.Application.DTOs.UserAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface IUserAddressService
    {
        Task<IEnumerable<GetUserAddressDto>> GetUserAddress();
        Task PostUserAddress(PostUserAddressDto model);
        Task DeleteUserAddress(string userAddressId);
        Task SetDefaultAddress(string userAddressId);
    }
}
