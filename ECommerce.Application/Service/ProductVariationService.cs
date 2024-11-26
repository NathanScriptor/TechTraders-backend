using AutoMapper;
using ECommerce.Application.DTOs.Product;
using ECommerce.Application.DTOs.Variation;
using ECommerce.Application.Interfaces.IRepositories;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Service
{
    public class ProductVariationService(IUnitOfWork unitOfWork, IMapper mapper) : IProductVariationService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<GetVariationDto>> GetVariation(string? id)
        {
            var variations = id == null
                ? await _unitOfWork.GetRepository<ProductVariation>().Entities.ToListAsync()
                : new List<ProductVariation>
                {
            await _unitOfWork.GetRepository<ProductVariation>().Entities
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new ErrorException(StatusCodes.Status404NotFound, ErrorCode.NotFound, "This variation is not found!")
                };

            return _mapper.Map<IEnumerable<GetVariationDto>>(variations);
        }


        public async Task PostVariation(PostVariationDto model)
        {
            ProductVariation? productVariation = await _unitOfWork.GetRepository<ProductVariation>().Entities.FirstOrDefaultAsync(obj => obj.Name == model.Name && obj.Type == model.Type); 
            if (productVariation != null)
            {
                throw new ErrorException(StatusCodes.Status409Conflict, ErrorCode.Conflicted, "This variation is already existed!");
            }

            await _unitOfWork.GetRepository<ProductVariation>().InsertAsync(new ProductVariation()
            {
                Name = model.Name,
                Type = model.Type,
            });
            await _unitOfWork.SaveAsync();
        }
    }
}
