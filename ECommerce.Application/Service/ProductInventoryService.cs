using AutoMapper;
using ECommerce.Application.DTOs.Inventory;
using ECommerce.Application.Interfaces.IRepositories;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Service
{
    public class ProductInventoryService(IUnitOfWork unitOfWork, IMapper mapper) : IProductInventoryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<GetProductInventoryDto>> GetProductInventoryByProductId(string productId)
        {
            IEnumerable<ProductInventory> productInventories = await _unitOfWork.GetRepository<ProductInventory>().Entities.Where(obj => obj.ProductId == productId).ToListAsync();
            return _mapper.Map<IEnumerable<GetProductInventoryDto>>(productInventories);
        }

        public async Task PostProductInventory(PostProductInventoryDto model)
        {
            if (string.IsNullOrEmpty(model.ProductId))
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This productId is null!");
            }

            // Check if the product exists
            var productExists = await _unitOfWork.GetRepository<Product>().Entities.AnyAsync(p => p.Id == model.ProductId);
            if (!productExists)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This product is not found!");
            }

            // Insert the new ProductInventory
            await _unitOfWork.GetRepository<ProductInventory>().InsertAsync(_mapper.Map<ProductInventory>(model));
            await _unitOfWork.SaveAsync();
        }

        public async Task ChangeQuantity(string inventoryId, int quantity)
        {
            if (string.IsNullOrEmpty(inventoryId))
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ErrorCode.InvalidInput, "Inventory ID must be provided.");
            }

            var productInventory = await _unitOfWork.GetRepository<ProductInventory>().Entities
                .FirstOrDefaultAsync(pi => pi.Id == inventoryId);

            if (productInventory == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This ProductInventory is not found!");
            }

            productInventory.Quantity = quantity;
            await _unitOfWork.SaveAsync();
        }

        public async Task ChangePrice(string inventoryId, double price)
        {
            if (string.IsNullOrEmpty(inventoryId))
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ErrorCode.InvalidInput, "Inventory ID must be provided.");
            }

            var productInventory = await _unitOfWork.GetRepository<ProductInventory>().Entities
                .FirstOrDefaultAsync(pi => pi.Id == inventoryId);

            if (productInventory == null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This ProductInventory is not found!");
            }

            productInventory.Price = price;
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProductInventory(string id)
        {
            ProductInventory? productInventory = await _unitOfWork.GetRepository<ProductInventory>().GetByIdAsync(id)
            ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This ProductInventory is not found!");

            await _unitOfWork.GetRepository<Product>().DeleteAsync(productInventory);
            await _unitOfWork.SaveAsync();
        }
    }
}
