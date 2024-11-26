using ECommerce.Application.DTOs.Inventory;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface IProductInventoryService
    {
        Task<IEnumerable<GetProductInventoryDto>> GetProductInventoryByProductId(string productId);
        Task PostProductInventory(PostProductInventoryDto model);
        Task ChangeQuantity(string inventoryId, int quantity);
        Task ChangePrice(string inventoryId, double price);
        Task DeleteProductInventory(string id);
    }
}
