using ECommerce.Application.DTOs.InventoryVariation;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface IInventoryVariationService
    {
        Task LinkInventoryWithVariation(PostInventoryVariationDto model);
        Task DeleteInventoryVariation(string inventoryVariationId);
    }
}
