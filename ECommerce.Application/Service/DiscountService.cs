using AutoMapper;
using ECommerce.Application.DTOs.Discount;
using ECommerce.Application.Interfaces.IRepositories;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Service
{
    public class DiscountService(IUnitOfWork unitOfWork, IMapper mapper) : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<GetDiscountDto> GetDiscount(string id) =>
            _mapper.Map<GetDiscountDto>(
                await _unitOfWork.GetRepository<Discount>().GetByIdAsync(id)
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This discount is not found!"));

        public async Task PostDiscount(PostDiscountDto model)
        {
            await _unitOfWork.GetRepository<Discount>().InsertAsync(new Discount
            {
                Name = model.Name,
                Description = model.Description,
                DiscountPercent = model.DiscountPercent,
                ExpiredAt = model.ExpiredAt,
                IsActive = true,
            });
            await _unitOfWork.SaveAsync();
        }

        public async Task PutDiscount(string id, PutDiscountDto model)
        {
            Discount? discount = await _unitOfWork.GetRepository<Discount>().GetByIdAsync(id)
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This discount is not found!");

            _mapper.Map(model, discount);
            discount.ModifiedAt = DateTime.Now;
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteDiscount(string id)
        {
            Discount? discount = await _unitOfWork.GetRepository<Discount>().GetByIdAsync(id)
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This discount is not found!");

            discount.IsActive = false;
            discount.DeletedAt = DateTime.Now;
            await _unitOfWork.SaveAsync();
        }
    }
}
