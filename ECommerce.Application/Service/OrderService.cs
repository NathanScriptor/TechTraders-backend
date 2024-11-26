using AutoMapper;
using ECommerce.Application.DTOs.Order;
using ECommerce.Application.Interfaces.IRepositories;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string GetCurrentUserId => Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<GetOrderDto>> GetAllOrders()
        {
            IEnumerable<Order> orders = await _unitOfWork.GetRepository<Order>().Entities.ToListAsync();
            return _mapper.Map<IEnumerable<GetOrderDto>>(orders);
        }

        public async Task<GetOrderDto> GetOrderDetail (string orderId)
        {
            Order? order = await _unitOfWork.GetRepository<Order>().Entities.FirstOrDefaultAsync(o => o.Id == orderId);
            return _mapper.Map<GetOrderDto>(order);
        }
        
        public async Task<IEnumerable<GetOrderDto>> GetOrdersByUser(string userId)
        {
            IEnumerable<Order> orders = await _unitOfWork.GetRepository<Order>().Entities.Where(o => o.UserId == userId).ToListAsync();
            return _mapper.Map<IEnumerable<GetOrderDto>>(orders);
        }

        public async Task<string> PostOrder()
        {
            string orderId = Guid.NewGuid().ToString("N");
            Order order = new()
            {
                Id = orderId,
                Status = OrderStatus.Pending,
                UserId = GetCurrentUserId,
            };
            await _unitOfWork.GetRepository<Order>().InsertAsync(order);
            await _unitOfWork.SaveAsync();
            return orderId; 
        }

        public async Task PostOrderItem(string orderId, PostOrderItemDto model)
        {
            Order? order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(orderId);

            OrderItem orderItem = new()
            {
                Model = model.Model,
                OrderId = orderId,
                ItemPrice = model.ItemPrice,
                Quantity = model.Quantity,
                ProductInventoryId = model.InventoryId,
            };

            order.Total += model.ItemPrice * model.Quantity;

            await _unitOfWork.GetRepository<OrderItem>().InsertAsync(orderItem);
            await _unitOfWork.SaveAsync();
        }
    }
}
