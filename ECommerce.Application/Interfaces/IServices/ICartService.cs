using ECommerce.Application.DTOs.Cart;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface ICartService
    {
        Task<GetCartDto> GetCartAsync();
        Task AddProductToCart(string productId, string variationId, int quantity, bool isModify);
        Task RemoveProductFromCart(string productId);
        Task ChangeQuantityByOne(string cartId, string itemId, string option);
    }
}