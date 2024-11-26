using ECommerce.Application.DTOs.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface IDiscountService
    {
        Task<GetDiscountDto> GetDiscount(string id);
        Task PostDiscount(PostDiscountDto model);
        Task PutDiscount(string id, PutDiscountDto model);
        Task DeleteDiscount(string id);
    }
}
