using ECommerce.Application.DTOs.Product;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface IProductService
    {
        Task<GetProductDto> GetProductById(string id);
        Task<IEnumerable<GetProductDto>> GetAllProducts();
        Task<IEnumerable<GetProductDto>> GetProduct(int index, int pageSize);
        Task<double> GetDiscountedPrice(string productId, string inventoryId);
        Task<IEnumerable<GetProductDto>> FilterProducts(string? brand, string? priceRange, string? capacity, string? sortBy);
        Task PostProduct(PostProductDto model);
        Task DeleteProduct(string productId);
        Task<IEnumerable<GetProductDto>> SearchProduct(string productName);
    }
}
