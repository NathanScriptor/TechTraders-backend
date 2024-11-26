using AutoMapper;
using ECommerce.Application.DTOs.InventoryVariation;
using ECommerce.Application.Interfaces.IRepositories;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Service
{
    public class InventoryVariationService : IInventoryVariationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InventoryVariationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task LinkInventoryWithVariation(PostInventoryVariationDto model)
        {
            // Ensure the ProductInventory and ProductVariation exist
            var productInventory = await _unitOfWork.GetRepository<ProductInventory>().Entities
                .FirstOrDefaultAsync(pi => pi.Id == model.ProductInventoryId)
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Product inventory not found!");

            var productVariation = await _unitOfWork.GetRepository<ProductVariation>().Entities
                .FirstOrDefaultAsync(pv => pv.Id == model.ProductVariationId)
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Product variation not found!");

            // Create InventoryVariation
            var inventoryVariation = new InventoryVariation
            {
                ProductInventoryId = productInventory.Id,
                ProductVariationId = productVariation.Id,
                ProductInventory = productInventory,
                ProductVariation = productVariation
            };

            await _unitOfWork.GetRepository<InventoryVariation>().InsertAsync(inventoryVariation);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteInventoryVariation(string inventoryVariationId)
        {
            var inventoryVariation = await _unitOfWork.GetRepository<InventoryVariation>().GetByIdAsync(inventoryVariationId)
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This inventory variation is not found!");

            await _unitOfWork.GetRepository<InventoryVariation>().DeleteAsync(inventoryVariation);
            await _unitOfWork.SaveAsync();
        }

    }
}
