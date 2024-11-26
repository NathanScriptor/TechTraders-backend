using ECommerce.Application.Interfaces.IRepositories;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Service
{
    public class ProductDiscountService(IUnitOfWork unitOfWork) : IProductDiscountService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task PostProductDiscount(string discountId, string productId)
        {
            ProductDiscount? productDiscount = _unitOfWork.GetRepository<ProductDiscount>().Entities.FirstOrDefault(obj => obj.ProductId == productId && obj.DiscountId == discountId);

            if (productDiscount != null)
            {
                throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "Not Found");
            }

            ProductDiscount pd = new() { DiscountId = discountId , ProductId = productId};

            await _unitOfWork.GetRepository<ProductDiscount>().InsertAsync(pd);
            await _unitOfWork.SaveAsync();
        }
    }
}
