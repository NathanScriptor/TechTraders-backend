using AutoMapper;
using ECommerce.Application.DTOs.User;
using ECommerce.Application.Interfaces.IRepositories;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Service
{
    public class UserService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public async Task<GetUserDto> GetCurrentUser()
        {
            string userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            return _mapper.Map<GetUserDto>(
                await _unitOfWork.GetRepository<User>().GetByIdAsync(userId));
        }

        public async Task<string> GetRole()
        {
            string userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            return _unitOfWork.GetRepository<User>().GetByIdAsync(userId).Result.Role;
        }

        public async Task<string> GetUserName()
        {
            string userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            User? user = await _unitOfWork.GetRepository<User>().GetByIdAsync(userId);
            return user.LastName;
        }

        public async Task<GetUserDto> GetUserByUsername(string username)
        {
            User? user = await _unitOfWork.GetRepository<User>().Entities.FirstOrDefaultAsync(obj => obj.Username == username)
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This username is not found!");
            return _mapper.Map<GetUserDto>(user);
        }
        public async Task PutUser(string id, PutUserDto model)
        {
            User? user = await _unitOfWork.GetRepository<User>().GetByIdAsync(id)
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This user is not found!");

            _mapper.Map(model, user);
            await _unitOfWork.GetRepository<User>().UpdateAsync(user);
            await _unitOfWork.SaveAsync();
        }
    }
}
