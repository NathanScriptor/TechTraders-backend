using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.IRepositories;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Service
{
    public class ProductImageService(IUnitOfWork unitOfWork, IBlobStorageHelper fileUploadHelper) : IProductImageService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IBlobStorageHelper _fileUploadHelper = fileUploadHelper;
        public async Task UploadImage(List<IFormFile> files, string productId)
        {
            var productImages = new List<ProductImage>();

            bool hasDefaultImage = await _unitOfWork.GetRepository<ProductImage>().Entities.AnyAsync(i => i.ProductId == productId && i.IsDefaultImage);

            foreach (var file in files)
            {
                var imageLink = await _fileUploadHelper.UploadFileAsync(file);

                productImages.Add(new ProductImage
                {
                    ImageLink = imageLink,
                    ProductId = productId,
                    IsDefaultImage = !hasDefaultImage && !productImages.Any()
                });
            }

            if (productImages.Any())
            {
                await _unitOfWork.GetRepository<ProductImage>().InsertRangeAsync(productImages);
                await _unitOfWork.SaveAsync();
            }
        }


        public async Task DeleteImage(string imageId)
        {
            await _fileUploadHelper.DeleteFileAsync(imageId);
            ProductImage? image = await _unitOfWork.GetRepository<ProductImage>().GetByIdAsync(imageId)
             ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This image is not found!");
            await _unitOfWork.GetRepository<ProductImage>().DeleteAsync(imageId);
            await _unitOfWork.SaveAsync();
        }
    }
}
