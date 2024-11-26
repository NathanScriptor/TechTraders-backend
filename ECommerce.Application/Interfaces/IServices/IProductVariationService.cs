using ECommerce.Application.DTOs.Variation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface IProductVariationService
    {
        Task<IEnumerable<GetVariationDto>> GetVariation(string? id);
        Task PostVariation(PostVariationDto model);
    }
}
