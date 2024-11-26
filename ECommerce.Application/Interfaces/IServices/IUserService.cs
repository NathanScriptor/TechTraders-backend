using ECommerce.Application.DTOs.User;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<GetUserDto> GetCurrentUser();
        Task<GetUserDto> GetUserByUsername(string username);
        Task PutUser(string id, PutUserDto model);
        Task<string> GetRole();
        Task<string> GetUserName();
    }
}
