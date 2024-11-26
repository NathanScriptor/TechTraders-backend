using AutoMapper;
using ECommerce.Application.DTOs.UserAddress;
using ECommerce.Application.Interfaces.IRepositories;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Service
{
    public class UserAddressService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IUserAddressService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly IMapper _mapper = mapper;

        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private string GetCurrentUserId => Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
        public async Task<IEnumerable<GetUserAddressDto>> GetUserAddress()
        {
            IEnumerable<UserAddress> result = await _unitOfWork.GetRepository<UserAddress>()
                    .Entities.Where(p => p.UserId == GetCurrentUserId).ToListAsync();
            return _mapper.Map<IEnumerable<GetUserAddressDto>>(result);
        }
            
        public async Task PostUserAddress(PostUserAddressDto model)
        {
            string userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            await _unitOfWork.GetRepository<UserAddress>().InsertAsync(new UserAddress()
            {
                AddressLine = model.AddressLine,
                City = model.City,
                Country = model.Country,
                UserId = userId
            });
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUserAddress(string userAddressId)
        {
            UserAddress? userAddress = await _unitOfWork.GetRepository<UserAddress>().GetByIdAsync(userAddressId)
            ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This userAddress is not found!");

            await _unitOfWork.GetRepository<UserAddress>().DeleteAsync(userAddressId);
            await _unitOfWork.SaveAsync();
        }

        public async Task SetDefaultAddress(string userAddressId)
        {
            UserAddress? currentUserAddress = await _unitOfWork.GetRepository<UserAddress>().Entities
                .FirstOrDefaultAsync(obj => obj.UserId == GetCurrentUserId && obj.DefaultAddress == true);

            if (currentUserAddress != null)
            {
                currentUserAddress.DefaultAddress = false;
            }

            UserAddress? userAddress = await _unitOfWork.GetRepository<UserAddress>().GetByIdAsync(userAddressId)
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This userAddress is not found!");

            userAddress.DefaultAddress = true;

            var addressesToUpdate = new List<UserAddress>();

            if (currentUserAddress != null)
            {
                addressesToUpdate.Add(currentUserAddress);
            }

            addressesToUpdate.Add(userAddress);

            await _unitOfWork.GetRepository<UserAddress>().UpdateRangeAsync(addressesToUpdate);
            await _unitOfWork.SaveAsync();

        }
    }
}
