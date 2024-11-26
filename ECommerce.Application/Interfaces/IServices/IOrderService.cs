using ECommerce.Application.DTOs.Order;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<GetOrderDto>> GetAllOrders();
        Task<GetOrderDto> GetOrderDetail(string orderId);
        Task<IEnumerable<GetOrderDto>> GetOrdersByUser(string userId);
        Task<string> PostOrder();
        Task PostOrderItem(string orderId, PostOrderItemDto model);
    }
}
