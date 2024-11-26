using ECommerce.Application.DTOs.Auth;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface ITokenService
    {
        public GetTokenDto GenerateTokens(User user);
    }
}
